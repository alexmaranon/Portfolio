using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Secuencia : MonoBehaviour
{
    public List<GameObject> Secuencia_facil = new List<GameObject>();
    public List<GameObject> Secuencia_medium = new List<GameObject>();
    public List<GameObject> Secuencia_hard = new List<GameObject>();

    public static List<int> numeros = new List <int>();
    public static int aumento;
    public int secuencias; //numero de veces que se ilumina
    public static bool fin_de_secuencia=false; //para no clicar mientras se iluminan
    public GameObject easy,medium,hard;
    public GameObject boton;
    public float time_between_sequence;
    public static int numero_aciertos;
    public int number_of_cards; //para seleccionar rango de cartas que se iluminan
    public float Total_time; 
    public Text time;
    public static int numero_de_fallos=3;
    public static bool start_time=false;
    public Text aciertos;
    public GameObject pause;
    public GameObject final;
    public Text t_final;
    public Text invers;
    public static int dificultad;
    public static bool inverso;
    public GameObject reverse;
    bool cerrar=false;
    public GameObject todo;
    public GameObject fula;
    public Animator anim;
    public GameObject ajuste;
    public GameObject esconder;
    public GameObject win;
    public Text turno;
    public AudioSource ganar;
    bool stoppls;
    public GameObject marco;
    void Start()
    {
        feedbackmanager.juego_feedback = "simon";//Para que la escena de feedback tenga (en otro script) el fondo de este minijuego
        start_time = false;
       
        switch (dificultad)//Setear valores y crear listas para hacer que la secuencia sea random y varíe según la dificultad
        {
            case 1:
                numero_de_fallos = 1;
                numeros.Clear();
                aumento = 0;
                numero_aciertos = 0;
                secuencias = 2;
                number_of_cards = 4;
                time_between_sequence = 2;
                for (int i = 0; i < secuencias; i++)
                {
                    int aleatorio = Random.Range(0, number_of_cards);
                    numeros.Add(Secuencia_facil[aleatorio].GetComponent<valor_secuencias>().Valor_Secuencia);
                }
                break;
            case 2:
                numero_de_fallos = 1;
                numero_aciertos = 0;
                aumento = 0;
                numeros.Clear();
                secuencias = 5;
                number_of_cards = 9;
                time_between_sequence = 1.3f;
                for (int i = 0; i < secuencias; i++)
                {
                    int aleatorio = Random.Range(0, number_of_cards);
                    numeros.Add(Secuencia_medium[aleatorio].GetComponent<valor_secuencias>().Valor_Secuencia);
                }
                break;
            case 3:
                numero_de_fallos = 1;
                numero_aciertos = 0;
                aumento = 0;
                numeros.Clear();
                secuencias = 7;
                number_of_cards = 16;
                time_between_sequence = 0.6f;
                for (int i = 0; i < secuencias; i++)
                {
                    int aleatorio = Random.Range(0, number_of_cards);
                    numeros.Add(Secuencia_hard[aleatorio].GetComponent<valor_secuencias>().Valor_Secuencia);
                }
                break;
            case 4:
                numero_de_fallos = 1;
                numero_aciertos = 0;
                numeros.Clear();
                inverso = true;
                secuencias = 7;
                aumento = 6;
                number_of_cards = 16;
                time_between_sequence = 0.6f;
                reverse.SetActive(true);
                for (int i = 0; i < secuencias; i++)
                {
                    int aleatorio = Random.Range(0, number_of_cards);
                    numeros.Add(Secuencia_hard[aleatorio].GetComponent<valor_secuencias>().Valor_Secuencia);
                }
                break;

        }
        time.text = ""+Mathf.Round(Total_time)+"  s";

        
    }
    public void start_button()//Botón de Start para empezar con el minijuego
    {

        switch (dificultad)
        {
            case 1:
                easy.SetActive(true);
                StartCoroutine("secuencia_facil");
                break;
            case 2:
                medium.SetActive(true);
                StartCoroutine("secuencia_medium");
                break;
            case 3:
                hard.SetActive(true);
                StartCoroutine("secuencia_hard");
                break;
            case 4:
                hard.SetActive(true);
                StartCoroutine("secuencia_hard");
                break;
        }
        turno.text = "Recuerda la secuencia";
        marco.SetActive(true);
        start_time = true;
        boton.SetActive(false);//Desactivar boton y videotutorial explicando el juego
        
    }
   
    IEnumerator secuencia_facil()//Hacer que siguiendo la lista creada en el start se cambien los sprites de las "cartas" en el orden que tiene que ser resuelto para indicar el orden
    {
        fin_de_secuencia = false;
        for (int i = 0; i < secuencias; i++)
        {
            yield return new WaitForSeconds(time_between_sequence);
            Secuencia_facil[numeros[i] - 1].GetComponent<valor_secuencias>().Light_button();
            yield return new WaitForSeconds(time_between_sequence);
            Secuencia_facil[numeros[i] - 1].GetComponent<valor_secuencias>().normal_button();
        }
        fin_de_secuencia = true;
        turno.text = "Tu turno";
    }
    IEnumerator secuencia_medium()//La diferencia es que coge la lista creada del valor medio
    {
        fin_de_secuencia = false;
        for (int i = 0; i < secuencias; i++)
        {
            yield return new WaitForSeconds(time_between_sequence);
            Secuencia_medium[numeros[i] - 1].GetComponent<valor_secuencias>().Light_button();
            yield return new WaitForSeconds(time_between_sequence);
            Secuencia_medium[numeros[i] - 1].GetComponent<valor_secuencias>().normal_button();
        }
        fin_de_secuencia = true;
        turno.text = "Tu turno";
    }
    IEnumerator secuencia_hard()
    {
        fin_de_secuencia = false;
        for (int i = 0; i < secuencias; i++)
        {
            yield return new WaitForSeconds(time_between_sequence);
            Secuencia_hard[numeros[i] - 1].GetComponent<valor_secuencias>().Light_button();
            yield return new WaitForSeconds(time_between_sequence);
            Secuencia_hard[numeros[i] - 1].GetComponent<valor_secuencias>().normal_button();
        }
        fin_de_secuencia = true;
        turno.text = "Tu turno";
    }
    public void resume()//Boton de resumir para la pausa
    {
        
        StartCoroutine(resumirse());

    }

    //Pensar si quitar update para optimizar
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&start_time==true && Settings.isOn == false)//Menu de escape en el que se deja de ver la secuencia y se para el tiempo para que no continue esta
        {
            if (cerrar == false)
            {
                switch (dificultad)
                {
                    case 1:
                        easy.SetActive(false);
                        break;
                    case 2:
                        medium.SetActive(false);
                        break;
                    case 3:
                        hard.SetActive(false);
                        break;
                    case 4:
                        hard.SetActive(false);
                        break;
                }
                Time.timeScale = 0;
                pause.SetActive(true);
                anim.Play("Anim_PauseOn");//Animación de aparicion para mejorar la estética de la pausa

                cerrar = true;
            }
            else
            {
                //Resumir
                ajuste.SetActive(false);
                StartCoroutine(resumirse());
            }
                       
        }
       

        if (start_time == true && numero_aciertos!=secuencias && fin_de_secuencia)//Timer
        {
            Total_time -= Time.deltaTime;
            time.text = ""+Mathf.Round(Total_time) + "  s";
            if (Total_time <= 0)//Si llega a 0 se guarda en un script para la base de datos el número de aciertos y se da como no resuelto el minijuego
            {
                BD_Simon.puntos_simon = "" + numero_aciertos;
                start_time = false;
                StartCoroutine("DELAYperder");

                //dirigir a perder
            }
        }
        aciertos.text = +numero_aciertos+"/"+secuencias;

        if (numero_aciertos == secuencias&&stoppls==false)//Si el numero de aciertos es igual al numero de la secuencia significa que ha seguido el patron correctamente y ha ganado
        {
            stoppls = true;

            StartCoroutine("DELAYOBAJANOTA");
             //dirigir a ganar
        }
        if (numero_de_fallos == 0 && stoppls == false)//Si se queda sin intentos (clica varias veces mal) pierde
        {
            stoppls = true;
            StartCoroutine("DELAYperder");
            //dirigir a perder
        }
    }
    IEnumerator resumirse()//Resumir
    {
        anim.Play("Anim_PauseOff");
        yield return new WaitForSecondsRealtime(1);
        switch(dificultad)
                {
                    case 1:
                        easy.SetActive(true);
            break;
                    case 2:
                        medium.SetActive(true);
            break;
                    case 3:
                        hard.SetActive(true);
            break;
                    case 4:
                        hard.SetActive(true);
            break;
        }
        Time.timeScale = 1;
        pause.SetActive(false);
        cerrar = false;
    }
    IEnumerator DELAYperder()
    {
        BD_Simon.puntos_simon = "" + numero_aciertos;
        fula.SetActive(true);

        feedbackmanager.win = false;
        feedbackmanager.lose = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Feedback_Escena");
    }
    IEnumerator DELAYOBAJANOTA()//Delay al ganar para que no salte directamente de escena
    {
        BD_Simon.puntos_simon=""+numero_aciertos;
        fula.SetActive(true);//Collider para que no se pueda clicar en escena

        feedbackmanager.win = true;
        feedbackmanager.lose = false;
        feedbackmanager.tiempo = Total_time /30;//Se calcula la puntuación para la escena de feedback
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Feedback_Escena");
    }

    public void CambiarEscena()//funcion de botón dependiendo del modo de juego Historia/Aventura
    {
        Time.timeScale = 1;
        
            SceneManager.LoadScene("LevelDif");
        
    }
    public void Salir()//función de botón Historia/Aventura
    {

        Time.timeScale = 1;

        feedbackmanager.tiempo = Total_time / 30;
        feedbackmanager.win = false;
        feedbackmanager.lose = true;
        SceneManager.LoadScene("Feedback_Escena");

    }
}
