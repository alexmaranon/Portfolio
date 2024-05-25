using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DeathRay : FinalBossStates
{
    public DeathRay(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_enemy, _agent, _anim, _player)
    {

    }
    public override void Enter()
    {
        Agent.SetDestination(Enemy.transform.position);
        Enemy.GetComponent<FinalBoss>().deathRayOn = true;
        base.Enter();
    }

    public override void Update()
    {
        if (Enemy.GetComponent<FinalBoss>().deathRayOn == false)
        {
            nextState = new Follow(Enemy, Agent, Anim, Player);
            Stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        
        base.Exit();
    }
}
