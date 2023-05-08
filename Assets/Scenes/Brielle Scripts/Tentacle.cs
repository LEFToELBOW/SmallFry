using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    //SUPER important notes so that this script places the mesh collider of the tail in the right position:
    //   - The tail renderer must be on the same level of the hierarchy as the object that moves the fish's body
    //   - If you want the fish to spawn in a different location, change the location of the fish's body as it has the player controller script and is parent to the wiggle/targetDirs



    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;

    //position the first point is to move towards
    public Transform targetDir;
    //the ideal distance between points on the tentacle; prevents bunching up
    public float targetDist;
    //speed at which the tentacle moves
    public float smoothSpeed;
    public float trailSpeed;

    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    private Mesh tailMesh;
    public GameObject meshHolder;
    private MeshCollider mc;

    //public PlayerController controller;

    private void Start()
    {
        //adds length # of points along the line to achieve a smooth, slithery motion instead of stiff movement of the whole line
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];

        tailMesh = new Mesh();
        mc = meshHolder.GetComponent<MeshCollider>();
    }

    private void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        //assigns the location of the first point to the target's position
        segmentPoses[0] = targetDir.position;

        //for loop to assign the target of the rest of the segments in a smooth following motion
        for(int i = 1; i < segmentPoses.Length; i++)
        {
            //SmoothDamp() slowly moves the transform of each point to the transform of the point before it over time
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed + i / trailSpeed);
        }
        lineRend.SetPositions(segmentPoses);

        CreateCollider();

        //controller.CreateCollider(tailMesh, lineRend, mc);
    }


    public void CreateCollider()
    {
        lineRend.BakeMesh(tailMesh, true);
        mc.sharedMesh = tailMesh;
    }
}
