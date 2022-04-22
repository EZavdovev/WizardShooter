using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct SphereVelocityComponent : IComponentData {
    public float3 velocity;
    public float speed;
}
