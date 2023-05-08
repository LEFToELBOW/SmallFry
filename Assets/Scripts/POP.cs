using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class POP : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    [SerializeField] private Transform player;

    private bool big;

    private void Start()
    {
        InvokeRepeating("Everthing", .02f, 1f);
    }
    private void Shoot()
    {
        GameObject lightIns1 = Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject lightIns2 = Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject lightIns3 = Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject lightIns4 = Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject lightIns5 = Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject lightIns6 = Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject lightIns7 = Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject lightIns8 = Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        Rigidbody2D lightInsRb1 = lightIns1.GetComponent<Rigidbody2D>();
        Rigidbody2D lightInsRb2 = lightIns2.GetComponent<Rigidbody2D>();
        Rigidbody2D lightInsRb3 = lightIns3.GetComponent<Rigidbody2D>();
        Rigidbody2D lightInsRb4 = lightIns4.GetComponent<Rigidbody2D>();
        Rigidbody2D lightInsRb5 = lightIns5.GetComponent<Rigidbody2D>();
        Rigidbody2D lightInsRb6 = lightIns6.GetComponent<Rigidbody2D>();
        Rigidbody2D lightInsRb7 = lightIns7.GetComponent<Rigidbody2D>();
        Rigidbody2D lightInsRb8 = lightIns8.GetComponent<Rigidbody2D>();

        lightInsRb1.AddForce(Vector2.up * 500);
        lightInsRb2.AddForce(Vector2.left * 500);
        lightInsRb3.AddForce(Vector2.right * 500);
        lightInsRb4.AddForce(Vector2.down * 500);
        lightInsRb5.AddForce(Vector2.one * 500);
        lightInsRb6.AddForce(new Vector2(-1, 1) * 500);
        lightInsRb7.AddForce(new Vector2(-1, -1) * 500);
        lightInsRb8.AddForce(new Vector2(1, -1) * 500);
        Grow();
        StartCoroutine(Wait(1));
    }

    private void Grow()
    {
        if(big == false)
        {
            for (float t = 0; t < 10; t += Time.deltaTime / 3f)
            {
                player.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(3, 3, 1), t);
            }
            big = true;
        }
        else if(big == true)
        {
            for (float t = 0; t < 10; t+= Time.deltaTime/3f)
                player.transform.localScale = Vector3.Lerp(new Vector3(2, 2, 1), new Vector3(1, 1, 1), t);
            big = false;
        }
        
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Shoot();
    }
    private void Everything()
    {
        Make(Vector2.up);
        Make(Vector2.right);
        Make(Vector2.left);
        Make(Vector2.down);
        Make(Vector2.one);
        Make(-Vector2.one);
        Make(new Vector2(-1, 1));
        Make(new Vector2(1, -1));
        Grow();
    }
    private void Make(Vector2 vec)
    {
        Instantiate(lightning, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(vec * 500);
    }
}
