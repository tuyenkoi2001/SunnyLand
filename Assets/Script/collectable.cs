using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    Animator animator;
    private AudioSource audio;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();    
        audio = GetComponent<AudioSource>();
    }
    public void isCollected()
    {
        audio.Play();
        animator.SetTrigger("isCollected");
    }
    private void gotItem()
    {
        Destroy(this.gameObject);
    }
}
