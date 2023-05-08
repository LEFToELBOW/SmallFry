using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image healthBarImage, healthBar;
    public float health, maxHealth;
    private void Start()
    {
        healthBar.enabled = false;
        healthBarImage.enabled = false;
    }
    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1f);
    }

    public void TakeDamage()
    {
        health = health - 40;
        UpdateHealthBar();
    }

    private void Update()
    {
        if(this.health <= 0)
        {
            ExpAndHealth.tempExp = ExpAndHealth.tempExp + 10;
            ExpAndHealth.score = ExpAndHealth.score + 10;
            Destroy(this.transform.parent.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerProjectile")
        {
            TakeDamage();
            healthBar.enabled = true;
            healthBarImage.enabled = true;
        }
    }
}