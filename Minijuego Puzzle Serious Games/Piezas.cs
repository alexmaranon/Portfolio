using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piezas : MonoBehaviour
{
    int Identificador;
    public bool Colisiones = false;

    public bool puedeMoverse = true;

    public bool BuenaPosi = false;
    Vector3 posicioninicial;
    bool tesaliste;
    public List<string> Propiedades = new List<string>();
    public bool Colisiona;
    public bool yaMovido = false;
   int twice = 0;
    bool respawned = false;

    public bool correcto = false;
    public bool enSitio = false;

    Vector2 Result;


    private void Start()
    {
        //Posición random para las piezas (como cuando en la vida real se tiran las piezas de la caja a la mesa para simular desorden)
        if (PuzzleManager.dificultad_puzzle == 1)
        {
            gameObject.transform.position = new Vector3(Random.Range(-5f, -1f), Random.Range(3f, 0f), gameObject.transform.position.z);
            
        }
        if (PuzzleManager.dificultad_puzzle == 2)
        {
            gameObject.transform.position = new Vector3(Random.Range(-5, -1), Random.Range(2, -2), gameObject.transform.position.z);
        }
        if (PuzzleManager.dificultad_puzzle == 3)
        {
         
            gameObject.transform.position = new Vector3(Random.Range(-6, 0), Random.Range(3, -2), gameObject.transform.position.z);
        }
        posicioninicial = gameObject.transform.position;
        puedeMoverse = true;
    }
    private void OnMouseDown()//Al clicar asignar la posición del ratón añadiendo un offset para que al clicar en la pieza esta no se teletransporte a la posición del ratón
    {
        if (puedeMoverse)
        {
            enSitio = false;
            Colisiones = false;
            Vector2 currentMouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 currentLocation = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Result = currentLocation - currentMouseLocation; //offset
        }
    }

    private void OnMouseDrag()
    {
        if (puedeMoverse)
        {
            Vector2 currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = currentTouchPos + Result; //Al mantener el click hacer que se mueva la pieza dependiendo de la posición del ratón y el offset previamente calculado
        }
    }

    private void OnMouseUp()//Al soltar
    {
        if (puedeMoverse)
        {
            Colisiones = true;
            twice = 0;
            if (tesaliste == true)//Si la pieza se sale de los límites vuelve a su posición original para no perderla
            {
                Respawn(gameObject, gameObject);
                tesaliste = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)//Al colocar la pieza
    {
        if (twice <=2)
        {
            if (collision.gameObject.tag == "Piezas" && collision.gameObject.GetComponent<Piezas>().enSitio && Colisiones == true)//Si ya hay una pieza puesta en ese lugar
            {
                collision.GetComponent<Piezas>().Respawn(collision.gameObject, gameObject);//Hacer que la pieza que ya estaba puesta vuelva a su posición inicial si no es correcta
                respawned = true;
            }
            if (collision.gameObject.tag == "posis" && respawned)
            {
                collision.GetComponent<Posiciones>().ocupada = false;
            }
            if (collision.gameObject.tag == "posis" && collision.GetComponent<Posiciones>().ocupada == false && !correcto)//Si no hay piezas puestas 
            {
                if (Colisiones == true)
                {
                    transform.position = collision.gameObject.transform.position;
                    collision.GetComponent<Posiciones>().visitante = gameObject;
                    yaMovido = true;
                    enSitio = true;
                    collision.GetComponent<Posiciones>().Check(this.gameObject);//Checkear si la pieza colocada es correcta
                }
            }
            if (collision.gameObject.tag == "Limite_puzzle")
            {
                tesaliste = true;
            }
            twice++;
        }      
    }
    private void OnBecameInvisible()
    {
        tesaliste = true;
        
    }
    
    public void Respawn(GameObject otraPieza, GameObject estaPieza)//Si la piza se sale del margen hacer que vuelva a posición original
    {
        if (otraPieza.GetComponent<Piezas>().correcto == false)
        {
            gameObject.transform.position = posicioninicial;
            gameObject.GetComponent<Piezas>().enSitio = false;
        }
        else
        {
            estaPieza.transform.position = estaPieza.GetComponent<Piezas>().posicioninicial;
            estaPieza.GetComponent<Piezas>().enSitio = false;
        }
    }
}
