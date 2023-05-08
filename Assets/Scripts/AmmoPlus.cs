using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPlus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PLayerMelee.ammoCount += 4;
            Destroy(this.gameObject);
        }
    }
}
