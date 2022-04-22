using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
public class ShootingSystem : SystemBase
{

    private BeginSimulationEntityCommandBufferSystem _commandBufferSystem;
    protected override void OnCreate() {
        _commandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate() {

        var bufferSystem = _commandBufferSystem.CreateCommandBuffer();
        if (!Input.GetKey(KeyCode.Mouse0)) {
            return;
        }
        var centre = new float3(Screen.width / 2, Screen.height / 2, 0f);
        Entities.ForEach((in SphereSpawnerComponent sphere, in LocalToWorld staffPos) => {
            var newSphere = bufferSystem.Instantiate(sphere.sphere);
            bufferSystem.SetComponent<Translation>(newSphere, new Translation { Value = staffPos.Position });
            var velocitySphere = new float3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            velocitySphere -= centre;
            bufferSystem.SetComponent<SphereVelocityComponent>(newSphere, new SphereVelocityComponent { velocity = math.normalizesafe(velocitySphere), speed = sphere.speed });
        }
         ).Run();
    }

}
