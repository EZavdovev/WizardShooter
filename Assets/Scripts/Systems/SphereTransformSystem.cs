using Unity.Entities;
using Unity.Transforms;
public class SphereTransformSystem : SystemBase {
    protected override void OnUpdate() {
        var dt = Time.DeltaTime;
        Entities.ForEach((ref Translation pos, in SphereVelocityComponent velocity) => {
            pos.Value += velocity.velocity * velocity.speed * dt;
        }
         ).Run();
    }
}
