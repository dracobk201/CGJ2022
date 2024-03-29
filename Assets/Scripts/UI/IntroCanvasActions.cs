using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;

public class IntroCanvasActions : MonoBehaviour
{
    [SerializeField] private InputField usernameInputField = default(InputField);
    [SerializeField] private StringReference playfabUsername = default(StringReference);
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private GameEvent startGame = default(GameEvent);
    [SerializeField] private CanvasGroup canvasGroup = default(CanvasGroup);
    [Header("Audio")]
    [SerializeField] private AudioClipGameEvent sfxToPlay = default(AudioClipGameEvent);
    [SerializeField] private AudioClip uiConfirmAudio = default(AudioClip);

    private void Awake()
    {
        if (!PlayerPrefs.GetString("username", "none").Equals("none"))
        {
            playfabUsername.Value = PlayerPrefs.GetString("username", "none");
            usernameInputField.text = playfabUsername.Value;
        }
    }

    public void StartGame()
    {
        if (usernameInputField.text.Trim().Equals(string.Empty)) return;
        sfxToPlay.Raise(uiConfirmAudio);
        playfabUsername.Value = usernameInputField.text.Trim();
        PlayerPrefs.SetString("username", playfabUsername.Value);
        isGameStarted.Value = true;
        HidePanel();
        startGame.Raise();
    }

    private void HidePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
