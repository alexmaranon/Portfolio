using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FinalBoss : MonoBehaviour
{
    FinalBossStates CurrentState;
    NavMeshAgent agent;
    Animator anim;
    public GameObject player;
    public int PowerUp=1;//Al rellenar la barra de almas
    public int Fase = 1;//Fase actual. va por % de vidas

    [Header("Rotation")]
    bool cantRotate;
    public float rotSpeed;
    Quaternion Looking;

    [Header("Ball Attack")]
    public GameObject Ball;
    public bool BallOn;


    [Header("Whip Attack")]
    public bool WhipOn;
    public GameObject[] WhipAttackRange;
    bool cantSpamWhip;
    public bool iswhiping;
    int currentAttack;

    [Header("Death Ray Attack")]
    public GameObject deathRayCast,poweUpRay;
    public bool deathRayOn;
    bool cantSpamDeathRay;

    [Header("Chaos Bombs")]
    [SerializeField] GameObject BombChaos;
    public bool bombChaosOn;
    public bool fixxxxxxx;

    [Header("Scythe attack")]
    [SerializeField] GameObject Scythe;
    public bool ScytheAttackOn;
    bool cantSpamScythe;

    [Header("Hammer attack")]
    [SerializeField] GameObject Hammer;
    [SerializeField] GameObject VFXHammer;
    public bool HammerAttackOn;
    bool cantSpamHammer;

    [Header("CrossBow attack")]
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject ArrowArea;
    public bool CrossBowAttackOn;
    bool cantSpamCrossBow;

    [Header("Souls")]
    [SerializeField] Slider soulSlider;
    [SerializeField] float timeToReach;
    public bool endAttack;
    public float Souls;
    public bool AbsorbingSouls;
    bool isDead;
    bool absorbingRotation=false;
    float timer;
    bool reachedFirstUpgrade;
    bool cantSpamShield;
    [SerializeField] GameObject Shield;
    GameObject shieldToBreak;
    [SerializeField] GameObject ShieldCanvas;

    public GameObject desaparecer, efecto;

    void Start()
    {
        //We assign the variables to the states machine
        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        
        soulSlider.value = 0;
        
    }
    public void startall()
    {
        CurrentState = new Follow(this.gameObject, agent, anim, player);
    }

    
    void Update()
    {
        //Prevent the enemy from rotating while absorbing souls or if he is throwing the scythe
        if (endAttack&&!absorbingRotation|| !absorbingRotation&&cantSpamScythe) 
        {
            Vector3 _fixRotar = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
            Looking = Quaternion.LookRotation(_fixRotar);
            transform.rotation = Quaternion.Slerp(transform.rotation, Looking, rotSpeed * Time.deltaTime);
        }
        if (CurrentState != null) { CurrentState = CurrentState.Process(); }
        

        //Whip
        if (WhipOn == true&&!cantSpamWhip) { cantSpamWhip = true; StartCoroutine(WhipAttack()); endAttack = false; }
        #region Jumping ball
        //Powered Jumping Ball
        if (PowerUp == 2&& BallOn == true)
        {
            endAttack = false;
            BallOn = false;
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y+10, transform.position.z);
            for (int i = 0; i < 3; i++)
            {
                //Spawn balls
                Instantiate(Ball, spawnPoint, Quaternion.identity);
            }
        }
        //Jumping Ball
        else if(BallOn == true)
        {
            endAttack = false;
            BallOn = false;
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y+20, transform.position.z);
            Instantiate(Ball, spawnPoint, Quaternion.identity);
        }
        #endregion

        //Bomb
        if (bombChaosOn == true&&!fixxxxxxx)
        {
            fixxxxxxx = true;
            endAttack = false;
            bombChaosOn = false;
            BombBulletHell();
        }

        //DeathRay
        if (deathRayOn&&!cantSpamDeathRay)
        {
            endAttack = false;
            cantSpamDeathRay = true;
            StartCoroutine(deathRay());
        }
        //Hammer
        if (HammerAttackOn&&!cantSpamHammer)
        {
            GetComponent<Collider>().enabled = false;
 
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            agent.SetDestination(transform.position);
            endAttack = false;
            cantSpamHammer = true;
            
            
        }
        //Scythe
        if (ScytheAttackOn && !cantSpamScythe)
        {
            endAttack = false;
            cantSpamScythe = true;
            StartCoroutine(ScytheAttack());
        }
        //CrossBow
        if (CrossBowAttackOn && !cantSpamCrossBow)
        {
            anim.SetBool("IsCrossBow", true);
            endAttack = false;
            cantSpamCrossBow = true;
            StartCoroutine(CrossBowAttack());
        }
        //Absorbe Souls state
        if (timer > timeToReach&&Souls<=1&&!AbsorbingSouls&&endAttack)
        {
            AbsorbingSouls = true;
            absorbingRotation = true;
            CurrentState = new Follow(this.gameObject, agent, anim, player);
            GetComponent<CapsuleCollider>().enabled = false;
            StopAllCoroutines();

        }
        if (transform.position.x == 0 && transform.position.z == 0&&AbsorbingSouls)
        {
            anim.SetBool("IsAbsorbing", true);
            Souls = Souls + Time.deltaTime / 20;
            soulSlider.value = Souls;

            if (!cantSpamShield)
            {
                cantSpamShield = true;
                ShieldCanvas.SetActive(true);
                shieldToBreak = Instantiate(Shield, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
                
                shieldToBreak.SetActive(true);
            }
            
        }
        else if(!AbsorbingSouls) { timer = timer + Time.deltaTime; }
        if (Souls >1&&!reachedFirstUpgrade) { GetComponent<CapsuleCollider>().enabled = true; reachedFirstUpgrade = true; PowerUp = 2; efecto.SetActive(true); timer = 0; AbsorbingSouls = false; cantSpamShield = false; Destroy(shieldToBreak); ShieldCanvas.SetActive(false); anim.SetBool("IsAbsorbing", false); absorbingRotation = false; }
        if (shieldToBreak!=null&&AbsorbingSouls) 
        {
            if(shieldToBreak.GetComponent<IA_STATS>().vida <= 0) { GetComponent<CapsuleCollider>().enabled = true; Destroy(shieldToBreak); timer = 0; AbsorbingSouls = false; cantSpamShield = false; ShieldCanvas.SetActive(false); anim.SetBool("IsAbsorbing", false); absorbingRotation = false; }

        }

        //Fases
        if (GetComponent<IA_STATS>().vida / GetComponent<IA_STATS>().vidaMax <= 0.66f&& GetComponent<IA_STATS>().vida / GetComponent<IA_STATS>().vidaMax > 0.33f)
        {
            Fase = 2;
            
        }
        if (GetComponent<IA_STATS>().vida / GetComponent<IA_STATS>().vidaMax <= 0.33f)
        {
            Fase = 3;
        }
        if (GetComponent<IA_STATS>().vida <= 0&&!isDead)
        {
            GameObject.Find("activateCanvas").GetComponent<Bestiario>().GetKill("MASTER");
            isDead = true;
            FindObjectOfType<TestingInputSystem>().canceled = false;
            FindObjectOfType<TestingInputSystem>().canAttack = true;
            MagiaCuración.curando = false;
            absorbingRotation = true;
            
            agent.SetDestination(gameObject.transform.position);
            CurrentState = new BossDeath(this.gameObject, agent, anim, player);
            StopAllCoroutines();
            StartCoroutine(killBoss());
        }

    }

    IEnumerator killBoss()
    {
        yield return new WaitForSeconds(2);
        GameObject pof= Instantiate(desaparecer, transform.position, Quaternion.identity);
        pof.SetActive(true);
        agent.enabled = false;
        transform.position = new Vector3(0,-200,0);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Creditos");
    }
    public void resetAnimSpeed()
    {
        anim.speed = 1;
    }
    IEnumerator CrossBowAttack()
    {
        int arrowCount;
        agent.SetDestination(transform.position);
        if (PowerUp == 1) { arrowCount = 1; } else { arrowCount = 2; }
        yield return new WaitForSeconds(2.5f);
        for (int i = 0; i < arrowCount; i++)
        {

            Vector3 Posiciones = new Vector3((player.transform.position.x + Random.Range(-1, 1)), 0, player.transform.position.z + Random.Range(-1, 1));
            GameObject area = Instantiate(ArrowArea, Posiciones, Quaternion.identity);
            area.SetActive(true);

            Vector3 arrowPosi = new Vector3(area.transform.position.x, 20, area.transform.position.z);
            GameObject arrow = Instantiate(Arrow, arrowPosi, Quaternion.identity);
            arrow.SetActive(true);
            arrow.GetComponent<Rigidbody>().AddForce(-transform.up * 10, ForceMode.Impulse);

            anim.SetBool("IsWalking", true);
            anim.SetBool("IsCrossBow", false);
            cantSpamCrossBow = false;
            CrossBowAttackOn = false;
            endAttack = true;
            yield return new WaitForSeconds(3f);
        }

    }

    IEnumerator ScytheAttack()
    {
        GameObject ActiveScythe = Instantiate(Scythe, new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z), Quaternion.identity);
        ActiveScythe.SetActive(true);
        yield return new WaitForSeconds(4);

        
    }
    public void endScythe()
    {
        anim.SetBool("IsWalking", true);
        anim.SetBool("IsScythe", false);
        endAttack = true;
        ScytheAttackOn = false;
        cantSpamScythe = false;
    }
    public void hammerFixAttack()
    {
        StartCoroutine(HammerAttack());
    }
    IEnumerator HammerAttack()
    {
        Hammer.SetActive(true);
        
        
        if (PowerUp == 2) { yield return new WaitForSeconds(0.5f); }
        else { yield return new WaitForSeconds(1.5f); }
        GetComponent<Collider>().enabled = true;
        VFXHammer.SetActive(true);
        Hammer.GetComponent<BoxCollider>().enabled = true;//If done with collider
        yield return new WaitForSeconds(3);//Cooldown time
        anim.SetBool("IsHammer", false);
        anim.SetBool("IsWalking", true);
        VFXHammer.SetActive(false);
        Hammer.SetActive(false);
        endAttack = true;
        HammerAttackOn = false;
        cantSpamHammer = false;
    }
    void BombBulletHell() //Bullets instance
    {
        
        int numberOfBombs;
        if (PowerUp == 1) { numberOfBombs = 3; } else { numberOfBombs = 6; }
        {
            List<GameObject> bullets = new List<GameObject>();
            for (int i = 0; i < numberOfBombs; i++)
            {
                Vector3 randomPlace = new Vector3(Random.Range(-5,5),0, Random.Range(-5,5));
                bullets.Add(Instantiate(BombChaos, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity));
                bullets[i].GetComponent<Rigidbody>().velocity = 2 * randomPlace;
                bullets[i].SetActive(true);
            }
        }

        
    }

    IEnumerator WhipAttack()
    {
        if (PowerUp == 2)
        {
            for (int i = 0; i < WhipAttackRange.Length; i++)
            {
                if (WhipOn)
                {
                    if (PowerUp == 2)
                    {
                        anim.speed = 3;
                        anim.SetBool("IsWhip"+i, true);
                        yield return new WaitForSeconds(0.5f);
                        anim.SetBool("IsWhip" + i, false);
                    }
                    anim.speed = 1;
                }
                else
                {
                    StopCoroutine(WhipAttack());
                }
            }
        }
        
        else
        {
            cantRotate = true;
            anim.SetBool("IsWhip" + currentAttack, true);
            iswhiping = true;
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("IsWhip" + currentAttack, false);
            cantRotate = false;
            currentAttack++;
            yield return new WaitForSeconds(1f);//Tiempo de cd
            iswhiping = false;
        }
        if (currentAttack >= WhipAttackRange.Length)
        {
            currentAttack = 0;
        }
        cantSpamWhip = false;
        endAttack = true;
    }

    IEnumerator deathRay()
    {
        GameObject ataqueRayoCast;
        agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);//haCasteado
        rotSpeed = rotSpeed-7;
        if (PowerUp == 2)
        {
            anim.SetBool("IsRayUpgrade", true);
            yield return new WaitForSeconds(0.5f);
            transform.position = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0.5f);
            ataqueRayoCast = Instantiate(poweUpRay, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
        }
        else
        {
            anim.SetBool("IsRay", true);
            ataqueRayoCast = Instantiate(deathRayCast, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), gameObject.transform.rotation);
        }

        ataqueRayoCast.SetActive(true);
        if (PowerUp == 2){yield return new WaitForSeconds(7); anim.SetBool("IsRayUpgrade", false); } else {yield return new WaitForSeconds(2); anim.SetBool("IsRay", false); }//haCasteado
        anim.SetBool("IsWalking", true);
        agent.isStopped = false;
        cantSpamDeathRay = false;
        deathRayOn = false;
        endAttack = true;
        rotSpeed =rotSpeed+7;
        Destroy(ataqueRayoCast);
    }
}
