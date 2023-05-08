using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLightning : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Wait(0.65f));
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);

    }
}
