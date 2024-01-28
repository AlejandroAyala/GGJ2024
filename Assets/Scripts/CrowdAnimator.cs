using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAnimator : MonoBehaviour
{

    public Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(WaitRandom());
    }

    public IEnumerator WaitRandom()
    {
        float millis = Random.Range(0, 2000);
        yield return new WaitForSeconds(millis/1000);
        if(animator!=null)
        {
            animator.SetBool("Rand", true);
        }
    }
}
