using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

public class SphereOnTriggerSystem : JobComponentSystem {

    private BuildPhysicsWorld _buildPhysicsWorld;
    private StepPhysicsWorld _stepPhysicsWorld;
    struct SphereOnTriggerSystemJob : ITriggerEventsJob {

        public ComponentDataFromEntity<SphereHealthComponent> sphereRef;
        public ComponentDataFromEntity<HealthComponent> enemyHealthRef;
        public void Execute(TriggerEvent triggerEvent) {
            Entity enemy, sphere;
            if (sphereRef.HasComponent(triggerEvent.EntityA)) {
                sphere = triggerEvent.EntityA;
                enemy = triggerEvent.EntityB;
            } else if (sphereRef.HasComponent(triggerEvent.EntityB)) {
                sphere = triggerEvent.EntityB;
                enemy = triggerEvent.EntityA;
            } else {

                return;
            }

            if (enemyHealthRef.HasComponent(enemy)) {
                var isDead = sphereRef[sphere];
                isDead.isDestroyed = true;
                sphereRef[sphere] = isDead;

                var health = enemyHealthRef[enemy];
                health.health -= 100;
                enemyHealthRef[enemy] = health;
            }
        }
    }

    struct PlayerOnTriggerSystemJob : ITriggerEventsJob {

        public ComponentDataFromEntity<PlayerLiveComponent> playerRef;
        public ComponentDataFromEntity<HealthComponent> enemyHealthRef;
        public void Execute(TriggerEvent triggerEvent) {
            Entity enemy, player;
            if (enemyHealthRef.HasComponent(triggerEvent.EntityB) && playerRef.HasComponent(triggerEvent.EntityA)) {
                player = triggerEvent.EntityA;
                enemy = triggerEvent.EntityB;
            } else if (enemyHealthRef.HasComponent(triggerEvent.EntityA) && playerRef.HasComponent(triggerEvent.EntityB)) {
                player = triggerEvent.EntityB;
                enemy = triggerEvent.EntityA;
            } else {

                return;
            }

            if (playerRef.HasComponent(player)) {
                var isDead = playerRef[player];
                isDead.isDead = true;
                playerRef[player] = isDead;
            }
        }
    }

    protected override void OnCreate() {
        _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        var jobSphere = new SphereOnTriggerSystemJob();
        var jobPlayer = new PlayerOnTriggerSystemJob();
        jobSphere.sphereRef = GetComponentDataFromEntity<SphereHealthComponent>(isReadOnly: false);
        jobSphere.enemyHealthRef = GetComponentDataFromEntity<HealthComponent>(isReadOnly: false);

        jobPlayer.playerRef = GetComponentDataFromEntity<PlayerLiveComponent>(isReadOnly: false);
        jobPlayer.enemyHealthRef = GetComponentDataFromEntity<HealthComponent>(isReadOnly: false);

        var jobResult = jobSphere.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, inputDeps);
        var jobResultPlayer = jobPlayer.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, jobResult);


        return jobResultPlayer;
    }
}
