using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
   
    void OnObjectTriggerEnter(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false) {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
            Debug.Log("picked up");
            Destroy (gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
