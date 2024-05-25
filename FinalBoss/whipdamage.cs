using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whipdamage : MonoBehaviour
{
    bool daño = false;
    bool cdInvulnerabilidad = false;

    private void OnEnable()
    {
        cdInvulnerabilidad = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (cdInvulnerabilidad == false && other.gameObject.tag == "Player")
        {
            cdInvulnerabilidad = true;
            other.gameObject.GetComponent<PlayerGM>().TakeDamage(20);
        }

    }



}
