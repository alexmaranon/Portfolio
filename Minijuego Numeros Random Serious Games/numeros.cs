using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class numeros : MonoBehaviour
{
    public static int dificultad; //ESTO IRÁ DENTRO DE BOTON YA QUE SELECCIONA LA DIFICULTAD. 9=EASY 30= MEDIO por lo que cuando se implemente cambiar a static
    public List<int> numeros_random = new List<int>();
    public List<int> numeros_elegidos = new List<int>();
    public List<Text> numeros_pantalla = new List<Text>();
    Color moneditas = new Vector4(0, 0, 0, 0.3f);
    public List<Text> facil_pantalla = new List<Text>();
    public List<Text> hard_pantalla = new List<Text>();
    public List<GameObject> facil_value = new List<GameObject>();
    public List<GameObject> hard_value = new List<GameObject>();
    public List<GameObject> add_value = new List<GameObject>();
    public static int elegido;
    public Text elegir;
    public GameObject dif_facil, dif_medio, dif_dificil;
    public Text aciertos;
    public Text mostrar_elegir;
    public Text tiempos;
    float Total_time; //tiempo no reset
    public static float guardar_tiempo;
    public static bool siguiente = false;
    public static bool time_on;
    public static int acertar;
    public static int numero_aciertos;
    bool cerrar = false;
    public GameObject pause;
    public GameObject final;
    public GameObject todo;
    public Text puntuacion;
    public Animator anim;
    bool pausarse;
    public GameObject ajuste;
    public GameObject empezar_button;
    public AudioSource perder, acertars,ganar;
    public static bool parateh;
    public GameObject marco;
    bool terminare;


    void Start()
    {
        feedbackmanager.juego_feedback = "numeros";//En la escena de feedback se le aplicará el fondo de este minijuego

        if (valores.acertare == true) //Debido a que al clicar a una moneda se recarga la escena para que vuelva a salir todo random en el start mostraremos si se ha equivocado o no
        {
            valores.acertare = false;
            acertars.Play();
        }
        if (valores.perdere == true)
        {
            valores.perdere = false;
            perder.Play();
        }

        elegir.text = "";
        mostrar_elegir.text = "";
        aciertos.text = "" + numero_aciertos;
        Time.timeScale = 1;
        
        acertar = 0;//Reseteamos para que no resetee la escena continuamente

        if (siguiente == true) //Al continuar jugando se guardará el tiempo para que no se resetee constantemente
        {
            marco.SetActive(true);
            Total_time = guardar_tiempo;

        }
        if (siguiente == false)//Si se acaba el minijuego se reseteará el tiempo para la siguiente partida
        {
            Total_time = 120;
        }
        tiempos.text = "" + Mathf.Round(Total_time) + "  s";


        for (int i = 0; i <= 99; i++) //Añadimos en la lista los valores del 1-99 debido a que debe encontrar un valor en específico (Especificado por la demandante)
        {
            numeros_random.Add(i);
        }
        if (time_on == true) //Al clicar el botón de empezar por primera vez se dejará activo para que no tenga que pulsar constantemente empezar para iniciar la corrutina
        {
            empezar_button.SetActive(false);
            StartCoroutine("mostrar");
        }
     
    }
    IEnumerator mostrar()
    {
        for (int i = 0; i < dificultad; i++) //Debido a que la dificultad es a su vez el número de "monedas" será a su vez el valor máximo de la lista que recorrer
        {
            int aleatorio = Random.Range(0, numeros_random.Count - 1);

            numeros_elegidos.Add(numeros_random[aleatorio]);//Selecciona los números randoms de la lista creada anteriormente con valores del 1-99
            numeros_random.RemoveAt(aleatorio);//Quitamos el número que ha tocado anteriormente para que no salga repetido

            switch (dificultad)
            {
                case 99: //Dificultad dificil con 99 monedas
                    hard_pantalla[i].text = "" + numeros_elegidos[i];//Asignamos a cada "Moneda"(GameObject) el valor random que hemos obtenido anteriormente
                    hard_pantalla[i].color = moneditas;//color para diferenciar del fondo

                    hard_value[i].GetComponent<valores>().valor = numeros_elegidos[i];//Asignamos los valores a cada GameObject (número random que llevan escritos)

                    float stature = Random.Range(0.9f, 1.4f);
                    hard_value[i].transform.localScale = new Vector3(stature, stature, stature);//Se pidió variar el tamaño en el nivel difícil y medio para que sea más difícil encontrar la moneda

                    hard_value[i].transform.localPosition = new Vector3(hard_value[i].transform.localPosition.x + Random.Range(-50, 20), hard_value[i].transform.localPosition.y + Random.Range(-10, 10), 0);
                    //También se pidió que estuviesen las monedas de tal forma que no parezcan estar ordenadas por lo que para que no se sobrepongan entre ellas variamos a mano la dispersión

                    break;

                case 9: //Dificultad fácil con 9 monedas
                    facil_pantalla[i].text = "" + numeros_elegidos[i];
                    facil_pantalla[i].color = moneditas;
                    facil_value[i].GetComponent<valores>().valor = numeros_elegidos[i];

                    facil_value[i].transform.localPosition = new Vector3(facil_value[i].transform.localPosition.x + Random.Range(-100, 100), facil_value[i].transform.localPosition.y + Random.Range(-50, 50), 0);

                    break;
                case 30: //Dificultad media con 30 monedas
                    numeros_pantalla[i].text = "" + numeros_elegidos[i];
                    numeros_pantalla[i].color = moneditas;

                    add_value[i].GetComponent<valores>().valor = numeros_elegidos[i];

                    stature = Random.Range(0.8f, 1.4f);
                    add_value[i].transform.localScale = new Vector3(stature, stature, stature);

                    add_value[i].transform.localPosition = new Vector3(add_value[i].transform.localPosition.x + Random.Range(-70, 70), add_value[i].transform.localPosition.y + Random.Range(-25, 25), 0);
                    break;



            }


       



        }
        int otro_random = Random.Range(0, dificultad - 1);
        elegido = numeros_elegidos[otro_random];//Número random seleccionado entre los números randoms obtenidos el cual será el número que tendrá que encontrar el jugador
        elegir.text = "Encuentra el " + elegido;
        mostrar_elegir.text = "Encuentra el " + elegido;

        yield return new WaitForSeconds(2);
        time_on = true;
        switch (dificultad)
        {

            case 9:
                dif_facil.SetActive(true);
                elegir.text = "";
                break;

            case 30:
                dif_medio.SetActive(true);
                elegir.text = "";
                break;

            case 99:
                dif_dificil.SetActive(true);
                elegir.text = "";
                break;
        }
        
        pausarse = true;

    }
    public void resumir()
    {

        ajuste.SetActive(false);
        StartCoroutine(resumirse());
    }
    public void empezar()
    {
        marco.SetActive(true);
        mostrar_elegir.text = "Encuentra el " + elegido;
        empezar_button.SetActive(false);
        StartCoroutine("mostrar");
    }
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape) && time_on == true && pausarse == true && Settings.isOn == false) //Menú de pausa
        {

            if (cerrar == false)
            {
                switch (dificultad)
                {

                    case 9:
                        dif_facil.SetActive(false);
                        break;

                    case 30:
                        dif_medio.SetActive(false);
                        break;

                    case 99:
                        dif_dificil.SetActive(false);
                        break;
                }
                Time.timeScale = 0;
                pause.SetActive(true);
                anim.Play("Anim_PauseOn");
                cerrar = true;
            }
            else
            {
                ajuste.SetActive(false);
                StartCoroutine(resumirse());
            }



        }

        if (time_on == true && acertar != 1 && Total_time > 0)//Timer
        {
            Total_time -= Time.deltaTime;
            tiempos.text = "" + Mathf.Round(Total_time) + "  s";
            if (Total_time <= 0)//Al llegar a 0 se acaba
            {
                BD_NumerosRandom.puntos_numeros_random = " " + numero_aciertos;
                siguiente = false;
                Terminar();
            }
        }
        if (acertar == 1)//Si ha clicado el jugador (no hace falta que acierte simplemente clicando en una moneda) se recargará la escena para volver a empezar con todos los valores guardados, como si continuase sin problemas
        {
            siguiente = true;
            guardar_tiempo = Total_time;
            SceneManager.LoadScene("numeros");
        }

    }
    IEnumerator resumirse()
    {
        anim.Play("Anim_PauseOff");
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        pause.SetActive(false);
        switch (dificultad)
        {

            case 9:
                dif_facil.SetActive(true);
                break;

            case 30:
                dif_medio.SetActive(true);
                break;

            case 99:
                dif_dificil.SetActive(true);
                break;
        }
        cerrar = false;
    }
    public void Replay()
    {

        Time.timeScale = 1;
        siguiente = false;
        numero_aciertos = 0;
        musica_numeros.romper_numeros = false;
        time_on = false;
        SceneManager.LoadScene("LevelDif");

    }

    public void Salir()
    {

        Time.timeScale = 1;
        siguiente = false;
        numero_aciertos = 0;
        musica_numeros.romper_numeros = false;
        time_on = false;




        switch (dificultad)//Al salir del minijuego guardar los resultados y llevarle a escena donde se mostrará la puntuación
        {
            case 9:

                feedbackmanager.tiempo = numero_aciertos / 20;
                if (feedbackmanager.tiempo >= 1)
                {
                    feedbackmanager.tiempo = 1;
                }
                break;
            case 30:
                feedbackmanager.tiempo = numero_aciertos / 10;
                if (feedbackmanager.tiempo >= 1)
                {
                    feedbackmanager.tiempo = 1;
                }
                break;
            case 99:
                feedbackmanager.tiempo = numero_aciertos / 5;
                if (feedbackmanager.tiempo >= 1)
                {
                    feedbackmanager.tiempo = 1;
                }
                break;
        }

        feedbackmanager.win = false;
        feedbackmanager.lose = true;
        SceneManager.LoadScene("Feedback_Escena");
    }

    public void Terminar()//Al terminar el minijuego
    {
       
        puntuacion.text = "Has conseguido: " + numero_aciertos + " puntos";

        time_on = false;
       

        
        StartCoroutine(DELAY());
    }
    IEnumerator DELAY()
    {
        switch (dificultad)//Se guardarán los resultados para la escena de feedback y se determinará si ha perdido o no
        {
            case 9:
                
                feedbackmanager.tiempo = numero_aciertos / 20;
                if (feedbackmanager.tiempo >= 1)
                {
                    feedbackmanager.tiempo = 1;
                }
                break;
            case 30:
                feedbackmanager.tiempo = numero_aciertos / 10;
                if (feedbackmanager.tiempo >= 1)
                {
                    feedbackmanager.tiempo = 1;
                }
                break;
            case 99:
                feedbackmanager.tiempo = numero_aciertos / 5;
                if (feedbackmanager.tiempo >= 1)
                {
                    feedbackmanager.tiempo = 1;
                }
                break;
        }
        if (numero_aciertos >= 1)
        {
            feedbackmanager.win = true;
            feedbackmanager.lose = false;
        }
        else {
            feedbackmanager.win = false;
            feedbackmanager.lose = true;
        }
        parateh = true;
        yield return new WaitForSeconds(1);
        musica_numeros.romper_numeros = false;
        numero_aciertos = 0;
        parateh = false;
        SceneManager.LoadScene("Feedback_Escena");
    }

    
}
