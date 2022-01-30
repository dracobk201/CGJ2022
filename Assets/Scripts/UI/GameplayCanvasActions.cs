using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasActions : MonoBehaviour
{
    [SerializeField] private BoolReference isHardnessActive = default(BoolReference);
    [SerializeField] private IntReference mortalProjectilesDestroyed = default(IntReference);
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessMaxAmount = default(FloatReference);
    [SerializeField] private FloatReference timePlayed = default(FloatReference);
    [SerializeField] private CanvasGroup canvasGroup = default(CanvasGroup);
    [SerializeField] private Image hardnessBar = default(Image);
    [SerializeField] private Image hardnessActiveIcon = default(Image);
    [SerializeField] private Text mortalProjectilesLabel = default(Text);
    [SerializeField] private Text currentTimeLabel = default(Text);

    private void Start()
    {
        hardnessActiveIcon.color = new Color(hardnessActiveIcon.color.r, hardnessActiveIcon.color.g, hardnessActiveIcon.color.b, 0);
    }

    private void Update()
    {
        UpdateHardnessBar();
        UpdateTimer();
        UpdateMortalProjectiles();
    }

    private void UpdateHardnessBar()
    {
        hardnessBar.fillAmount = hardnessCurrentAmount.Value / hardnessMaxAmount.Value;
    
        if (isHardnessActive.Value && hardnessActiveIcon.color.a == 0)
            hardnessActiveIcon.color = new Color(hardnessActiveIcon.color.r, hardnessActiveIcon.color.g, hardnessActiveIcon.color.b, 1);
        if (!isHardnessActive.Value && hardnessActiveIcon.color.a == 1)
            hardnessActiveIcon.color = new Color(hardnessActiveIcon.color.r, hardnessActiveIcon.color.g, hardnessActiveIcon.color.b, 0);
    }

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(timePlayed.Value / 60f);
        int seconds = Mathf.RoundToInt(timePlayed.Value % 60f);

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        currentTimeLabel.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void UpdateMortalProjectiles()
    {
        mortalProjectilesLabel.text = mortalProjectilesDestroyed.Value.ToString("000");
    }

    public void ShowPanel()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    private void HidePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

}
