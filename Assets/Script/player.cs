using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private AudioSource audio;

    //FSN
    private enum State{idle,running,jumping,falling,hurt,climb};
    private State state = State.idle;
    
    //Inspector variable
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Text cherry; 
    [SerializeField] private Text gemText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private int level;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = 10f;
        coll = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
        CollectionPoint.level = level;
        CollectionPoint.ResetPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal < 0)
        {
            transform.Translate(new Vector2(horizontal, 0) * (speed * Time.deltaTime));
            transform.localScale = new Vector2(-1, 1);
        }
        else if (horizontal > 0)
        {
            rb.transform.Translate(new Vector2(horizontal, 0) * (speed * Time.deltaTime));
            transform.localScale = new Vector2(1, 1);
        }
        if (Input.GetKey(KeyCode.UpArrow) && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    void AnimationState()
    {
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }

        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }


        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "collectable")
        {
            collectable item = collision.gameObject.GetComponent<collectable>();
            item.isCollected();
            //Destroy(collision.gameObject);
            CollectionPoint.cherry += 1;
            cherry.text = CollectionPoint.cherry.ToString();
        }
        if (collision.tag == "gem")
        {
            collectable item = collision.gameObject.GetComponent<collectable>();
            item.isCollected();
            //Destroy(collision.gameObject);
            CollectionPoint.gem += 1;
            gemText.text = CollectionPoint.gem.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemy enemy = collision.gameObject.GetComponent<enemy>();
            if(state == State.falling || rb.velocity.y < .0f)
            {
                Jump();
                enemy.JumpedOn();
            }
            else
            {
                state = State.hurt;
                float xEnemy = collision.gameObject.transform.position.x;
                float xPlayer = rb.transform.position.x;
                float yPlayer = rb.transform.position.y;
                if (xEnemy > xPlayer)
                {
                    rb.velocity = new Vector2(-hurtForce, yPlayer);
                }
                if(xEnemy < xPlayer)
                {
                    rb.velocity = new Vector2(hurtForce, yPlayer);
                }
            }
        }

        if(collision.gameObject.tag == "spike")
        {
            rb.transform.position = new Vector3(rb.position.x-5,rb.position.y,-3);
        }
        if (collision.gameObject.tag == "Finish")
        {
            rb.transform.position = new Vector3(rb.position.x - 5, rb.position.y, -3);
        }
    }

    void Footstep()
    {
        audio.Play();
    }
}
