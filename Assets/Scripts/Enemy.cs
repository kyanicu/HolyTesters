using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Created by Nick Tang 
    4/23/19
*/

public class Enemy : MonoBehaviour
{
    public int health;

    void Update()
    {
        if (health <= 0) {
            die();
        }
    }

    public void takeDamage(int attackPower) {
        health -= attackPower;
    }

    void die() {
        this.gameObject.SetActive(false);
    }
}
