using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Created by Nick Tang 
    4/30/19
*/

public class AI_BasicAttack : MonoBehaviour
{

    [SerializeField] private float
        attackPower,
        pushSpeed;

    Enemy status;
    // Start is called before the first frame update
    void Start()
    {
        attackPower = 10;
        status = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Player_Status player = other.gameObject.GetComponent<Player_Status>();
            player.TakeDamage(attackPower);
            player.TakeKnockBack(new Vector2(transform.forward.x, transform.forward.z), pushSpeed);
            status.InitiateAttackCoolDown();
        }
    }
}
