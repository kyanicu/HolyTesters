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
    
    private bool isStunned;
    
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
                agent.SetDestination(RandomNavmeshLocation(100f));
                break;
        }

    }

    public void Stun()
    {
        isStunned = true;
        agent.isStopped = true;
    }

    public void Unstun()
    {
        isStunned = false;
        agent.isStopped = false;
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
