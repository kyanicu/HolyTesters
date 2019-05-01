using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    Created by Nick Tang 
    4/30/19
*/

public class AI_MoveRange : MonoBehaviour
{
    EnemyNavAgent parentAgent;
    // Start is called before the first frame update
    void Start()
    {
        parentAgent = this.gameObject.GetComponentInParent<EnemyNavAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            parentAgent.setState("target");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            parentAgent.setState("wander");
        }
    }
}
