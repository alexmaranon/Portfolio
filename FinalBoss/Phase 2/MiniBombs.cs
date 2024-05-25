using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBombs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        StartCoroutine(Explosions());
    }

    IEnumerator Explosions()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
