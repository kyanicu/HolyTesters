using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavAgent : MonoBehaviour
{
    GameObject player;
    private Transform playerTransform;
    NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        //playerTransform = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = player.transform;
    }
}
