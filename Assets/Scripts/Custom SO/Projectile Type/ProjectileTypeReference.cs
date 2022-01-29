using ScriptableObjectArchitecture;

[System.Serializable]
public class ProjectileTypeReference : BaseReference<ProjectileType, ProjectileTypeVariable>
{
    public ProjectileTypeReference() : base() { }
    public ProjectileTypeReference(ProjectileType value) : base(value) { }
}