using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OknoSkills : MonoBehaviour {

    public Animator animator;

    public void StartSkills()
    {
        animator.SetBool("IsDialogOpen", true);     
    }

    public void EndSkills()
    {
       animator.SetBool("IsDialogOpen", false);
       CharacterAnimationController.anim.SetBool("StopMovement", false);
       return;
    }
}
