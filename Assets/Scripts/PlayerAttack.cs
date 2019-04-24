using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Created by Nick Tang 4/23/19
*/

public class PlayerAttack : MonoBehaviour
{
    public int playerPower; //How much damage the attack does
    public float pushForce; //How far the enemy is pushed after being hit
    public float hitLength; //Range of attack

    private GameObject enemy;
    private Rigidbody enemyRB;
    private RaycastHit hit;
    
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            //Attack in direction of the player
            Vector3 forward = transform.TransformDirection(Vector3.forward) * hitLength;
            //Debug.DrawRay(transform.position, forward, Color.red, 5.0f);
            if (Physics.Raycast(transform.position, (forward), out hit)){
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    //Debug.Log("Hit Enemy");
                    enemy = hit.collider.gameObject;
                    enemyRB = enemy.GetComponent<Rigidbody>();
                    //Enemy take damage
                    enemy.GetComponent<Enemy>().health -= playerPower;
                    Vector3 direction = (transform.position - enemy.transform.position).normalized;
                    //Enemy gets pushed away from player
                    enemyRB.AddForce(-direction * pushForce);
                    
                }
            }
        }
    }

    
}
