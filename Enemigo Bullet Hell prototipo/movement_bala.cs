using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_bala : MonoBehaviour
{
    //PROTOTIPO ENEMIGO BULLET HELL (Prototipo rápido para proyecto)
    public GameObject caha;
    public int valor;
    int numeros;
    int random;
    public float x;
    public float speed_bola=5;
    // Start is called before the first frame update
    void Start()
    {
         random = Random.Range(0, 8);
        if (random != 2)
        {
            gameObject.transform.parent = caha.transform; //Hacer que el 88% de las balas spawneadas sean hijas del cubo para movimiento orbitar
        }
        
        StartCoroutine(despawn());

    }
    IEnumerator despawn()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        x = x + 0.007f;//aumentar el valor del seno para que vaya cambiando de valor y conseguir un movimiento ondulatorio Sen(x)
        
        if (valor == 0)//Hacer que las balas con valor 0 (valor proporcionado en script hell) tengan de direccion front
        {
            
            
            if (random == 2)
            {
                transform.Translate(new Vector3(-Mathf.Sin(x)*2, 0, 1) * Time.deltaTime * speed_bola);//el 12% de las balas que vayan en esta direccion tendran un movimiento ondulatorio
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed_bola);
            }
        }
        if (valor == 1)//Hacer que las balas con valor 1 (valor proporcionado en script hell) tengan de direccion back
        {

            if (random == 2)
            {
                transform.Translate(new Vector3(Mathf.Sin(x)*2, 0, -1) * Time.deltaTime * speed_bola);//el 12% de las balas que vayan en esta direccion tendran un movimiento ondulatorio
            }
            else
            {
                transform.Translate(Vector3.back * Time.deltaTime * speed_bola);
            }



        }
        if (valor == 2)//Hacer que las balas con valor 2 (valor proporcionado en script hell) tengan de direccion right
        {

            if (random == 2)
            {
                transform.Translate(new Vector3(1, 0, Mathf.Sin(x)*2) * Time.deltaTime * speed_bola);//el 12% de las balas que vayan en esta direccion tendran un movimiento ondulatorio
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed_bola);
            }

        }
        if (valor == 3)//Hacer que las balas con valor 3 (valor proporcionado en script hell) tengan de direccion left
        {
            if (random == 2)
            {
                transform.Translate(new Vector3(-1, 0, -Mathf.Sin(x)*2) * Time.deltaTime * speed_bola);//el 12% de las balas que vayan en esta direccion tendran un movimiento ondulatorio
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed_bola);
            }

        }
      
    }
}
