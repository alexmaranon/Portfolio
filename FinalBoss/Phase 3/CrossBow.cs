using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrossBow : FinalBossStates
{
    public CrossBow(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_enemy, _agent, _anim, _player)
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
        if (timer > 0.5f && !hasAttacked)
        {
            hasAttacked = true;
            Enemy.GetComponent<FinalBoss>().CrossBowAttackOn = true;
        }
        if (timer > 2 && !Enemy.GetComponent<FinalBoss>().CrossBowAttackOn) { nextState = new Follow(Enemy, Agent, Anim, Player); Stage = Event.Exit; }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
