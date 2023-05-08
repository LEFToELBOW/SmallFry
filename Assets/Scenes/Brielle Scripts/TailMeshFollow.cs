using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailMeshFollow : MonoBehaviour
{
    //Description: This allows the mesh holder for the tails of fish to follow the fish without interfering with the functioning of the PlayerController script.

    //Changes to be made:
    // -

    //Change Log:
    // - This is a modified version of the CameraFollow Script that allows for rotation
    // - The tail mesh holder matches the position of the fish exactly


    // Start is called before the first frame update
    private Transform FakeParent;


    private void Start()
    {
        FakeParent = GameObject.FindWithTag("Player").transform;

        //If there is a selected parent object, go to SetFakeParent()
        if (FakeParent != null)
        {
            SetFakeParent(FakeParent);
        }
    }

    private void Update()
    {
        //Will not function if there is no selected parent (in this case a fish)
        if (FakeParent == null)
            return;

        //Sets the targeted position equal to parent object's position - the wanted distance b/w the tail and the fish
        var targetPos = FakeParent.position;
        var targetRot = FakeParent.localRotation;

        //Calls RotatePointAroundPivot() which calculates the new position of the camera
        //transform.position = RotatePointAroundPivot(targetPos, FakeParent.position, targetRot);
        transform.position = targetPos;
        transform.localRotation = targetRot;

    }

    //Gets the difference in position and rotation between the camera and the player before the first frame.
    public void SetFakeParent(Transform parent)
    {
        //Our fake parent
        FakeParent = parent;
    }

    
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        //Get a vector for the direction from the pivot (parent's position) to the target position (point)
        Vector3 dir = point - pivot;

        //Rotate vector around parent's position
        dir = rotation * dir;

        
        //Calc the target position by adding the direction vector to the current position
        point = dir + pivot;

        //Return calculated vector if the magnitude of the dif. in positions is > 5

        if (dir.magnitude > 5)
        {
            return point;
        }

        //Otherwise do not move the camera
        else
        {
            return new Vector3(0,0,0);
        }
    }
}
