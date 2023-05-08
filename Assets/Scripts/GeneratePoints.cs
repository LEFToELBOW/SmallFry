using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class GeneratePoints : MonoBehaviour
{
    public Vector2[] points;
    Vector2 genPoint;

    int x;
    int y;

    //private GameObject wallCheckTemp;
    private TilemapCollider2D wallCheck;
    private float xRange = 10;
    private float yRange = 7;
    

    [SerializeField] private Vector2 startPt;

    [SerializeField] private Transform enemyTransform;
    //public playerController playerController;

    // on awake (before start) script makes an array of vector2s that enemy follows
    private void Start()
    {
        wallCheck = GameObject.FindWithTag("tilemap").GetComponent<TilemapCollider2D>();

        points = new Vector2[4];

        enemyTransform = GetComponent<Transform>();
        for (int i = 0; i < points.Length; i++)
        {
            //points[i] = MakeNewPt();
        }
    }

    // generates vector2 w/ rand x & y
    // checks if the vector2 is within bounds of a collider—if so, regenerates the vector2 until its good
    //private Vector3 MakeNewPt()
    //{
    //    for(int i = x-2; x < x + 3; i++)
    //    {
    //        for(int j = y-2; y < y + 3; y++)
    //        {
    //            System.Random rand = new System.Random();
    //            if (RealScript.map[x, y] == 0)
    //            {
    //                int r = rand.Next(0, 100);
    //                if(r < 33)
    //                {
    //                    return new Vector3(i, j);
    //                }
    //            }
    //        }
    //    }
    //    return new Vector3(x, y);
    //}
    public void SetXandY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
