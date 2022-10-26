using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    private Vector2 target;
    public static float speed = 0f;

    #region Bools que se activan al clicar en ciertos objetos para saber que estamos clicando
    public static bool interact = false;
    public static bool tercero = false;
    public static bool cuadros = false;
    #endregion
    float scale =0.01f;
    string sceneName;

    #region bools para interactuar en diferentes escenas
    public GameObject cuadro1;
    public GameObject cuadro2;
    public static bool noentraralinco=true;
    public static bool entrada;
    public static bool lobby;
    public static bool calle;
    public static bool teatro;
    public static bool callejuela;
    public static bool palco;
    public static bool pajar;
    public static bool callejuana;
    public static float contador=2f;
    public static float monedascontar = 0f;
    public static bool trileros=false;
    public bool saltos=false;
    public static bool opcion2 = false;
    public static bool volvertiempo = false;
    public GameObject toro;
    #endregion
    Animator anim;
    
    // Start is called before the first frame update

    void Start()
    {
       // anim = GetComponent<Animator>();

        StartCoroutine("salir");

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        //Guardar posicion en distintas salas
        if (sceneName == "Entrada" && entrada == true)
        {
            gameObject.transform.position = inretaction.guardarposientrada;
        }
        if (sceneName == "Lobby"&&lobby==true)
        {
            gameObject.transform.position = inretaction.guardarposi;
        }
        if (sceneName == "CalleLincoln" && calle==true)
        {
            gameObject.transform.position = inretaction.guardarposi1;
        }
        if (sceneName == "Teatro_Lincoln"&&teatro==true)
        {
            gameObject.transform.position = inretaction.guardarposi2;
        }
        if (sceneName == "callejon"&&callejuela==true)
        {
            gameObject.transform.position = inretaction.guardarposi3;
        }
        if (sceneName == "palco" && palco == true)
        {
            gameObject.transform.position = inretaction.guardarposi4;
        }
        if (sceneName == "Pajar" && palco == true)
        {
            gameObject.transform.position = inretaction.guardarposipajar;
        }
        if (sceneName == "callejuana" && callejuana == true)
        {
            gameObject.transform.position = inretaction.guardarposicalle;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "farola")
        {
            if (farolas.farola == true)
            {
                contador--;
                Debug.Log(contador);

            }
            if (contador == 0)
            {
                toro.GetComponent<BoxCollider2D>().enabled = false;
                toro.GetComponent<BoxCollider2D>().isTrigger = false;
               
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "monedita")
        {
            if (puzzlebu.romper == true && sceneName == "Teatro_Lincoln")
            {
                monedascontar++;
                
                Debug.Log(monedascontar);
            }
           
            if (monedascontar > 0)
            {
                trileros = true;

                Debug.Log(monedascontar);
            }

        }
    }
    IEnumerator salir()
    {
        //Setear velocidad para que el personaje no empiece con animación de caminar al entrar en nueva sala
        speed = 0;
        yield return new WaitForSeconds(0);
        speed = 3;

    }
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.B)&&volvertiempo==false) //Habilidad volver al tiempo actual (sala principal)
        {
            anim.SetBool("ph", true);

           
        }
        
        //Detectar que estamos pulsando (se activan en otro script al clicar en dichos objetos)
        if (Input.GetMouseButtonDown(0) && cuadros == false && interact == false 
            || Input.GetMouseButtonDown(0) && cuadros == false && interact == true 
            || Input.GetMouseButtonDown(0) && cuadros == true && interact == false)
        {
            saltos = true;
            if (sceneName == "Teatro_Lincoln"|| sceneName == "palco") //Salas en las que el movimiento es tanto en vertical como horizontal
            {
                Vector2 mousePosteatro = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target = new Vector2(mousePosteatro.x, mousePosteatro.y);
                speed = 2f;

              
            }
            if (sceneName != "Teatro_Lincoln"&& sceneName != "palco") //salas cuyo movimiento solo es posible en horizontal
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target = new Vector2(mousePos.x, transform.position.y);
                
                
            
            }           
            cuadros = false;
            interact = false;
            //Deseleccionar que estábamos clicando
        }
     
        if (Input.GetMouseButtonDown(0)&& cuadros==true &&interact == true) //Al clicar en objeto que activa el bool cuadro hacer que target (target es un vector que determina el destino del personaje) sea el centro del cuadro 
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector2(cuadro1.transform.position.x, transform.position.y);
            saltos = true;
        }
        if (Input.GetMouseButtonDown(0) && cuadros == true && tercero==true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector2(cuadro2.transform.position.x, transform.position.y);
            //speed = 3f;
            saltos = true;
        }
        if (saltos == true) //Movimiento del personaje
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);

        }
    }
}
