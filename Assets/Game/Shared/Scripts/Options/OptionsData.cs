using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionsData
{
    [SerializeField] private int qualityLevel; 
    [SerializeField] private float volume; 
    [SerializeField] private float cameraShake;

    private static string keyName = "options";

    public int QualityLevel { get => qualityLevel; set => qualityLevel = value; }
    public float Volume { get => volume; set => volume = value; }
    public float CameraShake { get => cameraShake; set => cameraShake = value; }

    public OptionsData() 
    {
        qualityLevel = 0;
        volume = 0;
        cameraShake = 1;
    }

    public static void SaveData(OptionsData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(keyName, json);
    }

    public static OptionsData GetData()
    {
        if(!PlayerPrefs.HasKey(keyName)) return new OptionsData();

        string json = PlayerPrefs.GetString(keyName);
        return JsonUtility.FromJson<OptionsData>(json);
    }
}
