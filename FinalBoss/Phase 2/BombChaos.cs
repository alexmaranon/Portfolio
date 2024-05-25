using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BombChaos : FinalBossStates
{
    public BombChaos(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_enemy, _agent, _anim, _player)
    {

    }

    public override void Enter()
    {
        Anim.SetBool("IsBall", true);
        Enemy.GetComponent<FinalBoss>().endAttack = false;
        Agent.SetDestination(Enemy.transform.position);
        base.Enter();
    }

    public override void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer > 1.2f && !hasAttacked)
        {
            Enemy.GetComponent<FinalBoss>().bombChaosOn = true;
            //Attacks
            hasAttacked = true;
        }
        if(timer>4&&!Enemy.GetComponent<FinalBoss>().bombChaosOn == true)
        {
            Anim.SetBool("IsBall", false); Anim.SetBool("IsWalking", true); nextState = new Follow(Enemy, Agent, Anim, Player); Enemy.GetComponent<FinalBoss>().endAttack = true; Enemy.GetComponent<FinalBoss>().fixxxxxxx = false; Stage = Event.Exit;
        }

    }

    public override void Exit()
    {

        base.Exit();
    }
}
