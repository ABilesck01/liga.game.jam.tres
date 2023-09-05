using UnityEngine;
using PlayFab;
using System;
using PlayFab.ClientModels;

public class Login
{
    private static readonly string titleId = "56036";

    public static string playfabId;
    public static string displayName;

    public static void LoginWithCustomId(Action<LoginResult> onSuccess = null, Action<PlayFabError> onError = null)
    {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            TitleId = titleId
        }, onSuccess, onError);

    }
    
    public static void GetDisplayName()
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest
        {
            PlayFabId = playfabId
        },
        success =>
        {
            displayName = success.PlayerProfile.DisplayName;
        },
        error =>
        {
            displayName = "";
        });

    }

    public static void SetDisplayName(string newName)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = newName
        },
        onSuccess =>
        {
            Login.displayName = newName;
        },
        error =>
        {
            Debug.LogError(error.ErrorMessage);
        });
    }
}
