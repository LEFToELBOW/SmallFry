using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;


public class ScanMap : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitFrame());
        Debug.Log("Scanned");
    }

    IEnumerator WaitFrame()
    {
        yield return new WaitForEndOfFrame();
        AstarPath.active.Scan();
    }
}
