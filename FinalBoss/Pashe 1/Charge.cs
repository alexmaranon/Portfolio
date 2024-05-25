using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Charge : FinalBossStates
{
    Vector3 dashPosition;
    int hasDashedTwice;
    float timerIfBug;
    public Charge(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_enemy, _agent, _anim, _player)
    {

    }
    public override void Enter()
    {
        Anim.SetBool("IsWalking", true);
        Anim.speed = 3;

        Agent.speed = Agent.speed+ 50;
        Agent.acceleration = Agent.acceleration+ 50;
        dashPosition = Player.transform.position;
        Agent.isStopped = false;
        Agent.SetDestination(dashPosition);
        hasDashedTwice = 1;
        Enemy.GetComponent<FinalBoss>().endAttack = false;
        base.Enter();
    }

    public override void Update()
    {
        timerIfBug = timerIfBug + Time.deltaTime;
        float distance = Mathf.Abs((dashPosition - Enemy.transform.position).magnitude);
        if (distance < 2) { Agent.isStopped = true; timer = timer + Time.deltaTime;Anim.speed = 1;}

        if (timer > 1&&hasAttacked&&Enemy.GetComponent<FinalBoss>().PowerUp==2) { hasDashedTwice = 2; hasAttacked =false; dashPosition = Player.transform.position; Agent.isStopped = false; Agent.SetDestination(dashPosition);}


        if (hasDashedTwice == 2 && timer > 1 || Enemy.GetComponent<FinalBoss>().PowerUp ==1 && timer>1)
        {
            nextState = new Follow(Enemy, Agent, Anim, Player);
            Enemy.GetComponent<FinalBoss>().endAttack = true;
            Anim.SetBool("IsWalking", false);
            Stage = Event.Exit;
        }

        if (timerIfBug > 3) { Enemy.GetComponent<FinalBoss>().endAttack = true; Anim.SetBool("IsWalking", false); nextState = new Follow(Enemy, Agent, Anim, Player); Stage = Event.Exit; }

    }

    public override void Exit()
    {
        
        Agent.speed = Agent.speed - 50;
        Agent.acceleration = Agent.acceleration - 50;
        base.Exit();
    }

}
