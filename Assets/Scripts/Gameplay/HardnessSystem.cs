using UnityEngine;
using ScriptableObjectArchitecture;
using MoreMountains.NiceVibrations;

public class HardnessSystem : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material normalSphere = default(Material);
    [SerializeField] private Material hardSphere = default(Material);
    [Header("Gameplay")]
    [SerializeField] private BoolReference isGameOver = default(BoolReference);
    [SerializeField] private GameEvent playerDead = default(GameEvent);
    [Header("Hardness")]
    [SerializeField] private BoolReference isHardnessActive = default(BoolReference);
    [SerializeField] private FloatReference hardnessDecreaseFactor = default(FloatReference);
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessIncrementalAmount = default(FloatReference);
    [SerializeField] private FloatReference hardnessMaxAmount = default(FloatReference);
    [SerializeField] private FloatReference mortalDamageValue = default(FloatReference);
    [Header("Audio")]
    [SerializeField] private AudioClipGameEvent sfxToPlay = default(AudioClipGameEvent);
    [SerializeField] private AudioClip turningOffHardnessAudio = default(AudioClip);
    [SerializeField] private AudioClip turningOnHardnessAudio = default(AudioClip);
    [SerializeField] private AudioClip gameOverAudio = default(AudioClip);

    private void Update()
    {
        if (hardnessCurrentAmount.Value > 0 && isHardnessActive.Value)
        {
            hardnessCurrentAmount.Value -= hardnessDecreaseFactor.Value * Time.deltaTime;
            if (hardnessCurrentAmount.Value <= 0)
            {
                hardnessCurrentAmount.Value = 0;
                isHardnessActive.Value = false;
                this.GetComponent<Renderer>().material = normalSphere;
            }
        }
    }

    public void ProjectileImpact(ProjectileType type)
    {
        if (type.Equals(ProjectileType.Mortal))
        {
            if (isHardnessActive.Value == true)
            {
                MMVibrationManager.Haptic(HapticTypes.RigidImpact);
                hardnessCurrentAmount.Value -= mortalDamageValue.Value;
            }
            else
            {
                MMVibrationManager.Haptic(HapticTypes.Failure);
                //hardnessCurrentAmount.Value -= 500;
                isGameOver.Value = true;
                sfxToPlay.Raise(gameOverAudio);
                playerDead.Raise();
            }

            if (hardnessCurrentAmount.Value <= 0)
            {
                hardnessCurrentAmount.Value = 0;
                //isGameOver.Value = true;
                //playerDead.Raise();
                //sfxToPlay.Raise(gameOverAudio);
                isHardnessActive.Value = false;
                this.GetComponent<Renderer>().material = normalSphere;
            }
        }
        else if (type.Equals(ProjectileType.Hardness))
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            hardnessCurrentAmount.Value += hardnessIncrementalAmount.Value;
            if (hardnessCurrentAmount.Value > hardnessMaxAmount.Value)
                hardnessCurrentAmount.Value = hardnessMaxAmount.Value;
        }
    }

    public void PlayerTapped()
    {
        if (isHardnessActive.Value)
        {
            isHardnessActive.Value = false;
            sfxToPlay.Raise(turningOffHardnessAudio);
            this.GetComponent<Renderer>().material = normalSphere;
        }
        else if (!isHardnessActive.Value && hardnessCurrentAmount.Value > 0)
        {
            isHardnessActive.Value = true;
            sfxToPlay.Raise(turningOnHardnessAudio);
            this.GetComponent<Renderer>().material = hardSphere;
        }
    }
}
