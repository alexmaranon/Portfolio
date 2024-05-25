using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Hammer : FinalBossStates
{
    Vector3 dashPosition;
    float timerIfBug;

    public Hammer(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_enemy, _agent, _anim, _player)
    {

    }
    public override void Enter()
    {

        
        //dashPosition = Player.transform.position;
        base.Enter();
    }

    public override void Update()
    {
        //timerIfBug = timerIfBug + Time.deltaTime;
        //float distance = Mathf.Abs((dashPosition - Enemy.transform.position).magnitude);
        //if (distance > 2) { Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, dashPosition, Time.deltaTime*20); }
        //if (distance < 2) {timer = timer + Time.deltaTime;  }
        
        timer = timer + Time.deltaTime;
        if (timer > 0.1f&&!hasAttacked&& Enemy.GetComponent<FinalBoss>().PowerUp==1) { Anim.speed = 3; Anim.SetBool("IsHammer", true); hasAttacked = true; Enemy.GetComponent<FinalBoss>().HammerAttackOn = true; }

        if (timer>0.1f&&Enemy.GetComponent<FinalBoss>().PowerUp == 2&&!hasAttacked) { Anim.speed = 5; Anim.SetBool("IsHammer", true); hasAttacked = true; Enemy.GetComponent<FinalBoss>().HammerAttackOn = true; }


        if(timer>0.1f&& !Enemy.GetComponent<FinalBoss>().HammerAttackOn) { nextState = new Follow(Enemy, Agent, Anim, Player); Stage = Event.Exit; }

        
        //if (timerIfBug > 6) {  }
    }

    public override void Exit()
    {

        Anim.speed = 1;
        base.Exit();
    }
}
