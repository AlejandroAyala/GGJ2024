using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAnimator : MonoBehaviour
{

    public Animator animator;

    public void Awake()
    {
        StartCoroutine(WaitRandom());
    }

    public IEnumerator WaitRandom()
    {
        yield return new WaitForSeconds(Random.Range(0, 2));
        if(animator!=null)
        {
            animator.SetBool("Rand", true);
        }
    }
}
