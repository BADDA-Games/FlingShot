using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunkSlam : StateMachineBehaviour
{
    public float timer = 1;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      animator.ResetTrigger("slam-head");

      // animator.SetBool("slam", false);

      timer = 1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer <= 0) {
          animator.SetTrigger("idle");
        }
        else {
          timer -= Time.deltaTime;
        }

        // if(timer <= 0) {
        //   // animator.ResetTrigger("slam-head");
        //   // animator.SetTrigger("idle");
        //   animator.SetBool("reset", true);
        // }
        // else {
        //   timer -= Time.deltaTime;
        // }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     animator.SetTrigger("idle");
    // }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
