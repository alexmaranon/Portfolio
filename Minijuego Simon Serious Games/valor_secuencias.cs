using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class valor_secuencias : MonoBehaviour
{
    public int Valor_Secuencia;
    public SpriteRenderer SpriteRenders;
    public Sprite Light_on;
    public Sprite defaults;
    public Sprite fallar;
    public GameObject lose;
    public GameObject win;
    public GameObject esconder;
    public GameObject todo;
    public Text t_final;
    public AudioSource perder, acertar;
    private void Start()
    {
        SpriteRenders = gameObject.GetComponent<SpriteRenderer>();

    }
    public void Light_button()
    {
        SpriteRenders.sprite = Light_on;//Cambiar sprite a sprite colorido para mostrar que se ha activado
    }

    //MOVER TODO AL SCRIPT SECUENCIA
    private void OnMouseDown()
    {
        
        if (Secuencia.fin_de_secuencia == true)//Que no se pueda spamear
        {
            Secuencia.fin_de_secuencia = false;
            StartCoroutine("Al_clicar");
        }
        
    }
    IEnumerator Al_clicar()
    {
        //Al clicar identificar si la carta clicada es la correcta ya que cada carta tiene un valor y en la secuencia se recogen los valores de la lista
        if (Secuencia.inverso != true)
        {
            if (Valor_Secuencia == Secuencia.numeros[Secuencia.aumento])//comparar que valor de lista y valor clicado es verdad
            {

                SpriteRenders.sprite = Light_on;

                escalar();
                Secuencia.aumento++;//Aumenta la posicion de la lista para que al clicar nos pille la siguiente posicion
                Secuencia.numero_aciertos++;
                BD_Simon.puntos_simon = "" + Secuencia.numero_aciertos;
                acertar.Play();

            }
            else//si se equivoca
            {
                BD_Simon.puntos_simon = "" + Secuencia.numero_aciertos;

                SpriteRenders.sprite = fallar;
                perder.Play();
                escalar();

                Secuencia.numero_de_fallos--;//se le resta vida
                if (Secuencia.numero_de_fallos == 0)//si se queda sin vida
                {
                    
                    Secuencia.start_time = false;
                    Secuencia.fin_de_secuencia = false;
                    yield return new WaitForSeconds(1);
                    
                }
            }
        }
        
        if (Secuencia.inverso == true)//Lo mismo que arriba pero en orden (el checkear la secuencia) contrario para hacerlo inverso
        {
            if (Valor_Secuencia == Secuencia.numeros[Secuencia.aumento])
            {
                
                SpriteRenders.sprite = Light_on;
                acertar.Play();
                escalar();

                Secuencia.aumento--;
                Secuencia.numero_aciertos++;
                BD_Simon.puntos_simon = "" + Secuencia.numero_aciertos;
            }
            else
            {
                BD_Simon.puntos_simon = "" + Secuencia.numero_aciertos;

                SpriteRenders.sprite = fallar;

                escalar();
                perder.Play();

                Secuencia.numero_de_fallos--;
                if (Secuencia.numero_de_fallos == 0)
                {
                    
                    Secuencia.start_time = false;
                    Secuencia.fin_de_secuencia = false;
                    yield return new WaitForSeconds(1);
                    
                }
            }
        }

        //HACER QUE AL CLICAR HAYA MUY POCO DELAY
        Secuencia.fin_de_secuencia = true;
        yield return new WaitForSeconds(0.5f);
        //Al pasar un tiempo el tamaño y el sprite volverán al original
        switch (Secuencia.dificultad)
        {
            case 1:
                gameObject.transform.localScale = new Vector3(30, 30, 0);
                SpriteRenders.sortingOrder = 5;

                break;
            case 2:
                gameObject.transform.localScale = new Vector3(17, 19, 0);
                SpriteRenders.sortingOrder = 5;

                break;
            case 3:
                gameObject.transform.localScale = new Vector3(12, 13, 0);
                SpriteRenders.sortingOrder = 5;

                break;
            case 4:
                gameObject.transform.localScale = new Vector3(12, 13, 0);
                SpriteRenders.sortingOrder = 5;

                break;
        }

        SpriteRenders.sprite = defaults;
    }
    void escalar()//Hacer que al clicar se amplie la "carta" seleccionada / varía según la dificultad ya que el tamaño no es el mismo
    {
        switch (Secuencia.dificultad)
        {
            case 1:
                gameObject.transform.localScale = new Vector3(35, 35, 1);
                SpriteRenders.sortingOrder = 5;
                break;
            case 2:
                gameObject.transform.localScale = new Vector3(25, 25, 1);
                SpriteRenders.sortingOrder = 5;

                break;
            case 3:
                gameObject.transform.localScale = new Vector3(20, 20, 1);
                SpriteRenders.sortingOrder = 5;

                break;
            case 4:
                gameObject.transform.localScale = new Vector3(20, 20, 1);
                SpriteRenders.sortingOrder = 5;

                break;
        }
    }
  
    public void normal_button()
    {
        SpriteRenders.sprite = defaults;
    }
}
