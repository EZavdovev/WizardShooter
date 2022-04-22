using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class SpawnerSystem : SystemBase {
    private BeginSimulationEntityCommandBufferSystem _commandBufferSystem;
    private float _timer;
    protected override void OnCreate() {
        _commandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        _timer = 0;
    }

    protected override void OnStartRunning() {
        var commandBuffer = _commandBufferSystem.CreateCommandBuffer();
        Entities.ForEach((ref EnemyPrefabComponent enemyPrefab, in SpawnerSettingsComponent settings) => {
            var random = new Random(settings.randomSeed);
            for (int i = 0; i < settings.countEntities; i++) {
                var enemy = commandBuffer.Instantiate(enemyPrefab.enemyPrefab);
                var posForSpawn = random.NextUInt(0, 4);
                float x;
                float y;
                switch (posForSpawn) {
                    case 0 :
                        x = random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner);
                        y = random.NextFloat(settings.bordersSpawner, settings.bordersSpawner + settings.radiusBorder);
                        break;

                    case 1:
                        x = random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner);
                        y = random.NextFloat(-settings.bordersSpawner, -settings.bordersSpawner - settings.radiusBorder);
                        break;

                    case 2:
                        x = random.NextFloat(-settings.bordersSpawner, -settings.bordersSpawner - settings.radiusBorder);
                        y = random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner);
                        break;

                    default:
                        x = random.NextFloat(settings.bordersSpawner, settings.bordersSpawner + settings.radiusBorder);
                        y = random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner);
                        break;
                }
                 
                commandBuffer.SetComponent(enemy, new Translation { Value = new float3(x, y, 0f)});
            }
        }).Run();

    }
    protected override void OnUpdate() {
        var dt = Time.DeltaTime;
        _timer += dt;
        if (_timer > 5f) {
            _timer = 0f;
            var commandBuffer = _commandBufferSystem.CreateCommandBuffer();
            Entities.ForEach((ref EnemyPrefabComponent enemyPrefab, in SpawnerSettingsComponent settings) => {
                var random = new Random(settings.randomSeed);
                for (int i = 0; i < settings.countEntities; i++) {
                    var enemy = commandBuffer.Instantiate(enemyPrefab.enemyPrefab);
                    var posForSpawn = random.NextUInt(0, 4);
                    float x;
                    float y;
                    switch (posForSpawn) {
                        case 0:
                            x = random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner);
                            y = random.NextFloat(settings.bordersSpawner, settings.bordersSpawner + settings.radiusBorder);
                            break;

                        case 1:
                            x = random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner);
                            y = random.NextFloat(-settings.bordersSpawner, -settings.bordersSpawner - settings.radiusBorder);
                            break;

                        case 2:
                            x = random.NextFloat(-settings.bordersSpawner, -settings.bordersSpawner - settings.radiusBorder);
                            y = random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner);
                            break;

                        default:
                            x = random.NextFloat(settings.bordersSpawner, settings.bordersSpawner + settings.radiusBorder);
                            y = random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner);
                            break;
                    }

                    commandBuffer.SetComponent(enemy, new Translation { Value = new float3(x, y, 0f) });
                }
            }).Run();
        }
    }
}
