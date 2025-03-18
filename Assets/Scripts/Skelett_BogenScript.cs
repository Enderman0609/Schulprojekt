using UnityEngine;
using System.Collections;

public class Skelett_BogenScript : MonoBehaviour
{
    private Transform player;          // Neue Variable für den Schuss-Cooldown
    public float shootCooldown;        // Cooldown-Zeit zwischen Schüssen    private bool isMoving;              // Referenz zum Spieler
    private Rigidbody2D rb;               // Rigidbody des Skeletts
    private Animator animator;             // Animator des Skeletts
    public float detectionRange = 14f;     // Erkennungsreichweite
    public float preferredRange = 8f;      // Bevorzugte Kampfdistanz
    public float moveSpeed = 3f;           // Bewegungsgeschwindigkeit
    private bool playerInRange = false;    // Ist der Spieler in Reichweite?
    private Vector2 movement;              // Bewegungsrichtung
    public GameObject pfeilPrefab;        // Prefab des Pfeils
    public float pfeilSpeed = 10f;        // Geschwindigkeit des Pfeils
    private float nextShootTime;          // Zeitpunkt des nächsten Schusses
    private bool canMove = true;  // Neue Variable für Bewegungskontrolle
    public int Health;
    public bool alive = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

     void Update()
    {
        alive = GetComponent<Damage>().alive;
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
            HandleMovement(distanceToPlayer);
            
            if (Time.time >= nextShootTime) 
            {
                canMove = false;
                animator?.SetTrigger("Attack");
                StartCoroutine(WaitBeforeShoot());
                nextShootTime = Time.time + shootCooldown;
                //Debug.Log("Skelett schießt");
                canMove = true;
            }
        }
        else
        {
            playerInRange = false;
            movement = Vector2.zero;
            animator?.SetBool("isMoving", false);
        }
    }
private IEnumerator WaitBeforeShoot()
{
    // Wartet für eine kurze Zeit (z.B. 0.5 Sekunden)
    yield return new WaitForSeconds(1.6f);
    // Führt den Schuss aus
    if (alive == true)
    {
        ShootArrowAnimation();
    }
}
    private void HandleMovement(float distanceToPlayer)
    {
        if (distanceToPlayer > preferredRange && canMove && alive)
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
        if (playerInRange && canMove)
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

    // Der Pfeil wird nur durch diese Funktion geschossen, die von der Animation aufgerufen wird
    void ShootArrowAnimation()
    {
        if (player == null) return;
        
        GameObject pfeil = Instantiate(pfeilPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pfeil.transform.rotation = Quaternion.Euler(0, 0, angle);

        Collider2D pfeilCollider = pfeil.GetComponent<Collider2D>();
        if (pfeilCollider != null)
        {
            pfeilCollider.enabled = false;
            StartCoroutine(EnableColliderAfterDelay(pfeilCollider, 0.09f));
        }
        
        Rigidbody2D pfeilRb = pfeil.GetComponent<Rigidbody2D>();
        pfeilRb.linearVelocity = direction * pfeilSpeed;
        
        Destroy(pfeil, 15f / pfeilSpeed);
        //Debug.Log("Pfeil geschossen");
        
    }
    IEnumerator EnableColliderAfterDelay(Collider2D collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }
    void ShootArrowText()
    {
        //Debug.Log("Pfeil Animation");
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
