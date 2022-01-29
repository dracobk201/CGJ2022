using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpitter : MonoBehaviour
{
    [SerializeField] private GameObjectCollection enemies = default(GameObjectCollection);
    [SerializeField] private BoolReference isGameOver = default(BoolReference);
    [SerializeField] private FloatReference minSpawnTime = default(FloatReference);
    [SerializeField] private FloatReference maxSpawnTime = default(FloatReference);
    private float leftBorder;
    private float rightBorder;
    private float bottomBorder;

    private void Start()
    {
        StartCoroutine(AutomaticSpitter());

        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        bottomBorder = edgeVector.y * 2;
        float width = edgeVector.x * 2;
        leftBorder = -width/2;
        rightBorder = width/2;
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

        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                enemies[i].transform.localPosition = initialPosition;
                enemies[i].transform.localRotation = initialRotation;
                enemies[i].SetActive(true);
                break;
            }
        }
    }
}