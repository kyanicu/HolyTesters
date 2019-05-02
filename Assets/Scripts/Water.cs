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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            customText.text = "[E] Fill grail";
            customText.enabled = true;
            customBackground.enabled = true;

        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            customText.enabled = false;
            customBackground.enabled = false;
        }
    }
}
