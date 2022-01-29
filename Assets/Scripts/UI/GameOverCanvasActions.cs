using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCanvasActions : MonoBehaviour
{
    [SerializeField] private CanvasGroup panelCanvasGroup = default(CanvasGroup);

    public void ShowGameOverPanel()
    {
        panelCanvasGroup.alpha = 1;
        panelCanvasGroup.blocksRaycasts = true;
    }

    public void Restart()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
        SceneManager.LoadScene(0);
    }
}
