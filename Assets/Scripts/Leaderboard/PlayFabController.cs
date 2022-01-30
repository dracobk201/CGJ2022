using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayFabController : MonoBehaviour
{
    [SerializeField] private BoolReference offlineMode = default(BoolReference);
    [SerializeField] private IntReference enemiesKilled = default(IntReference);
    [SerializeField] private StringReference playfabUsername = default(StringReference);
    [SerializeField] private StringCollection currentLeaderboard = default(StringCollection);
    [SerializeField] private GameEvent showLeaderboard = default(GameEvent);

    public void Login()
    {
        if (offlineMode.Value) return;
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void UpdateDisplayName()
    {
        if (offlineMode.Value) return;
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = playfabUsername.Value
        }, result => {
            Debug.Log("The player's display name is now: " + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    public void SendLeaderboard()
    {
        if (offlineMode.Value) return;
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "BestScore",
                    Value = enemiesKilled.Value
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLeaderboardError);
    }

    public void GetLearderboard()
    {
        if (offlineMode.Value) return;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "BestScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLeaderboardGetError);
    }

    #region Callbacks

    private void OnLoginSuccess(LoginResult result)
    {
        if (offlineMode.Value) return;
        Debug.Log($"{SystemInfo.deviceUniqueIdentifier} has been logged");
        UpdateDisplayName();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        if (offlineMode.Value) return;
        Debug.LogWarning("Something went wrong with your login call.");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        if (offlineMode.Value) return;
        Debug.Log($"Successful Leaderboard sent> {result.ToJson()}");
        GetLearderboard();
    }

    private void OnLeaderboardError(PlayFabError error)
    {
        if (offlineMode.Value) return;
        Debug.LogError("Something went wrong with Leadearboard Update.");
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        if (offlineMode.Value) return;
        currentLeaderboard.Clear();
        foreach (var currentPosition in result.Leaderboard)
            currentLeaderboard.Add($"{currentPosition.Position+1}. {currentPosition.DisplayName} has {currentPosition.StatValue} enemies killed");

        showLeaderboard.Raise();
        Debug.Log($"Successful Leaderboard get");
    }

    private void OnLeaderboardGetError(PlayFabError error)
    {
        if (offlineMode.Value) return;
        Debug.LogError("Something went wrong with Leadearboard Get.");
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion
}
