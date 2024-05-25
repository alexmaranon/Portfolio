using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Whip : FinalBossStates
{
    public Whip(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_enemy, _agent, _anim, _player)
    {

    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        timer = timer + Time.deltaTime;
        if (!hasAttacked)
        {
            Enemy.GetComponent<FinalBoss>().WhipOn = true;
            //Attacks
            hasAttacked = true;
        }
        if (!canTFollow()&&!Enemy.GetComponent<FinalBoss>().iswhiping)
        {
            Enemy.GetComponent<FinalBoss>().WhipOn = false;
            nextState = new Follow(Enemy, Agent, Anim, Player);
            Stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
