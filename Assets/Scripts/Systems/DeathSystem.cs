using UnityEngine;
using Unity.Entities;
public class DeathSystem : SystemBase {

    private EndSimulationEntityCommandBufferSystem endSimulationEntity;

    protected override void OnCreate() {
        endSimulationEntity = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate() {
        var bufferSystem = endSimulationEntity.CreateCommandBuffer();
        var dt = Time.DeltaTime;

        Entities.ForEach((Entity enemy, ref HealthComponent health) => {
            if (health.health <= 0) {
                bufferSystem.DestroyEntity(enemy);
            }
        }).Run();

        Entities.ForEach((Entity sphere, ref SphereHealthComponent sphereHealth) => {
            sphereHealth.timeLife -= dt;
            if (sphereHealth.timeLife <= 0 || sphereHealth.isDestroyed == true) {
                bufferSystem.DestroyEntity(sphere);
            }
        }
         ).Run();

        Entities.ForEach((Entity player, ref PlayerLiveComponent playerHealth) => {
           
            if (playerHealth.isDead == true) {
                bufferSystem.DestroyEntity(player);
            }
        }
         ).Run();
    }
}
