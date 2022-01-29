using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasActions : MonoBehaviour
{
    [SerializeField] private BoolReference isHardnessActive = default(BoolReference);
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessMaxAmount = default(FloatReference);
    [SerializeField] private Image hardnessBar = default(Image);
    [SerializeField] private Image hardnessActiveIcon = default(Image);

    private void Start()
    {
        hardnessActiveIcon.color = new Color(hardnessActiveIcon.color.r, hardnessActiveIcon.color.g, hardnessActiveIcon.color.b, 0);
    }

    private void Update()
    {
        UpdateHardnessBar();
    }

    private void UpdateHardnessBar()
    {
        hardnessBar.fillAmount = hardnessCurrentAmount.Value / hardnessMaxAmount.Value;
    
        if (isHardnessActive.Value && hardnessActiveIcon.color.a == 0)
            hardnessActiveIcon.color = new Color(hardnessActiveIcon.color.r, hardnessActiveIcon.color.g, hardnessActiveIcon.color.b, 1);
        else if (!isHardnessActive.Value && hardnessActiveIcon.color.a == 1)
            hardnessActiveIcon.color = new Color(hardnessActiveIcon.color.r, hardnessActiveIcon.color.g, hardnessActiveIcon.color.b, 0);
    }

}
