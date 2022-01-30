using ScriptableObjectArchitecture;
using UnityEngine;

public class GameAdministrator : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private BoolReference isGameOver = default(BoolReference);
    [Header("Punish System")]
    [SerializeField] private BoolReference isHardnessActive = default(BoolReference);
    [SerializeField] private IntReference initialPunishFrequency = default(IntReference);
    [SerializeField] private IntReference punishFrequency = default(IntReference);
    [SerializeField] private FloatReference initialMinSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference minSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference initialMaxSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference maxSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);
    [SerializeField] private FloatReference initialProjectileVelocity = default(FloatReference);
    [SerializeField] private FloatReference projectileVelocity = default(FloatReference);
    [Header("Point System")]
    [SerializeField] private IntReference totalPoints = default(IntReference);
    [SerializeField] private IntReference hardnessProjectilePoints = default(IntReference);
    [SerializeField] private IntReference mortalProjectilePoints = default(IntReference);
    [SerializeField] private IntReference mortalProjectilesDestroyed = default(IntReference);
    [SerializeField] private FloatReference pointsPerSecond = default(FloatReference);
    [SerializeField] private FloatReference timePlayed = default(FloatReference);
    private int punishedTimes;

    private void Awake()
    {
        ResetValues();
    }

    private void Update()
    {
        if (isGameStarted.Value && !isGameOver.Value)
            timePlayed.Value += Time.deltaTime;
    }

    private void ResetValues()
    {
        isGameOver.Value = false;
        isHardnessActive.Value = false;
        hardnessCurrentAmount.Value = 0;
        punishedTimes = 0;
        punishFrequency.Value = initialPunishFrequency.Value;
        projectileVelocity.Value = initialProjectileVelocity.Value;
        minSpawnTime.Value = initialMinSpawnTime.Value;
        maxSpawnTime.Value = initialMaxSpawnTime.Value;
        totalPoints.Value = 0;
        timePlayed.Value = 0;
        mortalProjectilesDestroyed.Value = 0;
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

    public void DecreaseFrecuency()
    {
        punishFrequency.Value--;
    }

    public void IncreasePoints(ProjectileType type)
    {
        if (type.Equals(ProjectileType.Hardness))
            totalPoints.Value += hardnessProjectilePoints.Value;
        else if (type.Equals(ProjectileType.Mortal))
        {
            mortalProjectilesDestroyed.Value++;
            totalPoints.Value += mortalProjectilePoints.Value;
        }
    }

    public void IncreasePointsWithSeconds()
    {
        int points =  (int)(timePlayed.Value * pointsPerSecond.Value);
        totalPoints.Value = points;
    }
}
