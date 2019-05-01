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
    [SerializeField] private Slider healthbar;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        healthbar.value = health;
    }

    public void setHealth(float amount) {
        health = amount;
    }
    public float getHealth() {
        return health;
    }

    public void takeDamage(float amount) {
        health -= amount;
    }
}
