using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string SceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("Scene changed");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exited");
    }

    private void Update()
    {
        if(Input.GetKey("escape")) 
        {
            Application.Quit();
            Debug.Log("Exited");
        }
    }
}
