using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
[UpdateAfter(typeof(InputSystem))]
public class TransformPlayerSystem : SystemBase {
    protected override void OnUpdate() {
        var dt = Time.DeltaTime;
        Entities.ForEach((ref Translation pos, in InputComponent direction, in SpeedComponent speed) => {
            pos.Value += new float3(direction.horizontal, direction.vertical, 0f) * dt * speed.speed;
        }
         ).Run();
    }
}
