using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
public class TargetPlayerSystem : SystemBase {
    protected override void OnUpdate() {
        float3 targetPos = float3.zero;
        var dt = Time.DeltaTime;
        Entities.ForEach((in Translation pos, in PlayerTag player) => {
            targetPos = pos.Value;
        }
         ).Run();

        Entities.ForEach((ref Translation pos, in EnemyTag enemy) => {
            var velocity = targetPos - pos.Value;
            pos.Value += math.normalizesafe(velocity) * 5 * dt;
        }
         ).Run();

    }
}
