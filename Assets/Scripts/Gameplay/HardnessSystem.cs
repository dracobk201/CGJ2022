using UnityEngine;
using ScriptableObjectArchitecture;

public class HardnessSystem : MonoBehaviour
{
    [SerializeField] private BoolReference isGameOver = default(BoolReference);
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessIncrementalAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessMaxAmount = default(FloatReference);
    [SerializeField] private FloatReference mortalDamageValue = default(FloatReference);

    private void Start()
    {
        hardnessCurrentAmount.Value = 0;
    }

    public void ProjectileImpact(ProjectileType type)
    {
        if (type.Equals(ProjectileType.mortal))
        {
            hardnessCurrentAmount.Value -= mortalDamageValue.Value;
            if (hardnessCurrentAmount.Value <= 0)
            {
                hardnessCurrentAmount.Value = 0;
                isGameOver.Value = true;
            }
        }
        else if (type.Equals(ProjectileType.hardness))
        {
            hardnessCurrentAmount.Value += hardnessIncrementalAmount.Value;
            if (hardnessCurrentAmount.Value > hardnessMaxAmount.Value)
                hardnessCurrentAmount.Value = hardnessMaxAmount.Value;
        }
    }
}
