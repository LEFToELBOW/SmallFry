using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Description: This allows the camera (or any object) to follow the player character without interfering with the functioning of the PlayerController script.

    //Changes to be made:
    // -

    //Change Log:
    // - Commented out lines that caused the camera to rotate with the player; may implement later. Interacted strangely with the PlayerController script.


    // Start is called before the first frame update
    private Transform FakeParent;

    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;

    private void Start()
    {
        FakeParent = GameObject.FindWithTag("Player").transform;
        //If there is a selected parent object, go to SetFakeParent()
        if (FakeParent != null)
        {
            SetFakeParent(FakeParent);
        }
        //Do not change the z coordinate of the camera or else game objects will not be rendered.
        transform.position = new Vector3(FakeParent.transform.localPosition.x, FakeParent.transform.localPosition.y, 0);
    }

    private void Update()
    {
        //Will not function if there is no selected parent (in this case the player)
        if (FakeParent == null)
            return;

        //Sets the targeted position equal to parent object's position - the wanted distance b/w the camera and the player
        var targetPos = FakeParent.position - _positionOffset;
        //var targetRot = FakeParent.localRotation * _rotationOffset;

        //I've set targetRot = 0 so the camera does not rotate
        var targetRot = new Quaternion(0, 0, 0, 0);

        //Calls RotatePointAroundPivot() which calculates the new position of the camera
        transform.position = RotatePointAroundPivot(targetPos, FakeParent.position, targetRot);
        //transform.localRotation = targetRot;

    }

    //Gets the difference in position and rotation between the camera and the player before the first frame.
    public void SetFakeParent(Transform parent)
    {
        //Offset vector
        _positionOffset = parent.position - transform.position;
        //Offset rotation
        _rotationOffset = Quaternion.Inverse(parent.localRotation * transform.localRotation);
        //Our fake parent
        FakeParent = parent;
    }

    //
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        //Get a vector for the direction from the pivot (parent's position) to the target position (point)
        Vector3 dir = point - pivot;

        //Rotate vector around parent's position
        //dir = rotation * dir;

        
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
