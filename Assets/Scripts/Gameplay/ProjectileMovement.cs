using ScriptableObjectArchitecture;
using System.Collections;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private FloatReference bulletVelocity = default(FloatReference);
    [SerializeField] private FloatReference bulletTimeOfLife = default(FloatReference);
    [SerializeField] private ProjectileTypeGameEvent playerImpacted = default(ProjectileTypeGameEvent);
    [HideInInspector] public ProjectileType type;

    private void OnEnable()
    {
        StartCoroutine(AutoDestruction());
    }

    private void Update()
    {
        transform.position += transform.forward * bulletVelocity.Value * Time.deltaTime;
    }

    private void Destroy()
    {
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    private IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(bulletTimeOfLife.Value);
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
