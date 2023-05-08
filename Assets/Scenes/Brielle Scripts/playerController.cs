using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerController : MonoBehaviour
{
    //Description: This allows the player character to rotate to a specific target. Currently, this is the mouse. However, I will change this so that this function is compatable with the joystick.
    //Changes to be made:
    // - No longer moves towards the mouse (Done)
    // - Dash Function
    public static Transform playerTransformRef;
    public static TilemapCollider2D mapColRef;
    //public PlayerController playerController;

    public float rotationSpeed;
    private Vector2 direction;
    private Vector2 position;

    public static float moveSpeed= 10;

    private float angle;

    private void Start()
    {
        //playerController = GetComponent<playerController>();
    }

    void Update()
    {
        //The sprite being rotated should face right by default in the sprite editor
        //If you want the object to rotate to something other than the mouse, just change Camera.main...mousePosition) to something else


        //Takes input from the joystick/keys; unfortunately w/ just the keys you can't get more than 8-dir movement like you can with the joystick, but you get the idea
        //Getting the direction we want to move in and the point we want to move towards
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        position = new Vector2(Input.GetAxis("Horizontal") + transform.position.x, Input.GetAxis("Vertical") + transform.position.y);

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Moves and rotates the player in the direction of the input, rotates only if there is a desired change in direction
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);


        //Change: the player no longer moves towards the mouse
        /*
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        //Moving the object towards the cursor's position on the screen
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = Vector2.MoveTowards(transform.position, cursorPos, moveSpeed * Time.deltaTime);
        */
    }


    /*
    public void CreateCollider(Mesh tailMesh, LineRenderer lineRend, MeshCollider mc)
    {
        lineRend.BakeMesh(tailMesh, true);
        mc.sharedMesh = tailMesh;
    }
    */

}
