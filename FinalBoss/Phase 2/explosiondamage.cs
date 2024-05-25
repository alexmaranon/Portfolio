using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiondamage : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.GetComponent<PlayerGM>().TakeDamage(20);
        }

    }
}
