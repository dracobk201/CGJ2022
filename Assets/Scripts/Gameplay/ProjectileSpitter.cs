using ScriptableObjectArchitecture;
using System.Collections;
using UnityEngine;

public class ProjectileSpitter : MonoBehaviour
{
    [SerializeField] private GameObjectCollection mortalProjectiles = default(GameObjectCollection);
    [SerializeField] private GameObjectCollection hardnessProjectiles = default(GameObjectCollection);
    [SerializeField] private BoolReference isGameOver = default(BoolReference);
    [SerializeField] private FloatReference minSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference maxSpawnTime = default(FloatReference);
    private float leftBorder;
    private float rightBorder;
    private float bottomBorder;
    private ProjectileType lastProjectileType;

    private void Start()
    {
        lastProjectileType = ProjectileType.mortal;

        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        bottomBorder = edgeVector.y * 2;
        float width = edgeVector.x * 2;
        leftBorder = -width/2;
        rightBorder = width/2;

        StartCoroutine(AutomaticSpitter());
    }

    private IEnumerator AutomaticSpitter()
    {
        var time = Random.Range(minSpawnTime.Value, maxSpawnTime.Value);
        while (!isGameOver.Value)
        {
            if (time <= 0)
            {
                SpitEnemy();
                time = Random.Range(minSpawnTime.Value, maxSpawnTime.Value);
            }
            else
                time -= Time.deltaTime;
            yield return null;
        }
    }

    public void SpitEnemy()
    {
        var leftfactor = leftBorder * 0.1f;
        var rightfactor = rightBorder * 0.1f;
        var initialPosition = new Vector3(Random.Range(leftBorder - leftfactor, rightBorder - rightfactor), 0, 0);
        var targetPosition = new Vector3(Random.Range(leftBorder, rightBorder), -bottomBorder, 0);
        var direction = (targetPosition - initialPosition);
        var lookRotation = Quaternion.LookRotation(direction);
        var initialRotation = lookRotation;

        if (lastProjectileType.Equals(ProjectileType.hardness))
        {
            for (int i = 0; i < mortalProjectiles.Count; i++)
            {
                if (!mortalProjectiles[i].activeInHierarchy)
                {
                    mortalProjectiles[i].transform.localPosition = initialPosition;
                    mortalProjectiles[i].transform.localRotation = initialRotation;
                    mortalProjectiles[i].SetActive(true);
                    lastProjectileType = ProjectileType.mortal;
                    break;
                }
            }
        }
        else if (lastProjectileType.Equals(ProjectileType.mortal))
        {
            for (int i = 0; i < hardnessProjectiles.Count; i++)
            {
                if (!hardnessProjectiles[i].activeInHierarchy)
                {
                    hardnessProjectiles[i].transform.localPosition = initialPosition;
                    hardnessProjectiles[i].transform.localRotation = initialRotation;
                    hardnessProjectiles[i].SetActive(true);
                    lastProjectileType = ProjectileType.hardness;
                    break;
                }
            }
        }



    }
}

public enum ProjectileType
{
    mortal, hardness
}