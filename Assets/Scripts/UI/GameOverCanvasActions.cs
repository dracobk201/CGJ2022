using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ScriptableObjectArchitecture;

public class GameOverCanvasActions : MonoBehaviour
{
    [SerializeField] private StringCollection currentLeaderboard = default(StringCollection);
    [SerializeField] private IntReference points = default(IntReference);
    [SerializeField] private CanvasGroup panelCanvasGroup = default(CanvasGroup);
    [SerializeField] private Text currentPointsLabel = default(Text);
    [SerializeField] private Text leaderboardText = default(Text);

    public void ShowGameOverPanel()
    {
        panelCanvasGroup.alpha = 1;
        panelCanvasGroup.blocksRaycasts = true;
        currentPointsLabel.text = $"{points.Value} points";
        leaderboardText.text = "Loading Leaderboard...";
    }

    public void ShowLeaderboard()
    {
        leaderboardText.text = string.Empty;
        foreach (var currentPosition in currentLeaderboard)
        {
            leaderboardText.text += currentPosition;
            leaderboardText.text += "\n";
        }
    }

    public void Restart()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
        SceneManager.LoadScene(0);
    }
}
