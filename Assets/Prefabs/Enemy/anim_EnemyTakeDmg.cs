using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class anim_EnemyTakeDmg : StateMachineBehaviour
{

    [SerializeField] private GameObject SFX;
    private GameObject _sfx;

    public NavMeshAgent agent;
    private float walkingSpeedBefore;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null)
        {
            try
            {
                agent = animator.gameObject.GetComponent<NavMeshAgent>();
                walkingSpeedBefore = agent.speed;
                agent.speed = 0;
            }
            catch
            {

            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            agent.speed = walkingSpeedBefore;
        }
        catch
        {

        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            agent.speed = 0.1f;
        }
        catch
        {

        }
    }



}
