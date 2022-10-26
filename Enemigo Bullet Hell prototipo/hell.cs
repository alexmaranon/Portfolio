using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hell : MonoBehaviour
{
    //PROTOTIPO ENEMIGO BULLET HELL (Prototipo rápido para proyecto)
    public GameObject bola;
    bool time_shoot=true;
    bool recarga;
    public static int x;
    int y = 0;
    public float timer_spawn;
    public List<GameObject> lados_bolas = new List<GameObject>();
    float rotar=20;
    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        lados_bolas.Clear();
        timer_spawn = 1;
        StartCoroutine(randoms());

    }


    void Update()
    {
        transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime * 4); //Rotar el gameobject para que las balas tengan un movimiento orbitante
        if (time_shoot==true)
        {
            StartCoroutine("spawntime");
        }
    }
    
    
    IEnumerator spawntime()
    {
         time_shoot = false;

        yield return new WaitForSeconds(timer_spawn);
        //creamos 4 bolas a la vez (cada una irá en una dirección en otro script)
        lados_bolas.Add(Instantiate(bola, bola.transform.position, Quaternion.identity));
        lados_bolas.Add(Instantiate(bola, bola.transform.position, Quaternion.identity));
        lados_bolas.Add(Instantiate(bola, bola.transform.position, Quaternion.identity));
        lados_bolas.Add(Instantiate(bola, bola.transform.position, Quaternion.identity));
        lados_bolas[x].SetActive(true);
        lados_bolas[x+1].SetActive(true);
        lados_bolas[x+2].SetActive(true);
        lados_bolas[x+3].SetActive(true);
        //a cada bola le damos un valor ya que en otro script dependiendo del valor que tenga se moverá en una dirección distinta
        lados_bolas[x].GetComponent<movement_bala>().valor =0;
        lados_bolas[x+1].GetComponent<movement_bala>().valor = 1;
        lados_bolas[x+2].GetComponent<movement_bala>().valor = 2;
        lados_bolas[x+3].GetComponent<movement_bala>().valor = 3;


        x=x+4; //aumentamos el valor de X para que al activar los gameobjects se activen las siguientes 4 bolas
        time_shoot = true;

        yield return new WaitForSeconds(7);
        //al pasar 7 segundos las 4 primeras bolas de la lista desaparecen
        lados_bolas.RemoveAt(y);
        lados_bolas.RemoveAt(y);
        lados_bolas.RemoveAt(y);
        lados_bolas.RemoveAt(y);
        x = x - 4; //al eliminar los 4 primeros huecos de la lista reducimos el valor de x ya que todos los gameobjects de la lista retroceden 4 valores y se activen correctamente los nuevos
    }

    IEnumerator randoms()
    {
      
        while (1 < 2)
        {
            //Cada 5 "segundos" haremos que la velocidad de spawneo de las balas cambie 
            yield return new WaitForSeconds(5);
            timer_spawn = Random.Range(0.1f,1f);
            //Cada 10 segundos el boss generará un ataque muy complejo en el que empezará a spamear ataques
            yield return new WaitForSeconds(5);
            timer_spawn = 0f;
        }
    }


}
