using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SHOOT : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    [SerializeField] private Transform player;

    private void Start()
    {
        InvokeRepeating("Shoot", 0.2f, 1f);
    }
    private void Shoot()
    {
        Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(transform.right * 1200);    
    }
}
