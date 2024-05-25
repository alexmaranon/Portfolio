using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ball : FinalBossStates
{
    public Ball(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_enemy, _agent, _anim, _player)
    {

    }
    public override void Enter()
    {
        Anim.SetBool("IsInvoking", true);
        Enemy.GetComponent<FinalBoss>().endAttack = false;
        Agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >1f && !hasAttacked)
        {
            Enemy.GetComponent<FinalBoss>().BallOn = true;
            hasAttacked = true;
        }
        if (timer > 2)
        {
            Enemy.GetComponent<FinalBoss>().BallOn = false;
            nextState = new Follow(Enemy, Agent, Anim, Player);
            Anim.SetBool("IsInvoking", false);
            Enemy.GetComponent<FinalBoss>().endAttack = true;
            Stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        
        base.Exit();
    }
}
