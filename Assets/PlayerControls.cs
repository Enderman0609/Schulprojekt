using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed;
    float speedX, speedY;
    private bool isMoving;
    private bool Swordattack;
    bool canMove = true;
    public GameObject swordHitbox;
    Collider2D swordCollider;
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (canMove)
        {
            moveSpeed = 2;
        }
        else
        {
            moveSpeed = 0;
        }
        speedX = Input.GetAxis("Horizontal");
        speedY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(speedX * moveSpeed, speedY * moveSpeed);
        animator.SetFloat("moveX", speedX);
        animator.SetFloat("moveY", speedY);
        isMoving = speedX != 0 || speedY != 0;
        animator.SetBool("isMoving", isMoving);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Swordattack");
        }

        if (Mathf.Abs(speedX) < Mathf.Abs(speedY))
        {
            if (speedY > 0)
            {
                gameObject.BroadcastMessage("FacingTop", true);
            }
            else if (speedY < 0)
            {
                gameObject.BroadcastMessage("FacingBot", true);
            }

        }
        else
        {
            if (speedX < 0)
            {
                gameObject.BroadcastMessage("FacingLeft", true);
            }
            else if (speedX > 0)
            {
                gameObject.BroadcastMessage("FacingRight", true);
            }
        }
    }
    void LockMovement()
    {
        canMove = false;
    }
    void UnlockMovement()
    {
        canMove = true;
    }

}