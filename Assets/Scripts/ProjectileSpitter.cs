using ScriptableObjectArchitecture;
using System.Collections;
using UnityEngine;

public class ProjectileSpitter : MonoBehaviour
{
    [SerializeField] private GameObjectCollection type1Projectiles = default(GameObjectCollection);
    [SerializeField] private GameObjectCollection type2Projectiles = default(GameObjectCollection);
    [SerializeField] private BoolReference isGameOver = default(BoolReference);
    [SerializeField] private FloatReference minSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference maxSpawnTime = default(FloatReference);
    private float leftBorder;
    private float rightBorder;
    private float bottomBorder;
    private ProjectileType lastProjectileType;

    private void Start()
    {
        lastProjectileType = ProjectileType.Type2;

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

        if (lastProjectileType.Equals(ProjectileType.Type2))
        {
            for (int i = 0; i < type1Projectiles.Count; i++)
            {
                if (!type1Projectiles[i].activeInHierarchy)
                {
                    type1Projectiles[i].transform.localPosition = initialPosition;
                    type1Projectiles[i].transform.localRotation = initialRotation;
                    type1Projectiles[i].SetActive(true);
                    lastProjectileType = ProjectileType.Type1;
                    break;
                }
            }
        }
        else if (lastProjectileType.Equals(ProjectileType.Type1))
        {
            for (int i = 0; i < type2Projectiles.Count; i++)
            {
                if (!type2Projectiles[i].activeInHierarchy)
                {
                    type2Projectiles[i].transform.localPosition = initialPosition;
                    type2Projectiles[i].transform.localRotation = initialRotation;
                    type2Projectiles[i].SetActive(true);
                    lastProjectileType = ProjectileType.Type2;
                    break;
                }
            }
        }



    }
}

public enum ProjectileType
{
    Type1, Type2
}