using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasActions : MonoBehaviour
{
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessMaxAmount = default(FloatReference);
    [SerializeField] private Image hardnessBar = default(Image);

    public void Update()
    {
        UpdateHardnessBar();
    }

    private void UpdateHardnessBar()
    {
        hardnessBar.fillAmount = hardnessCurrentAmount.Value / hardnessMaxAmount.Value;
    }
}
