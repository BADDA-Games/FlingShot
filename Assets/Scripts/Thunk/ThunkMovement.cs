using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunkMovement : MonoBehaviour {
   private Animator animate;
   private GameObject goal;
   private bool play;
   private float d;

   void Start () {
        goal = GameObject.Find("goal");
        animate = gameObject.GetComponent<Animator>();

        play = true;
        d = 0f;
   }

   void Update() {
        if (!play && d < 1f) {
            Vector3 oldPosition = goal.transform.position;
            goal.transform.position = Vector3.MoveTowards(goal.transform.position, goal.transform.position + Vector3.down, (float)1.0 * Time.deltaTime);
            d += Vector3.Distance(oldPosition, goal.transform.position);
        }

        if(GameVariables.TimeRemaining <= 0 && play) {
            StartCoroutine(PlayDeathAnimation());
        }
   }

   IEnumerator PlayDeathAnimation() {
        animate.SetBool("dead", true);
        float normalizedTime = Math.Min(animate.GetCurrentAnimatorStateInfo(0).normalizedTime, Constants.MAX_NORMALIZED_TIME);
        yield return new WaitForSecondsRealtime(animate.GetCurrentAnimatorStateInfo(0).length + normalizedTime);
        play = false;
   }
}