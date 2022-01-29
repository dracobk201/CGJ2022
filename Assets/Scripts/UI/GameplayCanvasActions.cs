using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasActions : MonoBehaviour
{
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessMaxAmount = default(FloatReference);
    [SerializeField] private Image hardnessBar = default(Image);

    private void Start()
    {
        hardnessBar.fillAmount = 0;
        UpdateHardnessBar();
    }

    public void UpdateValues(ProjectileType type)
    {
        Invoke(nameof(UpdateHardnessBar), 0.1f);
    }

    private void UpdateHardnessBar()
    {
        hardnessBar.fillAmount = hardnessCurrentAmount.Value / hardnessMaxAmount.Value;
    }
}
