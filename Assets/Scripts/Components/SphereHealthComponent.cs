using Unity.Entities;

[GenerateAuthoringComponent]
public struct SphereHealthComponent : IComponentData
{
    public bool isDestroyed;
    public float timeLife;
}
