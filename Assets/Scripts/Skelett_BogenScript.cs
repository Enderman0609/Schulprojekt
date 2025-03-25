using UnityEngine;
using System.Collections;

// Diese Klasse steuert das Verhalten des Skelett-Bogenschützen
public class Skelett_BogenScript : MonoBehaviour
{
    private Transform player;              // Referenz zum Spieler
    public float shootCooldown;            // Cooldown-Zeit zwischen Schüssen
    private bool isMoving;                 // Zeigt an, ob sich das Skelett bewegt
    private Rigidbody2D rb;                // Rigidbody des Skeletts
    private Animator animator;             // Animator des Skeletts
    public float detectionRange = 14f;     // Erkennungsreichweite
    public float preferredRange = 8f;      // Bevorzugte Kampfdistanz
    public float moveSpeed = 3f;           // Bewegungsgeschwindigkeit
    private bool playerInRange = false;    // Ist der Spieler in Reichweite?
    private Vector2 movement;              // Bewegungsrichtung
    public GameObject pfeilPrefab;         // Prefab des Pfeils
    public float pfeilSpeed = 10f;         // Geschwindigkeit des Pfeils
    private float nextShootTime;           // Zeitpunkt des nächsten Schusses
    private bool canMove = true;           // Kontrolliert, ob sich das Skelett bewegen kann
    public int Health;                     // Gesundheit des Skeletts
    public bool alive = true;              // Status, ob das Skelett noch lebt

    // Wird beim Start aufgerufen
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Wird jeden Frame aufgerufen
    void Update()
    {
        alive = GetComponent<DamageController>().alive;
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
            HandleMovement(distanceToPlayer);
            
            // Überprüft, ob es Zeit für einen neuen Schuss ist
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

    // Wartet kurz, bevor der Pfeil geschossen wird (für Animation)
    private IEnumerator WaitBeforeShoot()
    {
        // Wartet für eine kurze Zeit (1.6 Sekunden)
        yield return new WaitForSeconds(1.6f);
        // Führt den Schuss aus, wenn das Skelett noch lebt
        if (alive == true)
        {
            ShootArrowAnimation();
        }
    }

    // Steuert die Bewegung des Skeletts basierend auf der Distanz zum Spieler
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

    // Wird für physikbasierte Bewegungen verwendet
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
        
        // Erstellt einen neuen Pfeil
        GameObject pfeil = Instantiate(pfeilPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pfeil.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Deaktiviert den Collider kurzzeitig, um Selbstkollisionen zu vermeiden
        Collider2D pfeilCollider = pfeil.GetComponent<Collider2D>();
        if (pfeilCollider != null)
        {
            pfeilCollider.enabled = false;
            StartCoroutine(EnableColliderAfterDelay(pfeilCollider, 0.09f));
        }
        
        // Setzt die Geschwindigkeit des Pfeils
        Rigidbody2D pfeilRb = pfeil.GetComponent<Rigidbody2D>();
        pfeilRb.linearVelocity = direction * pfeilSpeed;
        
        // Zerstört den Pfeil nach einer bestimmten Zeit
        Destroy(pfeil, 15f / pfeilSpeed);
        //Debug.Log("Pfeil geschossen");
    }

    // Aktiviert den Collider des Pfeils nach einer Verzögerung
    IEnumerator EnableColliderAfterDelay(Collider2D collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }

    // Debug-Funktion für die Pfeilanimation
    void ShootArrowText()
    {
        //Debug.Log("Pfeil Animation");
    }

    // Sperrt die Bewegung des Skeletts
    void LockMovement()
    {
        canMove = false;
    }

    // Entsperrt die Bewegung des Skeletts
    void UnlockMovement()
    {
        canMove = true;
    }
    
}
