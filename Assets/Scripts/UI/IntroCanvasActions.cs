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

    public void Awake()
    {
        isGameStarted.Value = false;
        if (!string.IsNullOrEmpty(playfabUsername.Value.Trim()))
            usernameInputField.text = playfabUsername.Value;
    }

    public void StartGame()
    {
        if (usernameInputField.text.Trim().Equals(string.Empty)) return;
        playfabUsername.Value = usernameInputField.text.Trim();
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
