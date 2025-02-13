using UnityEngine;
using System.Collections;

public class Skelett_BogenScript : MonoBehaviour
{
    private Transform player;          // Neue Variable für den Schuss-Cooldown
    public float shootCooldown = 4f;        // Cooldown-Zeit zwischen Schüssen    private bool isMoving;              // Referenz zum Spieler
    private Rigidbody2D rb;               // Rigidbody des Skeletts
    private Animator animator;             // Animator des Skeletts
    public float detectionRange = 14f;     // Erkennungsreichweite
    public float preferredRange = 8f;      // Bevorzugte Kampfdistanz
    public float moveSpeed = 3f;           // Bewegungsgeschwindigkeit
    private bool playerInRange = false;    // Ist der Spieler in Reichweite?
    private Vector2 movement;              // Bewegungsrichtung
    public float health = 5f;
    public float knockbackResistance = 1f;  // Wie stark der Knockback reduziert wird
    public GameObject pfeilPrefab;        // Prefab des Pfeils
    public float pfeilSpeed = 10f;        // Geschwindigkeit des Pfeils
    private float nextShootTime;          // Zeitpunkt des nächsten Schusses
    private bool canMove = true;  // Neue Variable für Bewegungskontrolle
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    void Start()
    {
        // Komponenten initialisieren
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootArrowAnimation();
        }
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange && canMove)  // Prüfe canMove
        {
            playerInRange = true;
            HandleMovement(distanceToPlayer);
            
            if (Time.time >= nextShootTime) 
            {
                animator?.SetTrigger("Attack");
                StartCoroutine(LockMovementDuringAttack());  // Neue Coroutine
                ShootArrowAnimation();
                nextShootTime = Time.time + shootCooldown;
                Debug.Log("Skelett schießt");
            }
        }
        else
        {
            playerInRange = false;
            movement = Vector2.zero;
            animator?.SetBool("isMoving", false);
        }
    }
    private void HandleMovement(float distanceToPlayer)
    {
        if (distanceToPlayer > preferredRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction * moveSpeed;
            
            if (animator != null)
            {
                animator.SetFloat("moveX", direction.x);
                animator.SetFloat("moveY", direction.y);
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            movement = Vector2.zero;
            animator?.SetBool("isMoving", false);
        }
    }

    void FixedUpdate()
    {
        if (playerInRange)
        {
            // Bewegung ausführen
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }

    // Hilfsfunktion zum Visualisieren der Reichweiten im Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, preferredRange);
    }

    void OnHit(float damage)
    {
        Debug.Log("Skelett hit for " + damage);
        Health -= (int)damage;
        if (health <= 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("death");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator LockMovementDuringAttack()
    {
        yield return new WaitForSeconds(0.9f);  // Gleiche Zeit wie in der Pause-Funktion
    }

    // Der Pfeil wird nur durch diese Funktion geschossen, die von der Animation aufgerufen wird
    void ShootArrowAnimation()
    {
        if (player == null) return;
        
        GameObject pfeil = Instantiate(pfeilPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pfeil.transform.rotation = Quaternion.Euler(0, 0, angle);
        
        Rigidbody2D pfeilRb = pfeil.GetComponent<Rigidbody2D>();
        pfeilRb.linearVelocity = direction * pfeilSpeed;
        
        Destroy(pfeil, 15f / pfeilSpeed);
        Debug.Log("Pfeil geschossen");
        
    }
    void ShootArrowText()
    {
        Debug.Log("Pfeil Animation");
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
