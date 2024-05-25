using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Reaver : FinalBossStates
{
    public Reaver(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_enemy, _agent, _anim, _player)
    {

    }
    public override void Enter()
    {
        
        //Anim.SetBool(isWalking, true);
        base.Enter();
    }

    public override void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer > 0.5f&&!hasAttacked)
        {
            Anim.SetBool("IsScythe", true);
            Agent.SetDestination(Enemy.transform.position);
            hasAttacked = true;
            //Enemy.GetComponent<FinalBoss>().ScytheAttackOn = true;
        }
        if(timer>3&& !Enemy.GetComponent<FinalBoss>().ScytheAttackOn) {  nextState = new Follow(Enemy, Agent, Anim, Player); Stage = Event.Exit; }

    }

    public override void Exit()
    {

        base.Exit();
    }
}
