using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle2 : MonoBehaviour
{
    //I do not plan to use this script in this project, the wiggle function doesn't seem to be working and I don't care to try and fix it


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

    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    private void Start()
    {
        //adds length # of points along the line to achieve a smooth, slithery motion instead of stiff movement of the whole line
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    private void Update()
    {

        //assigns the location of the first point to the target's position
        segmentPoses[0] = targetDir.position;

        //for loop to assign the target of the rest of the segments in a smooth following motion

        for(int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDist;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
        }

        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
        
        lineRend.SetPositions(segmentPoses);
    }
}
