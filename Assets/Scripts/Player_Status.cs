using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Created by Nick Tang 
    4/30/19
*/

public class Player_Status : MonoBehaviour
{
    [SerializeField] private double health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(double amount) {
        health = amount;
    }
    public double getHealth() {
        return health;
    }

    public void takeDamage(double amount) {
        health -= amount;
    }
}
