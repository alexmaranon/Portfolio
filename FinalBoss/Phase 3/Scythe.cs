using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject Enemy;

    Vector3 goalPosition;
    float distance;
    bool isReturning;
    bool canReturn;

    float powerUpTime;
    float timer;
    [SerializeField]GameObject Bombs;
    bool stopBombs;
    Rigidbody rb;
    bool dontSpamDamage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        goalPosition = player.transform.position;
        


        Vector3 directorV = player.transform.position - transform.position;
        if (Enemy.GetComponent<FinalBoss>().PowerUp == 2)
        {
            powerUpTime = 1;
            rb.AddForce(directorV*0.5f, ForceMode.Impulse);
        }
            

    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        transform.Rotate(0, 500 * Time.deltaTime, 0);

        if (Enemy.GetComponent<FinalBoss>().PowerUp == 1)
        {
            
            distance = Mathf.Abs((goalPosition - transform.position).magnitude);
            transform.position = Vector3.MoveTowards(transform.position, goalPosition, Time.deltaTime * 10);
            if (distance < 1 && !isReturning)
            {
                isReturning = true;
                StartCoroutine(Return());
            }
            if (canReturn)
            {
                goalPosition = Enemy.transform.position;
            }
        }
        if(Enemy.GetComponent<FinalBoss>().PowerUp == 2)
        {
            if (timer > powerUpTime&&!stopBombs)
            {
                powerUpTime++;

                GameObject bombSpray=Instantiate(Bombs, transform.position, Quaternion.identity);
                bombSpray.SetActive(true);

            }


            if (timer > 4)
            {
                if (!stopBombs) {
                    Vector3 director = Enemy.transform.position - player.transform.position;   
                    
                    transform.position = -director*2;
                    dontSpamDamage = false;


                }
                canReturn = true;
                rb.velocity = Vector3.zero;
                transform.position = Vector3.MoveTowards(transform.position, Enemy.transform.position, Time.deltaTime * 20);

                stopBombs = true;
            }
        }



    }
    IEnumerator Return()
    {
        yield return new WaitForSeconds(3);
        canReturn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Enemigo"&& canReturn == true)
        {
            Destroy(gameObject);
        }
        if (dontSpamDamage == false && other.gameObject.tag == "Player")
        {
            Debug.Log("a");
            dontSpamDamage = true;
            other.gameObject.GetComponent<PlayerGM>().TakeDamage(20);
        }
    }

}
