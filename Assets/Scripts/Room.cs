using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Doorway[] doorways;
    public MeshCollider MeshCollider;
    public Bounds RoomBounds
    {
        get { Debug.Log(MeshCollider.name); return MeshCollider.bounds; }
        
    }
}
