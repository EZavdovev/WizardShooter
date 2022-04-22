using UnityEngine;
using Unity.Entities;

public class PlayerForCamera : MonoBehaviour,IConvertGameObjectToEntity
{
    [SerializeField]
    private CameraMove _followcamera;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
        _followcamera.SetEntity(entity);
    }

}
