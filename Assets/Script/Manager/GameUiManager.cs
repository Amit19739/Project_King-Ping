using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public GameObject gameOverMenu;

    void Start()
    {
        gameOverMenu.SetActive(false);
    }
}
