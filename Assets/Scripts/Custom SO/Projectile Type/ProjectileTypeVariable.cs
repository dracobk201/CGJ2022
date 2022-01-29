using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

[System.Serializable]
public class ProjectileTypeEvent : UnityEvent<ProjectileType> { }
[CreateAssetMenu(
        fileName = "ProjectileTypeVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Other/ProjectileType")]
public sealed class ProjectileTypeVariable : BaseVariable<ProjectileType, ProjectileTypeEvent>
{
}