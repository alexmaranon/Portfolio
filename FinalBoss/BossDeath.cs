using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossDeath : FinalBossStates
{

    public BossDeath(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_enemy, _agent, _anim, _player)
    {

    }

    public override void Enter()
    {

        Anim.SetBool("IsDead", true);
        base.Enter();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        base.Exit();
    }

}
