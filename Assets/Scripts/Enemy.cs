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

    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
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

    void die() {
        this.gameObject.SetActive(false);
    }
}
