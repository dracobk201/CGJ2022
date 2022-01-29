using ScriptableObjectArchitecture;
using UnityEngine;

public class GameSetter : MonoBehaviour
{
    [SerializeField] private BoolReference isGameOver = default(BoolReference);
    [SerializeField] private BoolReference isDarknessActive = default(BoolReference);
    [SerializeField] private IntReference initialPunishFrequency = default(IntReference);
    [SerializeField] private IntReference punishFrequency = default(IntReference);
    [SerializeField] private FloatReference initialMinSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference minSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference initialMaxSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference maxSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);
    [SerializeField] private FloatReference initialProjectileVelocity = default(FloatReference);
    [SerializeField] private FloatReference projectileVelocity = default(FloatReference);
    private int punishedTimes;

    private void Awake()
    {
        isGameOver.Value = false;
        isDarknessActive.Value = false;
        hardnessCurrentAmount.Value = 0;
        punishedTimes = 0;
        punishFrequency.Value = initialPunishFrequency.Value;
        projectileVelocity.Value = initialProjectileVelocity.Value;
        minSpawnTime.Value = initialMinSpawnTime.Value;
        maxSpawnTime.Value = initialMaxSpawnTime.Value;
    }

    public void GeneratePunish()
    {
        punishedTimes++;

        var punishProbability = Random.value;
        if (punishProbability <= 0.3f)
        {
            var minMaxProbability = Random.value;
            if (minMaxProbability <= 0.5f)
                minSpawnTime.Value -= minSpawnTime.Value * 0.05f;
            else
                maxSpawnTime.Value -= maxSpawnTime.Value * 0.05f;
        }
        else
        {
            float valueFactor = Random.Range(2, 10);
            projectileVelocity.Value += (valueFactor / 100);
        }
        
        if (punishedTimes % 5 == 0)
            punishFrequency.Value--;
    }

    public void DeccreaseFrecuency()
    {
        punishFrequency.Value--;
    }
}
