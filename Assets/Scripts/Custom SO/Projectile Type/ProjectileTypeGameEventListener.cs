using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "ProjectileType Event Listener")]
    public sealed class ProjectileTypeGameEventListener : BaseGameEventListener<ProjectileType, ProjectileTypeGameEvent, ProjectileTypeEvent>
    {
    }
}