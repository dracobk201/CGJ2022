using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "ProjectileTypeGameEvent.asset",
        menuName = SOArchitecture_Utility.GAME_EVENT + "ProjectileType",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 2)]
    public sealed class ProjectileTypeGameEvent : GameEventBase<ProjectileType>
    {
    }
}