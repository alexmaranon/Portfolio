using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FinalBossStates
{
    public enum States
    {
        Follow,Whip,Ball,Charge
    };

    public enum Event
    {
        Enter,Update,Exit
    };
    public FinalBossStates nextState;
    public States name;
    protected Event Stage;
    public Animator Anim;
    public NavMeshAgent Agent;
    public GameObject Enemy;
    public GameObject Player;

    protected float timer;
    protected bool hasAttacked;
    public FinalBossStates(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player)
    {
        Enemy = _enemy;
        Agent = _agent;
        Anim = _anim;
        Player = _player;
    }

    public FinalBossStates()
    {
        Stage = Event.Enter;
    }

    public virtual void Enter() { Stage = Event.Update; }
    public virtual void Update() { Stage = Event.Update; }
    public virtual void Exit() { Stage = Event.Exit; }

    public bool canTFollow()
    {

        float distance = Mathf.Abs((Player.transform.position - Enemy.transform.position).magnitude);
        if (distance <= 5) {return true; }
        return false;
    }

    public FinalBossStates Process()
    {
        if (Stage == Event.Enter) { Enter(); }
        if (Stage == Event.Update) { Update(); }
        if (Stage == Event.Exit) { Exit(); return nextState; }
        return this;
    }
}
