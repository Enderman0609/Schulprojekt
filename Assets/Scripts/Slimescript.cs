using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Slimescript : MonoBehaviour
{
    public int knockbackForce;
    public int damage;
   // private bool canMove = true;
    public bool isMoving;
    private Animator animator;
    public float speed = 1;
    public Collider2D SlimeCollider;
    private Transform target;
    private Rigidbody2D rb;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 8) //&& canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }

        else
        {
            animator.SetBool("isMoving", false);
        }

    }
   // void LockMovement()
   // {
   //     canMove = false;
   // }

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Damage damageComponent = collider.gameObject.GetComponent<Damage>();
            if (damageComponent != null)
            {
                damageComponent.DealDamage(damage);
                Debug.Log("Enemy hit");
                damageComponent.KnockbackPlayer(knockbackForce, transform);
            }
        }
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Damage damageComponent = collider.gameObject.GetComponent<Damage>();
            if (damageComponent != null)
            {
                damageComponent.DealDamage(damage);
                Debug.Log("Enemy hit");
                damageComponent.KnockbackBothEnemy(knockbackForce, transform);
            }
        }
    }
}

