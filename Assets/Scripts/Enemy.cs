using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    Created by Nick Tang 
    4/23/19
*/

public class Enemy : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    public Slider healthBar;

    private bool _isStunned;
    public bool isStunned { get { return _isStunned; } private set { _isStunned = value; } }

    [SerializeField]
    private float
        kbDecel,
        kbStunTime,
        attackCoolDownTime;

    private ObjectMover mover;
    private EnemyNavAgent enemyNavAgent;


    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        mover = GetComponent<ObjectMover>();
        enemyNavAgent = GetComponent<EnemyNavAgent>();
    }

    void Update()
    {
        if (health <= 0) {
            die();
        }
    }

    void FixedUpdate() {
        healthBar.value = health;
    }

    public void takeDamage(int attackPower) {
        health -= attackPower;
    }

    public void TakeKnockBack(Vector2 direction, float speed)
    {
        StartCoroutine("KnockBack", direction * speed);
    }

    private IEnumerator KnockBack(Vector2 velocity)
    {
        Stun();

        for (float currSpeed = velocity.magnitude ; currSpeed > 0; currSpeed -= kbDecel * Time.fixedDeltaTime)
        {
            mover.Move(velocity.normalized * currSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }


        for (float timer = 0; timer < kbStunTime; timer += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }

        Unstun();

    }

    public void InitiateAttackCoolDown()
    {
        StartCoroutine("AttackCoolDown");
    }

    private IEnumerator AttackCoolDown()
    {
        Stun();

        for (float timer = 0; timer < attackCoolDownTime; timer += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }

        Unstun();

    }

    public void Stun()
    {
        isStunned = true;
        enemyNavAgent.Stun();
    }

    public void Unstun()
    {
        isStunned = false;
        enemyNavAgent.Unstun();
    }

    void die() {
        this.gameObject.SetActive(false);
    }
}
