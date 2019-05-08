using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunkIdle : StateMachineBehaviour
{
    public float timer = Constants.THUNK_TIMER * 4;
    public bool change;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("idle");
        timer = Constants.THUNK_TIMER * 2;
        change = false;

        //Determine speed of attacks
        GameObject player = GameObject.Find("player");
        if(player != null)
        {
            PlayerMovement script = player.GetComponent<PlayerMovement>();
            float speed = 0.4f + 0.1f * (script.currentLevel / Constants.BOSS_FREQUENCY);
            animator.SetFloat("speedMultiplier", System.Math.Min(1,speed));
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Debug.Log("THUNK IDLE");
       if(timer <= 0) {
         if(!change) {
           change = true;

           int attack = Random.Range(0, 2);
           if(attack == 0) {
             animator.SetTrigger("swing-left");
           }
           else {
             animator.SetTrigger("swing-right");
           }
         }
       }
       else {
         timer -= Time.deltaTime;
       }
    }
}
