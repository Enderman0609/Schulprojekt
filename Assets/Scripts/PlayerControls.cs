using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Animator animator;
    private Transform slimeTransform;
    public Rigidbody2D rbp;
    public float moveSpeed;
    public float knockbackForce;
    float speedX, speedY;
    private bool isMoving;
    bool canMove = true;
    public float Playerhealth = 20;
     public float PlayerHealth
    {
        get { return Playerhealth; }
        set
        {
            Playerhealth = value;
        }
    }
    
    public GameObject SwordHitbox;
    Collider2D swordCollider;
    private bool canBeKnockedBack = true;  // Verhindert mehrfachen Knockback
    public float knockbackResistance = 10f; // Wie stark der Spieler zurückgestoßen wird

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rbp = GetComponent<Rigidbody2D>();
        swordCollider = SwordHitbox.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    
    void Update()
    {
        if (canMove == true)
        {
            speedX = Input.GetAxis("Horizontal");
            speedY = Input.GetAxis("Vertical");
            rbp.linearVelocity = new Vector2(speedX * moveSpeed, speedY * moveSpeed);

            if (isMoving = speedX != 0 || speedY != 0)
            {
                animator.SetFloat("moveX", speedX);
                animator.SetFloat("moveY", speedY);
            }
            animator.SetBool("isMoving", isMoving);
        }
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
    void OnSpielerHit(float damage)
    {
        Debug.Log("Spieler hit for " + damage);
        PlayerHealth -= (int)damage;
        Debug.Log("HP" + PlayerHealth);
        if (Playerhealth <= 0)
        {
            animator.SetTrigger("death");
            Debug.Log("Spieler ist tot");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    [Obsolete]
    public void ApplyKnockback(Vector2 sourcePosition, float knockbackForce)
    {
        if (canBeKnockedBack && rbp != null)
        {
            // Berechne Richtung vom Gegner weg
            Vector2 direction = (transform.position - (Vector3)sourcePosition).normalized;
            
            // Setze aktuelle Geschwindigkeit zurück
            rbp.velocity = Vector2.zero;
            
            // Wende Knockback an
            rbp.linearVelocity = direction * knockbackForce;
             Debug.Log("Knockback Spieler");
            

            StartCoroutine(KnockbackCooldown());
        }
    }

    private IEnumerator KnockbackCooldown()
    {
        canBeKnockedBack = false;
        yield return new WaitForSeconds(0.2f);
        canBeKnockedBack = true;
    }
}