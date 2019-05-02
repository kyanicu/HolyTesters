using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    Created by Nick Tang 
    4/30/19
*/

public class Player_Status : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    private bool isRegenHealth;
    [SerializeField] private Slider healthbar;
    [SerializeField] private float healthRegenSpeed;
    [SerializeField] private float healthRegenAmount;

    PlayerController playCont;
    private ObjectMover mover;

    [SerializeField]
    private float
        kbDecel,
        kbStunTimer;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        maxHealth = 100;
        healthRegenSpeed = 2;
        healthRegenAmount = 3;
        isRegenHealth = false;

        playCont = GetComponent<PlayerController>();
        mover = GetComponent<ObjectMover>();

    }

    // Update is called once per frame
    void Update()
    {

        if (health != maxHealth && !isRegenHealth)
        {
            StartCoroutine(regainHealthOverTime());
        }

    }

    public void TakeKnockBack(Vector2 direction, float speed)
    {
        StartCoroutine("KnockBack", direction * speed);
    }

    public IEnumerator KnockBack(Vector2 velocity)
    {
        playCont.Stun();

        for (float currSpeed = velocity.magnitude; currSpeed > 0; currSpeed -= kbDecel * Time.fixedDeltaTime)
        {
            mover.Move(velocity.normalized * currSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }


        for (float timer = 0; timer < kbStunTimer; timer += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }

        playCont.Unstun();

    }

    void FixedUpdate()
    {
        healthbar.value = health;
    }

    public void setHealth(float amount)
    {
        health = amount;
    }
    public float getHealth()
    {
        return health;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void regenHealth()
    {
        if (health < maxHealth) {
            health += healthRegenAmount;
        }
    }

    private IEnumerator regainHealthOverTime()
    {
        isRegenHealth = true;
        while (health < maxHealth)
        {
            regenHealth();
            yield return new WaitForSeconds(healthRegenSpeed);
        }
        isRegenHealth = false;
    }
}
