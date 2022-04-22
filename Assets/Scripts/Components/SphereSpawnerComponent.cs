using Unity.Entities;

[GenerateAuthoringComponent]
public struct SphereSpawnerComponent : IComponentData {
    public Entity sphere;
    public float speed;
}
