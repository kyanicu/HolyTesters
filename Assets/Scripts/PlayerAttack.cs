using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Created by Nick Tang 
    4/23/19
*/

public class PlayerAttack : MonoBehaviour
{
    public int playerPower; //How much damage the attack does
    public float pushSpeed; //How far the enemy is pushed after being hit
    public float hitLength; //Range of attack

    private GameObject enemy;
    private Rigidbody enemyRB;
    private RaycastHit hit;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            //Attack in direction of the player
            Vector3 forward = transform.forward;
            //Debug.DrawRay(transform.position, forward, Color.red, 5.0f);
            if (Physics.Raycast(transform.position, forward, out hit, hitLength)){
                
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    //Debug.Log("Hit Enemy");
                    enemy = hit.collider.gameObject;
                    //Enemy take damage
                    enemy.GetComponent<Enemy>().health -= playerPower;
                    Vector2 direction = new Vector2(transform.forward.x, transform.forward.z);
                    //Enemy gets pushed away from player
                    enemy.GetComponent<Enemy>().TakeKnockBack(direction, pushSpeed);
                }
            }
        }
    }    
}
