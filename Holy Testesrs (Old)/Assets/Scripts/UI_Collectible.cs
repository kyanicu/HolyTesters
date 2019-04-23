using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Collectible : MonoBehaviour
{
    [SerializeField] private Text customText;
    [SerializeField] private Image customBackground;

    void OnObjectTriggerEnter(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            customText.enabled = true;
            customBackground.enabled = true;
            Debug.Log("Entered grail");
        }
    }
    void OnObjectTriggerExit(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            customText.enabled = false;
            customBackground.enabled = false;
            Debug.Log("Exit grail");
        }

    }
}
