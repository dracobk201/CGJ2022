using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] private GameObjectCollection enemies = default(GameObjectCollection);
    [SerializeField] private IntReference enemiesPool = default(IntReference);
    [SerializeField] private GameObject enemyPrefab = default(GameObject);

    private void Start()
    {
        enemies.Clear();
        InstantiateEnemy();
    }

    private void InstantiateEnemy()
    {
        for (int i = 0; i < enemiesPool.Value; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab) as GameObject;
            newEnemy.GetComponent<Transform>().SetParent(transform);
            enemies.Add(newEnemy);
            newEnemy.SetActive(false);
        }
    }
}
