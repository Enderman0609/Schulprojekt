using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Animator animator;
    private Transform slimeTransform;
    public Rigidbody2D PlayerRigidbody;
    public float moveSpeed;
    float speedX, speedY;
    private bool isMoving;
    bool canMove = true;
    private float holdTime = 0;
    private const float HOLD_TIME_LIMIT = 2f;
    public GameObject pfeilPrefab;
    private Transform nearestEnemy;
    public float detectionRange = 8f;
    public LayerMask enemyLayer;
    public GameObject SwordHitbox;
    Collider2D swordCollider;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        swordCollider = SwordHitbox.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (canMove)
        {
            speedX = Input.GetAxis("Horizontal");
            speedY = Input.GetAxis("Vertical");
            Vector2 moveDirection = new Vector2(speedX * moveSpeed, speedY * moveSpeed);
            PlayerRigidbody.linearVelocity += moveDirection * Time.deltaTime;

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
            if (Mathf.Abs(speedX) == Mathf.Abs(speedY))
            {
                gameObject.BroadcastMessage("FacingTop", true);
            }
            else if (speedY < 0)
            {
                gameObject.BroadcastMessage("FacingBot", true);
            }
        }
        else if (Mathf.Abs(speedX) > Mathf.Abs(speedY))
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
        if (Input.GetMouseButton(1))
        {
            holdTime += Time.deltaTime;
            if (holdTime >= HOLD_TIME_LIMIT)
            {
                ShootArrowAnimation();
                holdTime = 0;
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            holdTime = 0;
        }
        FindNearestEnemy();

    }
    void LockMovement()
    {
        canMove = false;
    }
    void UnlockMovement()
    {
        canMove = true;
    }
    private void FindNearestEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemyFound = null;

        foreach (Collider2D collider in hitColliders)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, collider.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemyFound = collider.transform;
            }
        }

        nearestEnemy = nearestEnemyFound;
    }
    void ShootArrowAnimation()
    {
        GameObject pfeil = Instantiate(pfeilPrefab, transform.position, Quaternion.identity);
        Vector2 direction = Vector2.zero;
        Debug.Log("Nearest Enemy: " + nearestEnemy);

        if (nearestEnemy != null)
        {
            // Schieße auf den nächsten Gegner
            direction = (nearestEnemy.position - transform.position).normalized;
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pfeil.transform.rotation = Quaternion.Euler(0, 0, angle);
        Collider2D pfeilCollider = pfeil.GetComponent<Collider2D>();
        if (pfeilCollider != null)
        {
            pfeilCollider.enabled = false;
            StartCoroutine(EnableColliderAfterDelay(pfeilCollider, 5f));
        }
    }
    IEnumerator EnableColliderAfterDelay(Collider2D collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }
}