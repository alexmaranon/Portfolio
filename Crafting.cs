using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{

    public bool Topacio = false;
    public bool Polvora = false;
    public bool Nieve = false;
    public bool Agua = false;
    public bool Naranja = false;
    public bool Zanahoria = false;
    public bool Huesos = false;
    public bool Obsidiana = false;

    public GameObject aparese;
    public GameObject Huesoh;
    public GameObject Obsidianah;
    public GameObject zanah;
    public GameObject narah;
    public GameObject topa;
    public GameObject polvorilla;
    public GameObject PocionDeFuego;
    public GameObject PocionDeOscuridad;
    public GameObject PocionMala;
    public GameObject PocionDeHielo;
    public GameObject PocionDeVida;
    public GameObject Oscuro;
    public GameObject fuegito;
    public GameObject ice;
    public GameObject Vida;
    public GameObject erronea;
    public Manager mn;
    public GameObject calderooscuro;
    public GameObject caldero57;
    public GameObject calderovida;
    public bool masobsi;
    public bool mashueso;
    public bool maszana;
    public bool masnara;
    public bool mastopa;
    public bool maspolv;
    //Combinaciones para las Pociones
    #region COMBINACIONES
    private void Start()
    {
        mn = FindObjectOfType<Manager>();
    }
    void Update()
    {
        if (mn.obsidiana <= 0)//Controlar que solo haya uno en la mesa
        {
            masobsi = true;
        }
        if (mn.obsidiana > 0 && masobsi == true)
        {
            masobsi = false;
            Vector3 posiobsidiana = new Vector3(18.565f, 0.554f, -1.501f);
            aparese = Instantiate(Obsidianah, posiobsidiana, Quaternion.identity);
            
        }


        if (mn.huesos <= 0)
        {
            mashueso = true;
        }
        if (mn.huesos > 0 && mashueso == true)
        {
            mashueso = false;
            Vector3 posihueso = new Vector3(19.297f, 0.445f, -1.704f);
            aparese = Instantiate(Huesoh, posihueso, Quaternion.identity);
            
        }


        if (mn.zanahoria <= 0)
        {
            maszana = true;
        }
        if (mn.zanahoria > 0 && maszana == true)
        {
            maszana = false;
            Vector3 posizanahoria = new Vector3(18.386f, 0.4412f, -1.4f);
            aparese = Instantiate(zanah, posizanahoria, Quaternion.identity);
            
        }

        if (mn.fresa <= 0)//La fresa fue cambiada por naranja
        {
            masnara = true;
        }
        if (mn.fresa > 0 && masnara == true)
        {
            masnara = false;
            Vector3 posinaranja = new Vector3(18.525f, 0.432f, -1.27f);
            aparese = Instantiate(narah, posinaranja, Quaternion.identity);
            
        }

        if (mn.topaceo <= 0)
        {
            mastopa = true;
        }
        if (mn.topaceo > 0 && mastopa == true)
        {
            mastopa = false;
            Vector3 positopaceo = new Vector3(18.364f, 0.4564f, -0.973f);
            aparese = Instantiate(topa, positopaceo, Quaternion.identity);
            
        }


        if (mn.polvora <= 0)
        {
            maspolv = true;
        }
        if (mn.polvora > 0 && maspolv == true)
        {
            maspolv = false;
            Vector3 posipolvora = new Vector3(18.241f, 0.4564f, -1.2686f);
            aparese = Instantiate(polvorilla, posipolvora, Quaternion.identity);
            
        }





        if (Topacio == true || Polvora == true)//Al añadir al caldero vacío
        {
            if (Polvora == true & Topacio == true)//Al añadir al caldero con un objeto el correspondiente, se genera poción
            {
                StartCoroutine(PozionDeFuego());
            }

            else if (Huesos == true || Obsidiana == true || Nieve == true || Agua == true || Naranja == true || Zanahoria == true)//Al equivocarse con la receta        
            {
                StartCoroutine(PozionMala());
            }

        }
        if (Huesos == true || Obsidiana == true)
        { 
            if (Huesos == true && Obsidiana == true)
            {
                StartCoroutine(PozionDeOscuridad());
            }

            else if (Nieve == true || Topacio == true || Polvora == true || Naranja == true || Zanahoria == true || Agua == true)
            {
                StartCoroutine(PozionMala());
            }
                
        }
       
        if (Agua == true || Nieve == true)
        {
            if (Agua == true && Nieve == true)
            {
                StartCoroutine(PozionDeHielo());
            }

            else if (Topacio == true || Polvora == true || Obsidiana == true || Huesos == true || Zanahoria == true || Naranja == true)
            {
                StartCoroutine(PozionMala());
            }

        }

        if (Naranja == true || Zanahoria == true)
        {
            if (Naranja == true & Zanahoria == true)
            {
                StartCoroutine(PozionDeVida());
            }

            else if (Topacio == true || Polvora == true || Obsidiana == true || Huesos == true || Agua == true || Nieve == true)
            {
                StartCoroutine(PozionMala());
            }

        } 
       
       
    }
    #endregion 

    //Añadir Ingredientes
    #region INGREDIENTES
    void OnCollisionEnter(Collision collision)//Si en el inventario tenemos más de un ingrediente (pero límite de la mesa es 1 por ingrediente) y lo vertemos al caldero, generar otro y restar del inventario
    {
        if (collision.gameObject.tag == "Topacio")
        {
            Topacio = true;
            mn.topaceo -= 1;
            if (mn.topaceo > 0)
            {
                Vector3 positopaceo = new Vector3(18.364f, 0.4564f, -0.973f);
                aparese = Instantiate(topa, positopaceo, Quaternion.identity);
            }
            
            Destroy(collision.gameObject);
            
        }

        else if (collision.gameObject.tag == "Huesos")
        {
            Huesos = true;
            mn.huesos -= 1;
            if (mn.huesos > 0)
            {
                Vector3 aguacates = new Vector3(19.297f, 0.445f, -1.704f);
                aparese = Instantiate(Huesoh, aguacates, Quaternion.identity);
            }
            Destroy(collision.gameObject);
            
        }

        else if (collision.gameObject.tag == "Polvora")
        {
            Polvora = true;
            mn.polvora -= 1;
            if (mn.polvora > 0)
            {
                Vector3 aguacateh = new Vector3(18.241f, 0.4564f, -1.2686f);
                aparese = Instantiate(polvorilla, aguacateh, Quaternion.identity);
            }
            
            Destroy(collision.gameObject);
            
        }

        else if (collision.gameObject.tag == "Obsidiana")
        {
            Obsidiana = true;
            mn.obsidiana -= 1;
            if (mn.obsidiana > 0)
            {
                Vector3 aguacate = new Vector3(18.565f, 0.554f, -1.501f);
                aparese = Instantiate(Obsidianah, aguacate, Quaternion.identity);
            }
            
            Destroy(collision.gameObject);
            
        }

        else if (collision.gameObject.tag == "Nieve")
        {
            Nieve = true;
            Destroy(collision.gameObject);
            mn.nieve -= 1;
        }

        else if (collision.gameObject.tag == "Agua")
        {
            Agua = true;
            Destroy(collision.gameObject);
            mn.agua -= 1;
        }

        else if (collision.gameObject.tag == "Zanahoria")
        {
            Zanahoria = true;
            
            mn.zanahoria -= 1;
            if (mn.zanahoria > 0)
            {
                Vector3 aguacatej = new Vector3(18.386f, 0.4412f, -1.4f);
                aparese = Instantiate(zanah, aguacatej, Quaternion.identity);
            }
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Naranja")
        {
            Naranja = true;
            
            mn.fresa -= 1;
            if (mn.fresa > 0)
            {
                Vector3 aguacateh = new Vector3(18.525f, 0.432f, -1.27f);
                aparese = Instantiate(narah, aguacateh, Quaternion.identity);
            }
            Destroy(collision.gameObject);
        }

    }
    #endregion 

    //Corutinas donde se hacen las pociones
    #region RECETAS
    IEnumerator PozionDeFuego()
    {
        
        Topacio = false;
        Polvora = false;
        caldero57.SetActive(true);
        yield return new WaitForSeconds(3f);
        caldero57.SetActive(false);
        fuegito = Instantiate(PocionDeFuego, new Vector3(20.707f, 0.376f, -0.415f), Quaternion.identity);

        Debug.Log("Pocion de Fuego Creada");
    }

    IEnumerator PozionDeOscuridad()
    {
        Huesos = false;
        Obsidiana = false;
        calderooscuro.SetActive(true);
        yield return new WaitForSeconds(3f);

        Oscuro = Instantiate(PocionDeOscuridad,new Vector3(20.707f,0.376f, -0.415f),Quaternion.identity);
        PocionDeOscuridad.SetActive(true);
        calderooscuro.SetActive(false);
        Debug.Log("Pocion Oscura Creada");

    }

    IEnumerator PozionDeVida()
    {
        Zanahoria = false;
        Naranja = false;
        calderovida.SetActive(true);
        yield return new WaitForSeconds(3f);
        calderovida.SetActive(false);
        Vida = Instantiate(PocionDeVida, new Vector3(20.707f, 0.376f, -0.415f), Quaternion.identity);

        Debug.Log("Pocion de Vida Creada");
    }

    IEnumerator PozionDeHielo()
    {
        Agua = false;
        Nieve = false;

        yield return new WaitForSeconds(3f);

        ice = Instantiate(PocionDeHielo, new Vector3(20.707f, 0.376f, -0.415f), Quaternion.identity);

        Debug.Log("Pocion de Hielo Creada");
    }
     
    IEnumerator PozionMala()
    {

        Topacio = false;
        Polvora = false;
        Huesos = false;
        Obsidiana = false;
        Agua = false;
        Nieve = false;
        Naranja = false;
        Zanahoria = false;

        yield return new WaitForSeconds(3f);

        erronea = Instantiate(PocionMala, new Vector3(20.707f, 0.376f, -0.415f), Quaternion.identity);


    }

    #endregion 

}