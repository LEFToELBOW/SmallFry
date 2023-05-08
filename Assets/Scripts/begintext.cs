using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class begintext : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject titleScreen;
    public void ActivateCredits()
    {
        titleScreen.SetActive(false);
        credits.SetActive(true);
        StartCoroutine(Wait(10));
        
    }

    public void ActivateInstructions()
    {
        titleScreen.SetActive(false);
        instructions.SetActive(true);
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        credits.SetActive(false);
        titleScreen.SetActive(true);
    }
}
