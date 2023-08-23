using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    private void Start()
    {
        Login.LoginWithCustomId(success =>
        {
            SceneManager.LoadScene("game");
        }, 
        error =>
        {
            Debug.LogError(error.ErrorMessage);
        });
    }
}
