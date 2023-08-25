using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public static readonly string SceneName = "options";

    private OptionsData options;

    [SerializeField] private ToggleGroup qualityToggleGroup;
    [SerializeField] private List<Toggle> qualityToggles;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider cameraSlider;
    [SerializeField] private TMP_InputField txtDisplayName;
    [SerializeField] private Button btnClose;

    private void Start()
    {
        options = OptionsData.GetData();

        //qualityToggles = qualityToggleGroup.().ToList();

        //int index = 0;
        //for (int i = 0; i < qualityToggles.Count; i++)
        //{
        //    index = i;
        //    qualityToggles[i].onValueChanged.AddListener(value => QualityChange(index, value));
        //}

        volumeSlider.onValueChanged.AddListener(VolumeChange);
        cameraSlider.onValueChanged.AddListener(CameraChange);
        btnClose.onClick.AddListener(BtnCloseClick);        

    }

    private void BtnCloseClick()
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }

    private void VolumeChange(float value)
    {

    }

    private void CameraChange(float value)
    {

    }

    private void QualityChange(int index, bool isActive)
    {
        if (!isActive) return;

        QualitySettings.SetQualityLevel(index);
    }
}
