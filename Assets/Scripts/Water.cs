using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Created by Nick Tang 
    5/2/19
*/
public class Water : MonoBehaviour
{
    [SerializeField] private Text customText;
    [SerializeField] private Image customBackground;
    private string liquidName = "water";
    GameObject player;
    Player_Status status;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        status = player.GetComponent<Player_Status>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            customText.text = "[F] Fill grail";
            customText.enabled = true;
            customBackground.enabled = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Fill Grail"))
            {
                status.equippedLiquid = liquidName;
            }
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            customText.enabled = false;
            customBackground.enabled = false;
        }
    }

    
}
