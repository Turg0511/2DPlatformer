using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("Checkpoint", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "player")
        {
            PlayerManager.lastCheckPointPos = transform.position;
            myAnimator.SetBool("Checkpoint", true);
        }
    }
}
