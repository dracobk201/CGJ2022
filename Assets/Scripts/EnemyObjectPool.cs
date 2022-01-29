using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] private GameObjectCollection type1Projectiles = default(GameObjectCollection);
    [SerializeField] private GameObjectCollection type2Projectiles = default(GameObjectCollection);
    [SerializeField] private IntReference projectilesPool = default(IntReference);
    [SerializeField] private GameObject projectileType1Prefab = default(GameObject);
    [SerializeField] private GameObject projectileType2Prefab = default(GameObject);
    [SerializeField] private Transform type1Container = default(Transform);
    [SerializeField] private Transform type2Container = default(Transform);

    private void Start()
    {
        type1Projectiles.Clear();
        type2Projectiles.Clear();
        InstantiateProjectiles();
    }

    private void InstantiateProjectiles()
    {
        for (int i = 0; i < projectilesPool.Value; i++)
        {
            GameObject newProjectile1 = Instantiate(projectileType1Prefab) as GameObject;
            GameObject newProjectile2 = Instantiate(projectileType2Prefab) as GameObject;
            newProjectile1.GetComponent<Transform>().SetParent(type1Container);
            newProjectile2.GetComponent<Transform>().SetParent(type2Container);
            type1Projectiles.Add(newProjectile1);
            type2Projectiles.Add(newProjectile2);
            newProjectile1.SetActive(false);
            newProjectile2.SetActive(false);
        }
    }
}
