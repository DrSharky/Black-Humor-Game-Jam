﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Events;
using System;

public class WanderingAI : MonoBehaviour
{

    public float wanderRadius;
    public float wanderTimer;
    public GameObject player;

    private float timer;
    private bool frozen = false;
    private bool endState = false;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Animator anim;

    private Action wanderListener;

    //Standard Asset adapting stuff
    private float turnAmt;
    private float forwardAmt;
    [SerializeField]
    float m_MovingTurnSpeed = 360;
    [SerializeField]
    float m_StationaryTurnSpeed = 180;

    Vector3 m_GroundNormal;

    public void Boom()
    {
        anim.StopPlayback();
        forwardAmt = 0.0f;
        turnAmt = 0.0f;
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        agent.enabled = false;
        endState = true;
        anim.enabled = false;
        frozen = true;  
    }


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderListener = new Action(() => { frozen = !frozen; });
    }

    // Use this for initialization
    void OnEnable()
    {
        timer = wanderTimer;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        m_GroundNormal = new Vector3(0, 1, 0);

        agent.updateRotation = false;
        agent.updatePosition = true;
        EventManager.StartListening("ToggleWander" + gameObject.GetInstanceID(), wanderListener);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);

            agent.SetDestination(newPos);
            timer = 0;
            wanderTimer = UnityEngine.Random.Range(1.0f, 5.0f);

        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            Move(agent.desiredVelocity);
        }
    }

    public void Move(Vector3 move)
    {

        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        turnAmt = Mathf.Atan2(move.x, move.z);

        forwardAmt = move.z * 0.5f;

        ApplyExtraTurnRotation();
    }


    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, forwardAmt);
        transform.Rotate(0, turnAmt * turnSpeed * Time.deltaTime, 0);
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    //public void PlayerIsDead()
    //{
    //    endState = true;
    //}
}