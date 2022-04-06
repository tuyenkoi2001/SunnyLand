using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected BoxCollider2D coll;
    private AudioSource audio;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
    }
    public void JumpedOn()
    {
        audio.Play();
        animator.SetTrigger("death");
        coll.isTrigger = true;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }

}
