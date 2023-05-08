using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirection : MonoBehaviour
{
    Vector2 rand;
    private float time;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 1;
        InvokeRepeating("Push", 1f, 1f);
    }
    
    private void Push()
    {
        rand = Random.insideUnitCircle;
        rb.AddForce(rand * 250 * Time.deltaTime, ForceMode2D.Impulse);
    }
}