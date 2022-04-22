using Unity.Entities;

[GenerateAuthoringComponent]
public struct PlayerLiveComponent : IComponentData
{
    public bool isDead;
}
