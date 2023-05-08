using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PLayerMelee : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    [SerializeField] private Transform player;
    public static int ammoCount;
    [SerializeField] private TMP_Text ammoCountText;
    private void Start()
    {
        ammoCount = 6;
    }
    private void Update()
    {
        ammoCountText.text = ammoCount.ToString();
        if(Input.GetKeyDown(KeyCode.Space) == true && ammoCount > 0)
        {
            Shoot();
            ammoCount--;
        }
    }

    private void Shoot()
    { 
        Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(transform.right * 1200);
    }
}

