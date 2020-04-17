using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSleepingSMB : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var chance = Random.Range(0, 100);
        animator.SetInteger("AnimationChance", chance);
    }
}
