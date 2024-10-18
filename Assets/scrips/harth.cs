using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harth : MonoBehaviour
{
    private playerControler playerScript;
    private bool interectable;
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && interectable)
        {
            playerScript.AddHealth();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            interectable = true;
            playerScript =collider.gameObject.GetComponent<playerControler>();
        }
    }

     void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
             
            interectable = false;
        }
    }
}
