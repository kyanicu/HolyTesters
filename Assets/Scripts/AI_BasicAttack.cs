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
        pushSpeed,
        attackStart,
        attackTime,
        attackEnd;

    private float playerRangeTimer;

    Enemy status;
    Animator animator;
    EnemyNavAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        attackPower = 10;
        animator = transform.parent.GetComponentInChildren<Animator>();
        status = GetComponentInParent<Enemy>();
        nav = GetComponentInParent<EnemyNavAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");

            nav.Stun();
        }
    }

    void OnTriggerStay(Collider other) {

        if (other.CompareTag("Player"))
        {

            playerRangeTimer += Time.fixedDeltaTime;

            if (playerRangeTimer >= attackStart && playerRangeTimer < attackTime)
            {
                nav.Unstun();
                Player_Status player = other.gameObject.GetComponent<Player_Status>();
                player.TakeDamage(attackPower);
                player.TakeKnockBack(new Vector2(transform.forward.x, transform.forward.z), pushSpeed);
                status.InitiateAttackCoolDown();
                playerRangeTimer = attackTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nav.Unstun();
            playerRangeTimer = 0;
        }
    }
}
