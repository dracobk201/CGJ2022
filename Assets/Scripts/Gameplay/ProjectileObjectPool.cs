using UnityEngine;
using ScriptableObjectArchitecture;

public class ProjectileObjectPool : MonoBehaviour
{
    [SerializeField] private GameObjectCollection mortalProjectiles = default(GameObjectCollection);
    [SerializeField] private GameObjectCollection hardnessProjectiles = default(GameObjectCollection);
    [SerializeField] private IntReference projectilesPool = default(IntReference);
    [SerializeField] private GameObject mortalProjectilePrefab = default(GameObject);
    [SerializeField] private GameObject hardnessProjectilePrefab = default(GameObject);
    [SerializeField] private Transform mortalContainer = default(Transform);
    [SerializeField] private Transform hardnessContainer = default(Transform);

    private void Start()
    {
        mortalProjectiles.Clear();
        hardnessProjectiles.Clear();
        InstantiateProjectiles();
    }

    private void InstantiateProjectiles()
    {
        for (int i = 0; i < projectilesPool.Value; i++)
        {
            GameObject mortal = Instantiate(mortalProjectilePrefab) as GameObject;
            GameObject hardness = Instantiate(hardnessProjectilePrefab) as GameObject;
            mortal.GetComponent<Transform>().SetParent(mortalContainer);
            hardness.GetComponent<Transform>().SetParent(hardnessContainer);
            mortal.GetComponent<ProjectileMovement>().type = ProjectileType.Mortal;
            hardness.GetComponent<ProjectileMovement>().type = ProjectileType.Hardness;
            mortalProjectiles.Add(mortal);
            hardnessProjectiles.Add(hardness);
            mortal.SetActive(false);
            hardness.SetActive(false);
        }
    }
}
