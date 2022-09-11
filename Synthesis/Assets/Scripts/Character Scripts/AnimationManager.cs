using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    private float JabDelay = 0.375f, CrossDelay = 0.333f; 
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAnimation(float LR, float UD, bool jump, bool Sprint, bool[] attack)
    {
        if (LR != 0)
        {
            if (Sprint == true)
                animator.SetBool("Run", true);
            else
                animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);

        }
        if (attack[0] == true)
             StartCoroutine(AttackAnimDelay("Jab", JabDelay));

        if (attack[1] == true)
            StartCoroutine(AttackAnimDelay("Cross", CrossDelay));

    }
    private void SetAttack()
    {

    }
     IEnumerator AttackAnimDelay(string name, float timer)
    {

        animator.SetBool(name, true);

        yield return timer;

        animator.SetBool(name, false);
    }
}
