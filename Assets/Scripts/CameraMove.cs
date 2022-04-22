using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public class CameraMove : MonoBehaviour
{
    private Entity _player;
    private Translation _playerPos;
    private EntityManager _entityManager;

    [SerializeField]
    private Transform _mainCamera;
    [SerializeField]
    private float3 offset = float3.zero;

    void Start() {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }


    void LateUpdate() {

        if (_player == null) {
            return;
        }
        if (_entityManager.HasComponent<Translation>(_player)) {
            _playerPos = _entityManager.GetComponentData<Translation>(_player);
            _mainCamera.position = _playerPos.Value + offset;
        }
    }

    public void SetEntity(Entity entity) {
        _player = entity;
    }
}
