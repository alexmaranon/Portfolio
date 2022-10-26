using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class disparar : MonoBehaviour
{
    public GameObject bola;
    float timeAux;
    Rigidbody2D rb;

    public int dados;
    public float caras;
    float bonus = 250f;
    //en vez de fuerza lo llamaré fuerzas
    float fuerzas = 200f;
    float probabilidad = 20f;


    public bool Ejercicio1;
    public bool Ejercicio2;
    public bool Ejercicio3;
    public bool Ejercicio4;
    public bool Ejercicio5;
    public bool Ejercicio6;
    public bool Ejercicio7;
    public bool Ejercicio8;
    public bool Ejercicio9;


    void Start()
    {
        timeAux = Time.time;



    }





    void Update()
    {

        float timeDif = Time.time - timeAux;

        if (timeDif > 2f)
        {
            //Instanciar objeto
            Vector3 pos = new Vector3(transform.position.x+1.5f, transform.position.y, transform.position.z);
            GameObject clon = Instantiate(bola, pos, Quaternion.identity) as GameObject;
            clon.SetActive(true);

            rb = clon.GetComponent<Rigidbody2D>();//Hacer que la variable rb sea igual al rigidbody del clon creado

            
            
            
            timeAux = Time.time;

            if (Ejercicio1==true)
            {
                P01FuerzaFija();
            }

            if (Ejercicio2==true)
            {
                P02RandomRange();
            }

            if (Ejercicio3==true)
            {
                P03RandomDosDados(caras);
            }

            if (Ejercicio4==true)
            {
                P04RandomVariosDados(dados, caras);
            }

            if (Ejercicio5==true)
            {
                P05maxDados(dados, caras);
            }

            if (Ejercicio6==true)
            {
                P06descatarMinDados(dados, caras);
            }

            if (Ejercicio7==true)
            {
                P07descatarMinYVolverATirar(dados, caras);
            }

            if (Ejercicio8==true)
            {
                P08descatarMaxYVolverATirar(dados, caras);
            }

            if (Ejercicio9==true)
            {
                P09PosibleBonus(fuerzas, bonus, probabilidad);
            }

        }


    }


    //FUNCIONES

    float P01FuerzaFija()
    {
        float fuerza = 500f;
        Debug.Log("La fuerza es de "+fuerza);

        Vector3 direccion = new Vector3(fuerza, 0f, 0f);
        rb.AddForce(direccion);

        return fuerza;
    }

    float P02RandomRange()
    {
        float fuerza = Random.Range(200f, 500f);
        Debug.Log("La fuerza es de "+fuerza);


        Vector3 direccion = new Vector3(fuerza, 0f, 0f);
        rb.AddForce(direccion);

        return fuerza;
    }

    float P03RandomDosDados(float caras)
    {
        float caras1 = Random.Range(0f, caras / 2);
        float caras2 = Random.Range(0f, caras / 2);
        float fuerza = caras1 + caras2;
        Debug.Log("La fuerza es de "+caras1+ " + " +caras2+ " que es= "+ fuerza );

        Vector3 direccion = new Vector3(fuerza, 0f, 0f);
        rb.AddForce(direccion);

        return fuerza;
    }

    float P04RandomVariosDados(int dados, float caras)
    {
       
        float fuerza = 0f;
        float cara = 0f;
        for (int i = 1; i <dados; i++)
        {
            cara = (Random.Range(0f, caras / dados) );
            fuerza = fuerza + cara;
            Debug.Log("El valor " + cara);
        }
        Debug.Log("La fuerza es de " + fuerza);


        Vector3 direccion = new Vector3(fuerza, 0f, 0f);
        rb.AddForce(direccion);

        return fuerza;
        

    }
    float P05maxDados(int dados, float caras)
     {
        float fuerza = 0f;
        
        for (int i = 0; i < dados; i++)
         {
            float valor = Random.Range(0f, caras);
             if (valor > fuerza)
             {
                  fuerza = valor;
             }

            Debug.Log("El valor es: " + valor);
        }   
        Debug.Log("La fuerza es de: " + fuerza);


        Vector3 direccion = new Vector3(fuerza, 0f, 0f);
        rb.AddForce(direccion);

        return fuerza;

    }
    float P06descatarMinDados(int dados, float caras)
    {
        
        float fuerza = 0f;
        float menor = 500f;
        float almacenar = 0f;
        //ponemos <= para que sea n+1 al añadir otro dato más
        for (int i = 0; i <= dados; i++)
        {
            float valor = Random.Range(0f, caras)/dados;
            almacenar = almacenar + valor;
            if (valor < menor)
            {
                menor = valor;
            }


            Debug.Log("el valor"+valor);
            
        }
        Debug.Log("el menor" + menor);
        fuerza = almacenar - menor;
        Debug.Log("La fuerza es= " + fuerza);

        


        Vector3 direccion = new Vector3(fuerza, 0f, 0f);
        rb.AddForce(direccion);

        return fuerza;

    }

    float P07descatarMinYVolverATirar(int dados, float caras)
    {
        float fuerza = 0f;
        float menor = 500f;
        float almacenar = 0f;
        float nuevodado = Random.Range(0f, caras);
        for (int i = 0; i < dados; i++)
        {
            float valor = Random.Range(0f, caras)/dados;
            almacenar = almacenar + valor;
            if (valor < menor)
            {
                menor = valor;
            }


            Debug.Log("el valor"+valor);

        }
        Debug.Log("el dado menor es= " + menor);
        fuerza = almacenar - menor + (nuevodado/dados);
        Debug.Log("El nuevo dado es= "+nuevodado/dados);
        Debug.Log("La fuerza es = " + fuerza);

        


        Vector3 direccion = new Vector3(fuerza, 0f, 0f);
        rb.AddForce(direccion);

        return fuerza;
    }


    float P08descatarMaxYVolverATirar(int dados, float caras)
    {
        float mayor = 0f;
        float fuerza = 0f;
        float almacenar = 0f;
        //ponemos <= para que sea n+1 al añadir otro dato más
        for (int i = 0; i <= dados; i++)
        {
            float valor = Random.Range(0f, caras);
            almacenar = almacenar + valor;
            if (valor > mayor)
            {
                mayor = valor;
            }
            //para ver los valores
            Debug.Log("El valor es de: " + valor);
        }
        fuerza = almacenar - mayor;
        Debug.Log("El mayor número es: " + mayor);
        Debug.Log("La fuerza es: " + fuerza);


        Vector3 direccion = new Vector3(fuerza, 0f, 0f);
        rb.AddForce(direccion);

        return fuerza;
    }



    float P09PosibleBonus(float fuerzas, float bonus, float probabilidad)
    {
        float prob = Random.Range(0f, 100f);
        float bonusdamage = 0f;

        if (prob <= probabilidad)
        {
            bonusdamage = fuerzas + bonus;
            Debug.Log("El impulso total con bonus fue= " + bonusdamage);

            Vector3 direccion = new Vector3(bonusdamage, 0f, 0f);
            rb.AddForce(direccion);
        }
        else
        {
            Debug.Log("El impulso total no tuvo bonus= " + fuerzas);

            Vector3 direccion = new Vector3(fuerzas, 0f, 0f);
            rb.AddForce(direccion);
        }



        

        return bonusdamage;
    }

}
