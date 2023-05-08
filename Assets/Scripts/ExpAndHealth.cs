using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExpAndHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar, expBar;
    [SerializeField] private TMP_Text expCountText;
    public static float maxHealth, health, maxExp, exp, score, tempHealth, tempExp;
    public static int expLevel;

    private void Start()
    {
        maxHealth = 100;
        health = 100;
        maxExp = 100;
        exp = 0;
        score = 0;
        tempExp = 0;
        tempHealth = health;
        UpdateHealthBar();
        UpdateExpBar();
    }
    private void Update()
    {
        if(tempHealth != health)
        {
            health = tempHealth;
            if(health >= maxHealth)
            {
                health = maxHealth;
            }
            else if(health <= 0)
            {
                SceneManager.LoadScene("End Game");
                //expCountText.text = exp.ToString();
            }
            UpdateHealthBar();
        }
        if(tempExp != exp)
        {
            if(exp >= maxExp)
            {
                exp = 0 + exp - maxExp;
                expLevel++;
                expCountText.text = expLevel.ToString();
                tempExp= exp;
            }
            exp = tempExp;
            UpdateExpBar();
        }
    }
    public void UpdateHealthBar()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1f);
    }
    public void UpdateExpBar()
    {
        expBar.fillAmount = Mathf.Clamp(exp / maxExp, 0, 1f);
    }
}
