using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyland : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float leftCap;
    [SerializeField] float rightCap;
    [SerializeField] float topCap;
    [SerializeField] float bottomCap;
    [SerializeField] bool moveHorizontal;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        if (moveHorizontal)
        {
            float xPos = rb.position.x;
            if (xPos <= leftCap)
            {
                rb.velocity = new Vector3(5, 0, 0);
            }
            else if (xPos >= rightCap)
            {
                rb.velocity = new Vector3(-5, 0, 0);
            }
        }
        else
        {
            float yPos = rb.position.y;
            if (yPos <= bottomCap)
            {
                rb.velocity = new Vector3(0, 5, 0);
            }
            else if (yPos >= topCap)
            {
                rb.velocity = new Vector3(0, -5, 0);
            }
        }
    }
}
