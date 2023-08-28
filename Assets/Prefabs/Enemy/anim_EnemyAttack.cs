using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public class anim_EnemyAttack : StateMachineBehaviour
{
    
    [SerializeField] private GameObject SFX;
    private GameObject _sfx;

    public NavMeshAgent agent;
    private float walkingSpeedBefore;

    private float inAnimTime;
    private EnemyAttack myEnemyAttack;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myEnemyAttack = animator.gameObject.GetComponent<EnemyAttack>();
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
            myEnemyAttack.canAttack = true;
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
        inAnimTime += 1 * Time.deltaTime;
        if (inAnimTime >= (stateInfo.length / 2))
        {
            myEnemyAttack.checkDmgAble(1);
        }
    }


    
}
