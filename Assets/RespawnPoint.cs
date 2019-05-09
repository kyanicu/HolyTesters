using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Transform res;

    void Awake() {
        res = this.transform;
        GameObject.FindGameObjectWithTag("Player").transform.position = res.position;
    }

}
