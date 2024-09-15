using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GravSwapScript : MonoBehaviour
{
    private bool pickedUp;
    private void Start()
    {
        pickedUp = false;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {   
        if(collision.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E) && !pickedUp)
            {
                gameObject.transform.SetParent(collision.gameObject.transform);
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                pickedUp = true;
            }
            else if(Input.GetKeyDown(KeyCode.E) && pickedUp)
            {
                gameObject.transform.SetParent(null);
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                pickedUp = false;
            }
            
        }
    }
    
}
