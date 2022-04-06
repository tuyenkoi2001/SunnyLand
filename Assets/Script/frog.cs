using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;

    private Collider2D coll;
    private bool facingLeft;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        facingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transition from jump to fall
        if (animator.GetBool("isJumping"))
        {
            if(rb.velocity.y < .1)
            {
                animator.SetBool("isFalling", true);
                animator.SetBool("isJumping", false);
            }
        }
        //transition from fall to idle
        if (coll.IsTouchingLayers(ground))
        {
            animator.SetBool("isFalling", false);
        }
    }

    private void Move()
    {
        float posX = rb.transform.position.x;
        float posY = rb.transform.position.y;
        float localScale = rb.transform.localScale.x;
        if (facingLeft)
        {
            if (posX >= leftCap)
            {
                if (localScale != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = (new Vector3(-jumpLength, jumpHeight));
                    animator.SetBool("isJumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (posX <= rightCap)
            {
                if (localScale != -1)
                {
                    rb.transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = (new Vector3(jumpLength, jumpHeight));
                    animator.SetBool("isJumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
    
 }
