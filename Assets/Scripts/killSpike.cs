using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killSpike : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Wait(0.4f));
    }

    // Update is called once per frame
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
        
    }
}
