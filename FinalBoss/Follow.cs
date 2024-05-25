using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Follow : FinalBossStates
{
    float time;
    float distance;
    public static float timers;
    public Follow(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) :base(_enemy, _agent, _anim, _player)
    {

    }

    public override void Enter()
    {
        if (Enemy.GetComponent<FinalBoss>().PowerUp == 2) { time = 3; } else { time = 5; }
        Agent.isStopped = false;
        Anim.SetBool("IsWalking", true);
        base.Enter();
    }

    public override void Update()
    {
        if (!Enemy.GetComponent<FinalBoss>().AbsorbingSouls)
        {
            timers = timers + Time.deltaTime;
            distance = Mathf.Abs((Player.transform.position - Enemy.transform.position).magnitude);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Agent.SetDestination(Player.transform.position + (Player.transform.forward * distance));
            }
            else { Agent.SetDestination(Player.transform.position); }
                
            if (canTFollow())
            {
                nextState = new Whip(Enemy, Agent, Anim, Player);
                Stage = Event.Exit;
            }
            if (timers > time && Enemy.GetComponent<FinalBoss>().Fase == 1)
            {
                timers = 0;
                if (Random.Range(0, 100) < 50) { nextState = new Charge(Enemy, Agent, Anim, Player); Stage = Event.Exit; }
                else { Stage = Event.Exit; nextState = new Ball(Enemy, Agent, Anim, Player); Stage = Event.Exit; }
            }

            if (timers > time && Enemy.GetComponent<FinalBoss>().Fase == 2)
            {
                timers = 0;
                if (Random.Range(0, 100) < 50) { nextState = new DeathRay(Enemy, Agent, Anim, Player); Stage = Event.Exit; }
                else { Stage = Event.Exit; nextState = new BombChaos(Enemy, Agent, Anim, Player); Stage = Event.Exit; }
            }

            if (timers > time && Enemy.GetComponent<FinalBoss>().Fase == 3)
            {
                timers = 0;
                int randomProb = Random.Range(0, 100);
                if (randomProb >= 66) { nextState = new Hammer(Enemy, Agent, Anim, Player); Stage = Event.Exit; }
                else if (randomProb < 66 && randomProb >= 33) { nextState = new Reaver(Enemy, Agent, Anim, Player); Stage = Event.Exit; }
                else { nextState = new CrossBow(Enemy, Agent, Anim, Player); Stage = Event.Exit; }
            }
        }
        else
        {
            Enemy.transform.LookAt(new Vector3(0, Enemy.transform.position.y, 0));
            Agent.SetDestination(new Vector3(0,Enemy.transform.position.y,0));
        }
        

    }

    public override void Exit()
    {
        Agent.SetDestination(Enemy.transform.position);
        //Agent.isStopped = true;
        Anim.SetBool("IsWalking", false);
        base.Exit();
    }

}
