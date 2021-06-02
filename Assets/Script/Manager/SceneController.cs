using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene("First Level");
    }

    public void GameExitButton()
    {
        Application.Quit();
        Debug.Log("Game Exit....");
    }
}
