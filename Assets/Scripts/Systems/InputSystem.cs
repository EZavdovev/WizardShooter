using Unity.Entities;
using UnityEngine;
public class InputSystem : SystemBase {
    protected override void OnUpdate() {
        Entities.ForEach((ref InputComponent direction) => {
            direction.horizontal = 0;
            direction.vertical = 0;
            if (Input.GetKey(KeyCode.W)) {
                direction.vertical = 1;
            }
            if (Input.GetKey(KeyCode.S)) {
                direction.vertical = -1;
            }
            if (Input.GetKey(KeyCode.A)) {
                direction.horizontal = -1;
            }
            if (Input.GetKey(KeyCode.D)) {
                direction.horizontal = 1;
            }
        }
        ).Run();
    }
}
