using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class SpeedBoost : MonoBehaviour
{
    private SpriteRenderer own;
    private void Start()
    {
        own = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerController.moveSpeed = 14f;
            ExpAndHealth.tempHealth = ExpAndHealth.tempHealth + 5;
            StartCoroutine(Wait(2.5f));
            Destroy(own);
        }

        IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
            playerController.moveSpeed = 10;
            Destroy(this.gameObject);
        }

    }
}
