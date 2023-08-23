using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject inGameMenuGameObject;

    private void OnEnable()
    {
        MainScreen.OnStartGame += MainScreen_OnStartGame;
    }

    private void OnDisable()
    {
        MainScreen.OnStartGame -= MainScreen_OnStartGame;
    }

    private void MainScreen_OnStartGame(object sender, System.EventArgs e)
    {
        inGameMenuGameObject.SetActive(true);
    }
}
