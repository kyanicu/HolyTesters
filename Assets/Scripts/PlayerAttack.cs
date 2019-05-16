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

    [SerializeField]
    private float
        leftStart,
        leftTime,
        leftEnd,
        rightStart,
        rightTime,
        rightEnd,
        slamStart,
        slamTime,
        slamEnd;

    private Animator animator;

    public bool attacking;
    Coroutine attackCoroutine;

    private void Start()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !attacking){ //attackCoroutine == null) {

            attackCoroutine = StartCoroutine(LeftPunch());
            /*
            //Attack in direction of the player
            Vector3 forward = transform.forward;
            //Debug.DrawRay(transform.position, forward, Color.red, 5.0f);
            Physics.queriesHitTriggers = true;
            if (Physics.Raycast(transform.position + Vector3.up, forward, out hit, hitLength)){
                Debug.Log(hit.collider);
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
            Physics.queriesHitTriggers = false;
            */
        }
    }

    public IEnumerator LeftPunch()
    {

        animator.SetTrigger("LeftPunch");
        attacking = true;

        for (float timer = 0; timer < leftStart; timer += Time.deltaTime)
        {
            yield return null;
        }

        transform.GetChild(0).gameObject.SetActive(true);
        
        for (float timer = 0; timer < leftTime; timer += Time.deltaTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                transform.GetChild(0).gameObject.SetActive(false);
                yield return StartCoroutine(RightPunch());
                yield break;
            }
            yield return null;
        }

        transform.GetChild(0).gameObject.SetActive(false);

        for (float timer = 0; timer < leftEnd; timer += Time.deltaTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                yield return StartCoroutine(RightPunch());
                yield break;
            }
            yield return null;
        }

        attacking = false;

    }

    public IEnumerator RightPunch()
    {

        animator.SetTrigger("RightPunch");
        attacking = true;

        for (float timer = 0; timer < rightStart; timer += Time.deltaTime)
        {
            yield return null;
        }

        transform.GetChild(1).gameObject.SetActive(true);

        for (float timer = 0; timer < rightTime; timer += Time.deltaTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                transform.GetChild(1).gameObject.SetActive(false);
                yield return StartCoroutine(Slam());
                yield break;
            }
            yield return null;
        }

        transform.GetChild(1).gameObject.SetActive(false);

        for (float timer = 0; timer < rightEnd; timer += Time.deltaTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                yield return StartCoroutine(Slam());
                yield break;
            }
            yield return null;
        }

        attacking = false;

    }

    public IEnumerator Slam()
    {

        animator.SetTrigger("Slam");
        attacking = true;

        for (float timer = 0; timer < slamStart; timer += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }

        transform.GetChild(2).gameObject.SetActive(true);

        for (float timer = 0; timer < slamTime; timer += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }

        transform.GetChild(2).gameObject.SetActive(false);

        for (float timer = 0; timer < slamEnd; timer += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }

        attacking = false;

    }

    public void setPlayerPower(int power) {
        playerPower = power;
    }

    public int getPlayerPower() {
        return playerPower;
    }
}
