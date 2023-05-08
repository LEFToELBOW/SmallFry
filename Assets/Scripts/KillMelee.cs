using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMelee : MonoBehaviour
{
    void Start()
    {
        float time = 0;
        int level = RealScript.levelCount;
        if(ExpAndHealth.expLevel <= 3)
        {
            time = .2f;
        }
        else if(ExpAndHealth.expLevel <= 6)
        {
            time = .4f;
        }
        else if(ExpAndHealth.expLevel <= 9)
        {
            time = .6f;
        }
        StartCoroutine(Wait(time));
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
