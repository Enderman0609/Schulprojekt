using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed;
    float speedX, speedY;
    private bool isMoving;
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        speedX = Input.GetAxis("Horizontal");
        speedY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(speedX * moveSpeed, speedY * moveSpeed);
        animator.SetFloat("moveX", speedX);
        animator.SetFloat("moveY", speedY);
        isMoving = speedX != 0 || speedY != 0;
        animator.SetBool("isMoving", isMoving);
    }
}