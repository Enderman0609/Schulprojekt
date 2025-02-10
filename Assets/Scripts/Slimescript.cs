using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Slimescript : MonoBehaviour
{
    public float knockbackForce = 10f;	
    public float slimedamage = 1f;
    private bool canMove = true;
    public bool isMoving;
    private Animator animator;
    public float speed = 1;
    public Collider2D SlimeCollider;
    public float health = 3;
    private Transform target;
    private Rigidbody2D rb;
    private Rigidbody2D rbp;

    public float Health
    {
        get { return health; }
        set
        {
            health = value;
        }
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rbp = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    void OnHit(float damage)
    {
        Debug.Log("Slime hit for " + damage);
        Health -= (int)damage;
        if (health <= 0)
        {
            animator.SetTrigger("death");
        }
    }
    void OnKnockback(float knockbackForce)
    {
        Debug.Log("Slime knockbacked for " + knockbackForce);
        Knockback(target, knockbackForce); // Ruft die Knockback-Methode des SlimeScript-Skripts auf
    }

    void DestroyEntity()
    {
        Destroy(gameObject); // Löscht das GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 8 && canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }

        else
        {
            animator.SetBool("isMoving", false);
        }

    }
    void LockMovement()
    {
        canMove = false;
    }
    
    public void Knockback(Transform playerTransform, float knockbackForce)
    {
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.linearVelocity = direction * knockbackForce;
        Debug.Log("Knockback");
    }

    [System.Obsolete]
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hole die PlayerControls Komponente
            PlayerControls playerControls = collision.gameObject.GetComponent<PlayerControls>();
            
            if (playerControls != null)
            {
                // Rufe die Knockback-Methode des Spielers auf
                playerControls.ApplyKnockback(transform.position, knockbackForce);
                // Füge Schaden zu
                collision.gameObject.SendMessage("OnSpielerHit", slimedamage);
            }
        }
    }
}

