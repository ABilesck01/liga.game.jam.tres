using UnityEngine;
using PlayFab;
using System;
using PlayFab.ClientModels;

public class Login
{
    private static readonly string titleId = "56036";

    public static void LoginWithCustomId(Action<LoginResult> onSuccess = null, Action<PlayFabError> onError = null)
    {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            TitleId = titleId
        }, onSuccess, onError);

    }
}
