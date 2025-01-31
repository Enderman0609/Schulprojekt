using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SlimeScript : MonoBehaviour
{
    private bool death;
    public bool isMoving;
    private Animator animator;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
        }
    }
    public float speed;
    public float health = 3;
    private Transform target;

    void OnHit(float damage)
    {
        Debug.Log("Slime hit for" + damage);
        Health -= (int)damage;
        if (health <= 0)
        {
            animator.SetTrigger("death");

        }
    }
    void DestroyEntity()
    {
        Destroy(gameObject); // Löscht das GameObject
    }
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 8)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }

        else
        {
            animator.SetBool("isMoving", false);
        }

    }
}

