using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerSkills : MonoBehaviour {

    public void OnTriggerStay2D(Collider2D other)
    {
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<OknoSkills>().StartSkills();
            CharacterAnimationController.anim.SetBool("StopMovement", true);
        }
    }
}
