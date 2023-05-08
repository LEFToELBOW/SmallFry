using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advance : MonoBehaviour
{
    bool on;
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player").transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            on = true;
            StartCoroutine(Wait(2f));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            on = false;
        }
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        if(on)
        {
            RealScript.levelCount++;
            on = false;
            player.GetComponent<TakeDamage>().InduceInvincibility();
            
        }
    }
}
