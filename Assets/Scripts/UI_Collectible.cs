using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Created by Nick Tang
    4/23/19
*/

public class UI_Collectible : MonoBehaviour
{
    [SerializeField] private Text customText;
    [SerializeField] private Image customBackground;

    private MeshRenderer mesh;

    void Start() {
        mesh = this.GetComponent<MeshRenderer>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            customText.text = this.name;
            customText.enabled = true;
            customBackground.enabled = true;
            StartCoroutine(FadeOut());
            mesh.enabled = false;
        }
        
    }
    
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2);
        // fade from opaque to transparent
        
            // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            customText.color = new Color(1, 1, 1, i);
            customBackground.color = new Color(1, 1, 1, i);
            yield return null;
        }
        
        customText.enabled = false;
        customBackground.enabled = false;
        Destroy(this.gameObject);

    }
}
