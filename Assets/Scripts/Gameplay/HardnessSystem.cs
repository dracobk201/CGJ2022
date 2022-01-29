using UnityEngine;
using System.Collections;
using ScriptableObjectArchitecture;
using MoreMountains.NiceVibrations;

public class HardnessSystem : MonoBehaviour
{
    [SerializeField] private BoolReference isGameOver = default(BoolReference);
    [SerializeField] private BoolReference isHardnessActive = default(BoolReference);
    [SerializeField] private FloatReference hardnessDecreaseFactor = default(FloatReference);
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessIncrementalAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessMaxAmount = default(FloatReference);
    [SerializeField] private FloatReference mortalDamageValue = default(FloatReference);
    [SerializeField] private GameEvent playerDead = default(GameEvent);

    public void ProjectileImpact(ProjectileType type)
    {
        if (type.Equals(ProjectileType.mortal))
        {
            if (isHardnessActive.Value == true)
            {
                MMVibrationManager.Haptic(HapticTypes.RigidImpact);
                hardnessCurrentAmount.Value -= mortalDamageValue.Value;
            }
            else
            {
                MMVibrationManager.Haptic(HapticTypes.Failure);
                hardnessCurrentAmount.Value = 0;
            }

            if (hardnessCurrentAmount.Value <= 0)
            {
                hardnessCurrentAmount.Value = 0;
                isGameOver.Value = true;
                playerDead.Raise();
            }
        }
        else if (type.Equals(ProjectileType.hardness))
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            hardnessCurrentAmount.Value += hardnessIncrementalAmount.Value;
            if (hardnessCurrentAmount.Value > hardnessMaxAmount.Value)
                hardnessCurrentAmount.Value = hardnessMaxAmount.Value;
        }
    }

    public void PlayerTapped()
    {
        if (!isHardnessActive.Value && hardnessCurrentAmount.Value > 0)
            StartCoroutine(TriggerHardness());
    }

    private IEnumerator TriggerHardness()
    {
        isHardnessActive.Value = true;
        while (hardnessCurrentAmount.Value > 0)
        {
            hardnessCurrentAmount.Value -= hardnessDecreaseFactor.Value * Time.deltaTime;
            yield return null;
        }
        isHardnessActive.Value = false;
    }
}
