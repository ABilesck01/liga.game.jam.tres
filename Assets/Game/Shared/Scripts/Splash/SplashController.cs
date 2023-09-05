using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    [SerializeField] private Transform loadCircle;
    [SerializeField] private Vector3 axis;
    [Space]
    [SerializeField] private AudioMixer audioMixer;
    [Space]
    [SerializeField] private Slider slider;


    private AsyncOperation operation;

    private void Start()
    {
        var options = OptionsData.GetData();

        audioMixer.SetFloat("master", options.Volume);

        StartCoroutine(LoadGame());

        Login.LoginWithCustomId(success =>
        {
            operation.allowSceneActivation = true;
            if (success.NewlyCreated)
                Login.SetDisplayName($"User {SystemInfo.deviceUniqueIdentifier.Substring(0, 4)}");
            else
            {
                Login.playfabId = success.PlayFabId;
                Login.GetDisplayName();
            }
        }, 
        error =>
        {
            Debug.LogError(error.ErrorMessage);
        });
    }

    private void Update()
    {
        if (loadCircle == null) return;

        loadCircle.Rotate(axis *  Time.deltaTime);
    }

    private IEnumerator LoadGame()
    {
        operation = SceneManager.LoadSceneAsync("game");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            yield return null;
            slider.value = operation.progress;
        }
    }
}
