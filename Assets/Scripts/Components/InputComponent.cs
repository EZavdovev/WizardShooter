using Unity.Entities;

[GenerateAuthoringComponent]
public struct InputComponent : IComponentData
{
    public float horizontal;
    public float vertical;
}
