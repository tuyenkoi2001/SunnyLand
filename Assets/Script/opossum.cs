using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossum : enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    private bool facingLeft;

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float posX = rb.transform.position.x;
        float localScale = rb.transform.localScale.x;
        if (facingLeft)
        {
            if (posX >= leftCap)
            {
                if (localScale != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                animator.SetBool("isRunning", true);
                rb.velocity = new Vector2(-3f, 0);
            }
            else
            {
                facingLeft = false;
                rb.velocity = Vector2.zero;
                animator.SetBool("isRunning", false);
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
                animator.SetBool("isRunning", true);
                rb.velocity = new Vector2(3f, 0);
            }
            else
            {
                facingLeft = true;
                rb.velocity = Vector2.zero;
                animator.SetBool("isRunning", false);
            }
        }
    }
}
