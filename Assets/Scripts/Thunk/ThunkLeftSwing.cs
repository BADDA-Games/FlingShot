using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunkLeftSwing : StateMachineBehaviour
{
    public float timer = Constants.THUNK_TIMER;
    public GameObject player;
    public bool change;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("swing-left");
        change = false;

        player = GameObject.FindWithTag("player");

        // timer = 1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer <= 0) {
          int attack = Random.Range(0, 6);

          if(attack <= 1 && !change) {
            change = true;
            // Debug.Log("Slam");
            animator.SetTrigger("slam-head");
          }
          else if(attack >= 3 && !change) {
            change = true;
            // if(player.transform.position.x <= 0) {
              // Debug.Log("Swing Right");
              animator.SetTrigger("swing-right");
            // }
            // else {
            //   animator.SetTrigger("slam-head");
            // }
          }
          else if(!change) {
            change = true;
            animator.SetTrigger("swing-left");
          }
        }
        else {
          timer -= Time.deltaTime;
        }
    }
}
