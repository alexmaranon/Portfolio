using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientobarco : MonoBehaviour
{
    public Transform barquito;
    Rigidbody barcorb;
    public float potencia;
    public float potenciarota;
    public float potenciarota2;
    public float aceleracion=0.05f;
    public float velocidadmax=10f;
    public float velocidadmin = -10f;
    controlador controla;
    public bool empezar;
    public bool rotas;
    public bool rotasderecha;
    public GameObject agua;
    bool soltar;
    public bool nomemuevo;
    void Start()
    {
        barcorb = GetComponent<Rigidbody>();
        controla = GetComponent<controlador>();
    }

    void Update()
    {
        if (movimientobarco.desactivar == false&&movimientobarco.alvg==false)
        {
            user();
            updates();
        }
        
    }

    void user()
    {
        
        if (Input.GetKey(KeyCode.W))//Aceleración para movimiento haca delante hasta alcanzar velocidad máxima
        {
            if (controla.CurrentSpeed < 10 && potencia < velocidadmax)
            {
                potencia += 1f * aceleracion;
            }
            
        }
        else
        {
            if (controla.CurrentSpeed > 0)//Al no pulsar hacia delante perderá velocidad con el tiempo
            {
                potencia += -1f * aceleracion;
            }
        }
        if (Input.GetKey(KeyCode.A)&&transform.rotation.y>-0.20f) //Movimiento hacia la derecha con aceleración hasta alcanzar grado de rotación máximo
        {
            if (controla.Currentrot < 10 && potenciarota < velocidadmax)
            {

                potenciarota += 1f * aceleracion;
            }
        }
        else
        {
            if (controla.Currentrot < 0)//Volver a rotación original para seguir recto
            {
                potenciarota += -1f * aceleracion;//Al hacer que vuelva con aceleración oscilará de derecha a izquierda hasta estabilizarse

            }
        }
        if (Input.GetKey(KeyCode.D) && transform.rotation.y < 0.20f)
        {
            if (controla.Currentrot < 10 && potenciarota > velocidadmin)
            {
                potenciarota += -1f * aceleracion;
            }
        }
        else
        {            
            if (controla.Currentrot > 0)
            {
                potenciarota += 1f * aceleracion;
            }
        }
        if (potencia > 0.1)//Al tener velocidad
        {
            agua.SetActive(true);//Partículas de agua
        }
        if (potencia < 0.1)
        {
            agua.SetActive(false);
        }

    }
    void updates()
    {
        Vector3 forceToAdd = barquito.forward * potencia;
        //barcorb.AddForce(forceToAdd);
        barcorb.velocity = forceToAdd;


        Vector3 forcerota = -barquito.right * potenciarota;
        barcorb.AddForce (forcerota);
        

        transform.Rotate(-Vector3.up * Time.deltaTime * potenciarota);

    }
}
