using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreshow : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    
    void Start()
    {
        scoreText.text  = ("Congratulations! You got " + ExpAndHealth.score.ToString() + " XP!");
    }

    
}
