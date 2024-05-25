using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMechanic : MonoBehaviour
{
    float Speed=10;
    GameObject player;
    Rigidbody rb;
    int bouncing;
    public GameObject Shadow;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 shadowPos = new Vector3(transform.position.x,0, transform.position.z);
        Vector3 shadowScale = new Vector3(10/transform.position.y, 0.1f, 10/ transform.position.y);
        Shadow.transform.parent = null;
        Shadow.transform.localScale = shadowScale;
        Shadow.transform.position = shadowPos;
        transform.position = Vector3.MoveTowards(transform.position,player.transform.position, Time.deltaTime*Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            bouncing++;
            rb.AddForce(Vector3.up*20,ForceMode.Impulse);
            if (bouncing > 3) { Destroy(Shadow); Destroy(gameObject);}
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerGM>().TakeDamage(10);
            Destroy(Shadow); Destroy(gameObject);
        }
    }
}
