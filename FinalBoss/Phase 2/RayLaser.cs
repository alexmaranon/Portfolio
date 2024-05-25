using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLaser : MonoBehaviour
{
    public GameObject player;
    Quaternion Looking;
    public float rotSpeed;
    public GameObject bossGm;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (bossGm.GetComponent<FinalBoss>().PowerUp == 2)
        {
            bossGm.transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
        }
        else
        {
            Vector3 _fixRotar = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
            Looking = Quaternion.LookRotation(_fixRotar);
            transform.rotation = Quaternion.Slerp(transform.rotation, Looking, rotSpeed * Time.deltaTime);
        }
        
    }

}
