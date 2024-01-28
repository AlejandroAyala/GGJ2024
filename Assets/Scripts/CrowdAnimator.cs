using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAnimator : MonoBehaviour
{

    public Animator animator;
    public SpriteRenderer r;
    public List<Sprite> sprites = new List<Sprite>();

    public void Awake()
    {
        r = GetComponent<SpriteRenderer>();
        if (sprites.Count != 0)
        {
            int i = Random.Range(0,sprites.Count-1);
            r.sprite = sprites[i];
        }
        animator = GetComponent<Animator>();
        StartCoroutine(WaitRandom());
    }

    public IEnumerator WaitRandom()
    {
        float millis = Random.Range(0, 1500);
        yield return new WaitForSeconds(millis/1000);
        if(animator!=null)
        {
            animator.SetBool("Rand", true);
        }
        yield return null;
        animator.speed = Random.Range(0.8f, 1.5f);
    }
}
