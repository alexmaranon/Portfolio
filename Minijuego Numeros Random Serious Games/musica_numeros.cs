using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musica_numeros : MonoBehaviour
{
    private AudioSource _audioSource;
    public static bool romper_numeros;
    private void Awake()//Script para que la música de fondo no se reinicie pese a recargar la escena y esté constantemente en loop
    {

        DontDestroyOnLoad(transform.gameObject);

        _audioSource = GetComponent<AudioSource>();
        if (romper_numeros == true)//Si ya hay un gameobject con la música y se recarga la escena con otro gameobject con música, que el nuevo desaparezca debido a que está en el awake y el antiguo no volverá a leerlo
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
