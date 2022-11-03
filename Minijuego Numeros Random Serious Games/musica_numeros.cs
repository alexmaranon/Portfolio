using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musica_numeros : MonoBehaviour
{
    private AudioSource _audioSource;
    public static bool romper_numeros;
    private void Awake()//Script para que la m�sica de fondo no se reinicie pese a recargar la escena y est� constantemente en loop
    {

        DontDestroyOnLoad(transform.gameObject);

        _audioSource = GetComponent<AudioSource>();
        if (romper_numeros == true)//Si ya hay un gameobject con la m�sica y se recarga la escena con otro gameobject con m�sica, que el nuevo desaparezca debido a que est� en el awake y el antiguo no volver� a leerlo
        {
            Destroy(this.gameObject);
        }
        
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name!="numeros")//Si el jugador sale de esta escena
        {
            Destroy(this.gameObject);
        }
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
