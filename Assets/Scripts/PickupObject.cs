// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class PickupObject : MonoBehaviour
// {
//     private Inventory inventory;
//     public GameObject itemButton;

//     public GameObject changeImage;
//     public Image grailSprite;

//     private void Start()
//     {
//         inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.tag == "Player")
//         {

//             for (int i = 0; i < inventory.slots.Length; i++)
//             {
//                 if (inventory.isFull[i] == false)
//                 {
//                     inventory.isFull[i] = true;
//                     changeImage = Instantiate(itemButton, inventory.slots[i].transform, false);

//                     changeImage.GetComponent<Image>();

//                     Destroy(gameObject);
//                     break;
//                 }
//             }
//             Debug.Log("picked up");
//             Destroy(gameObject);
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }
// }
