using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public static readonly string SceneName = "options";

    private OptionsData optionsData;

    [SerializeField] private List<Toggle> qualityToggles;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider cameraSlider;
    [SerializeField] private TMP_InputField txtDisplayName;
    [SerializeField] private Button btnClose;

    [Space]
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        optionsData = OptionsData.GetData();

        volumeSlider.onValueChanged.AddListener(VolumeChange);
        cameraSlider.onValueChanged.AddListener(CameraChange);
        btnClose.onClick.AddListener(BtnCloseClick);

        for (int i = 0; i < qualityToggles.Count; i++)
        {
            int index = i;
            qualityToggles[i].isOn = i == optionsData.QualityLevel;
            qualityToggles[i].onValueChanged.AddListener(value => QualityChange(index, value));
        }

        volumeSlider.value = optionsData.Volume;
        cameraSlider.value = optionsData.CameraShake;
        txtDisplayName.text = Login.displayName;

    }

    private void BtnCloseClick()
    {
        SceneManager.UnloadSceneAsync(SceneName);
        Login.SetDisplayName(txtDisplayName.text);
        OptionsData.SaveData(optionsData);
    }

    private void VolumeChange(float value)
    {
        audioMixer.SetFloat("master", value);
        optionsData.Volume = value;
    }

    private void CameraChange(float value)
    {
        CameraShake.Intensity = value;
        optionsData.CameraShake = value;
    }

    private void QualityChange(int index, bool isActive)
    {
        if (!isActive) return;

        Debug.Log(index);

        QualitySettings.SetQualityLevel(index);
        optionsData.QualityLevel = index;
    }
}
