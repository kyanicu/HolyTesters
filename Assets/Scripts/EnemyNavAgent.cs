using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    Created by Nick Tang
    4/25/19
*/
public class EnemyNavAgent : MonoBehaviour
{
    GameObject player; //The player object
    NavMeshAgent agent; //The object that will follow the player object
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly follow player
        agent.destination = player.transform.position;
    }
}
