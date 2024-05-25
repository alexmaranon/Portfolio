using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChaosMechanic : MonoBehaviour
{
    public GameObject mines;
    public List<GameObject> _minesExplosion;
    [SerializeField] GameObject vfxBomb;
    bool fixExplosion;
    void Start()
    {
        fixExplosion = false;
        StartCoroutine(Explosion());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&!fixExplosion)
        {
            fixExplosion = true;
            gameObject.GetComponent<Collider>().enabled = false;
            for (int i = 0; i < 8; i++)
            {
                this.transform.localEulerAngles = new Vector3(0, 360f / 8 * i, 0f);
                _minesExplosion.Add(Instantiate(mines, transform.position, Quaternion.identity));
                _minesExplosion[i].SetActive(true);
                _minesExplosion[i].GetComponent<Rigidbody>().velocity = transform.forward * 4f;
            }
            
            StartCoroutine(vfxEffect());
        }
        if (other.gameObject.CompareTag("void") && !fixExplosion)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            for (int i = 0; i < 8; i++)
            {
                this.transform.localEulerAngles = new Vector3(0, 360f / 8 * i, 0f);
                _minesExplosion.Add(Instantiate(mines, transform.position, Quaternion.identity));
                _minesExplosion[i].SetActive(true);
                _minesExplosion[i].GetComponent<Rigidbody>().velocity = transform.forward * 4;
            }
            fixExplosion = true;
            StartCoroutine(vfxEffect());
            Destroy(other.gameObject);
            
        }

    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2);
        if (!fixExplosion)
        {
            for (int i = 0; i < 8; i++)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                this.transform.localEulerAngles = new Vector3(0, 360f / 8 * i, 0f);
                _minesExplosion.Add(Instantiate(mines, transform.position, Quaternion.identity));
                _minesExplosion[i].SetActive(true);
                _minesExplosion[i].GetComponent<Rigidbody>().velocity = transform.forward * 4;
                
            }
            StartCoroutine(vfxEffect());
        }
        

    }
    IEnumerator vfxEffect()
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        if(gameObject.name== "Bomb(Clone)")
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        
        gameObject.GetComponent<Renderer>().enabled = false;
        vfxBomb.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
