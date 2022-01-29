using ScriptableObjectArchitecture;
using System.Collections;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private FloatReference projectileVelocity = default(FloatReference);
    [SerializeField] private FloatReference projectileTimeOfLife = default(FloatReference);
    [SerializeField] private ProjectileTypeGameEvent playerImpacted = default(ProjectileTypeGameEvent);
    [SerializeField] private GameEvent increasePunish = default(GameEvent);
    [HideInInspector] public ProjectileType type;

    private void OnEnable()
    {
        StartCoroutine(AutoDestruction());
    }

    private void Update()
    {
        transform.position += transform.forward * projectileVelocity.Value * Time.deltaTime;
    }

    private void Destroy()
    {
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    private IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(projectileTimeOfLife.Value);
        if (gameObject.activeInHierarchy)
            increasePunish.Raise();
        Destroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        string targetTag = other.tag;
        if (targetTag.Equals("Player"))
        {
            playerImpacted.Raise(type);
            Destroy();
        }
    }
}
