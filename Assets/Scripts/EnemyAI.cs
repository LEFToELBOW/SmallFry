using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using System;


public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform playerTarget, enemyTransform;

    public float speed, rotSpeed, waypointSetDistance, trackingDistance;

    Path path;
    int currentWaypoint, idlePointIncrement = 0;
    bool reachedEndOfPath, isTargeting = false;

    Vector2[] idlePoints;

    Seeker seeker;
    Rigidbody2D rb;
    void Start()
    {
        playerTarget = GameObject.FindWithTag("Player").GetComponent<Transform>();

        speed = 250f;
        rotSpeed = 40f;
        waypointSetDistance = 1.5f;
        trackingDistance = 15f;
        
        enemyTransform = GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        idlePoints = new Vector2[4];

        Array.Copy(GetComponent<GeneratePoints>().points, idlePoints, 4);

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

   
    // either calculates path toward player or toward a placeholder pt based on isTargeting
    void UpdatePath()
    { 
        if (seeker.IsDone() && isTargeting)
        {
            seeker.StartPath(enemyTransform.position, playerTarget.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // determines whether to do holding pattern or chasing player
    bool distCheck(float tdist)
    {
        bool targetFunc = false;

        if (Vector2.Distance(enemyTransform.position, playerTarget.position) < tdist)
        {
            targetFunc = true;
        }
        else
        {
            targetFunc = false;
        }
        return targetFunc;
    }

    void AStarTracking()
    {
        // a* stuff
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // applies force to rb based on direction of path, & lerped so it is smoother
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)enemyTransform.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;    

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        enemyTransform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);

        rb.AddForce(force);

        float distanceToNextWaypoint = Vector2.Distance(enemyTransform.position, path.vectorPath[currentWaypoint]);

        if (distanceToNextWaypoint < waypointSetDistance)
        {
            currentWaypoint++;
        }   
    }

    void MoveBetweenPoints()
    {
        if (path == null)
        {
            return;
        }
        // if enemy reaches the intended placeholder pt, the array will increment by 1 so it is constantly moving
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            
            idlePointIncrement++;
            if (idlePointIncrement == idlePoints.Length)
            {
                idlePointIncrement = 0;
            }
            return;
        }
        else
        {
            reachedEndOfPath = false;      
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)enemyTransform.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        enemyTransform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);

        rb.AddForce(force);

        float distanceToNextWaypoint = Vector2.Distance(enemyTransform.position, path.vectorPath[currentWaypoint]);

        if (distanceToNextWaypoint < waypointSetDistance)
        {
            currentWaypoint++;
        }
    }

    void FixedUpdate()
    {
        isTargeting = distCheck(trackingDistance);

        if (isTargeting)
        {
            AStarTracking();
        }
        else
        {
            MoveBetweenPoints();
        }
    }
}
