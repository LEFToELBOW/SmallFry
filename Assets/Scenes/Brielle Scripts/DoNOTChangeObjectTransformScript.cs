using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNOTChangeObjectTransformScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);

    }
}
