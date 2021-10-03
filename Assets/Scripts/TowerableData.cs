using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerableData : MonoBehaviour
{
    public enum ColliderType
    {
        BOX, CAPSULE, SPHERE, MESH
    }

    public GameObject mainObject;
    public ColliderType colliderType;

    public Type GetColliderType() {
        Type type = null;
        switch (colliderType) {
            case ColliderType.BOX:
                type = typeof(BoxCollider);
                break;
            case ColliderType.CAPSULE:
                type = typeof(CapsuleCollider);
                break;
            case ColliderType.SPHERE:
                type = typeof(SphereCollider);
                break;
            case ColliderType.MESH:
                type = typeof(MeshCollider);
                break;
            default:
                break;
        }
        return type;
    }
}
