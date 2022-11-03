using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class valores : MonoBehaviour
{
    public  int valor;
    public GameObject manager;
    public Sprite oro;
    public Sprite cobre;
    public Sprite plata;
    Image imagen;
    public static bool acertare, perdere;


    void Start()
    {
        imagen = gameObject.GetComponent<Image>();

        int moneda = Random.Range(0, 3);
        switch (moneda) //Al empezar se le asignará una moneda random a cada gameobject para que no sea repetitivo
        {
            case 0:
                
                imagen.sprite = oro;
                break;
            case 1:
                imagen.sprite = cobre;
                break;
            case 2:
                imagen.sprite = plata;
                break;
        }
            
    }
    public void al_clicar() 
    {
        if (numeros.parateh == false)//condición de si se puede clicar o no
        {
            musica_numeros.romper_numeros = true;

            if (valor == numeros.elegido)//si al clicar acierta
            {
                numeros.numero_aciertos++;
                acertare = true;
            }
            else
            {
                if (numeros.numero_aciertos > 0)
                {
                    numeros.numero_aciertos--;

                }
                perdere = true;
            }
            numeros.acertar++;//En script numeros hará que se repita (aunque de forma random) el minijuego para que el jugador pueda seguir jugando
        }
        
    }

}
