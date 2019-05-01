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

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        maxHealth = 100;
        healthRegenSpeed = 2;
        healthRegenAmount = 3;
        isRegenHealth = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (health != maxHealth && !isRegenHealth)
        {
            StartCoroutine(regainHealthOverTime());
        }

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

    public void takeDamage(float amount)
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
