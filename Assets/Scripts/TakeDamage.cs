using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private static bool invincible;
    private int time = 5;
    private SpriteRenderer rend;
    private LineRenderer rendT;
    [SerializeField] private GameObject tail;
    private void Start()
    {
        invincible = true;
        rendT = tail.GetComponent<LineRenderer>();
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(Wait(time));
    }
    public void InduceInvincibility()
    {
        invincible = true;
        StartCoroutine(Wait(time));
    }
    IEnumerator Wait(float time)
    {
        rendT.startColor = Color.yellow;
        rendT.endColor = Color.yellow;
        rend.color = Color.yellow;
        yield return new WaitForSeconds(time);
        invincible = false;
        rend.color = Color.magenta;
        rendT.startColor = Color.magenta;
        rendT.endColor = Color.magenta;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(invincible == false)
        {
            if (collision.gameObject.tag == "enemy")
            {
                ExpAndHealth.tempHealth = ExpAndHealth.tempHealth - 10;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(invincible == false)
        {
            if (collision.gameObject.tag == "eelProjectile")
            {
                ExpAndHealth.tempHealth = ExpAndHealth.tempHealth - 30;
            }
            if (collision.gameObject.tag == "pufferProjectile")
            {
                ExpAndHealth.tempHealth = ExpAndHealth.tempHealth - 15;
            }
        }
    }
}
    