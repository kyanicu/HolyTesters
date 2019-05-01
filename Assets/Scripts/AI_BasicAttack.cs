using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Created by Nick Tang 
    4/30/19
*/

public class AI_BasicAttack : MonoBehaviour
{

    [SerializeField] private float attackPower;
    // Start is called before the first frame update
    void Start()
    {
        attackPower = 1;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<Player_Status>().takeDamage(attackPower);
        }
    }
}
