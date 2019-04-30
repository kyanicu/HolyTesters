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
    [SerializeField] private string state;
    // Start is called before the first frame update
    void Start()
    {
        state = "wander";
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "target":
                goToPlayer();
                break;
            case "wander":
                //Debug.Log("Wandering");
                agent.SetDestination(RandomNavmeshLocation(100f));
                break;
        }
        //Constantly follow player
        //agent.destination = player.transform.position;
        //Wander
        //agent.SetDestination(RandomNavmeshLocation(100f));
    }

    public void setState(string desiredState) {
        state = desiredState;
    }

    public string getState() {
        return state;
    }

    void goToPlayer() {
        agent.SetDestination(player.transform.position);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
