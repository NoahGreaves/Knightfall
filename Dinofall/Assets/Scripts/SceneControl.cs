using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // loads the main game scene
    public void LoadScene(string MainGame)
    {
        SceneManager.LoadScene(MainGame);
    }

    // quits the application
    public void Quit()
    {
        Debug.Log("Quitting!");
        Application.Quit();
    }
}
