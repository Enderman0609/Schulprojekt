using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Quest-Status
    public bool BogenQuestErledigt = false;
    public bool AxtQuestErledigt = false;
    
    // Spielerposition für verschiedene Räume
    public float PlayerPositionX1;
    public float PlayerPositionY1;
    public float PlayerPositionX2;
    public float PlayerPositionY2;
    public float PlayerPositionX3;
    public float PlayerPositionY3;
    public float PlayerPositionX4;
    public float PlayerPositionY4;
    public float PlayerPositionX5;
    public float PlayerPositionY5;
    
    // Aktueller Raum
    public int Raum;
    
    // Komponenten
    private Animator animator;
    public int PlayerHealth;
    private Transform slimeTransform;
    public Rigidbody2D PlayerRigidbody;
    
    // Bewegungsvariablen
    public float moveSpeed;
    float speedX, speedY;
    private bool isMoving;
    
    // Kampfvariablen
    private bool BowAttack;
    bool canMove = true;
    private float holdTime = 0;
    private const float HOLD_TIME_LIMIT = 1.6f;
    public GameObject pfeilPrefab;
    private Transform nearestEnemy;
    public float detectionRange = 8f;
    public LayerMask enemyLayer;
    public GameObject SwordHitbox;
    Collider2D swordCollider;
    private bool canShoot = true;
    
    // Initialisierung der Komponenten
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // Wird beim Start aufgerufen
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        swordCollider = SwordHitbox.GetComponent<Collider2D>();
        
        // Lade gespeicherte Daten
        BogenQuestErledigt = PlayerPrefs.GetInt("BogenQuestErledigt",0) == 1;
        AxtQuestErledigt = PlayerPrefs.GetInt("AxtQuestErledigt",0) == 1;
        Raum = PlayerPrefs.GetInt("AktuellerRaum",1);
        
        // Lade Spielerpositionen für jeden Raum
        PlayerPositionX1 = PlayerPrefs.GetFloat("PlayerPosition1X",0);
        PlayerPositionY1 = PlayerPrefs.GetFloat("PlayerPosition1Y",0);
        PlayerPositionX2 = PlayerPrefs.GetFloat("PlayerPosition2X",0);
        PlayerPositionY2 = PlayerPrefs.GetFloat("PlayerPosition2Y",0);
        PlayerPositionX3 = PlayerPrefs.GetFloat("PlayerPosition3X",0);
        PlayerPositionY3 = PlayerPrefs.GetFloat("PlayerPosition3Y",0);
        PlayerPositionX4 = PlayerPrefs.GetFloat("PlayerPosition4X",0);
        PlayerPositionY4 = PlayerPrefs.GetFloat("PlayerPosition4Y",0);
        PlayerPositionX5 = PlayerPrefs.GetFloat("PlayerPosition5X",0);
        PlayerPositionY5 = PlayerPrefs.GetFloat("PlayerPosition5Y",0);
        
        // Setze Spielerposition basierend auf aktuellem Raum
        if (Raum == 1)
        {
            transform.position = new Vector2(PlayerPositionX1, PlayerPositionY1);
        }
        else if (Raum == 2)
        {
            transform.position = new Vector2(PlayerPositionX2, PlayerPositionY2);
        }
        else if (Raum == 3)
        {
            transform.position = new Vector2(PlayerPositionX3, PlayerPositionY3);
        }
        else if (Raum == 4)
        {
            transform.position = new Vector2(PlayerPositionX4, PlayerPositionY4);
        }
        else if (Raum == 5)
        {
            transform.position = new Vector2(PlayerPositionX5, PlayerPositionY5);
        }

    }

    // Wird jeden Frame aufgerufen
    void Update()
    {
        // Speichere aktuelle Position je nach Raum
        if(Raum == 1)
        {
            PlayerPositionX1 = (float)transform.position.x;
            PlayerPositionY1 = (float)transform.position.y;
            PlayerPrefs.SetFloat("PlayerPosition1X", PlayerPositionX1);
            PlayerPrefs.SetFloat("PlayerPosition1Y", PlayerPositionY1);
        }
        else if(Raum == 2)
        {
            PlayerPositionX2 = (float)transform.position.x;
            PlayerPositionY2 = (float)transform.position.y;
            PlayerPrefs.SetFloat("PlayerPosition2X", PlayerPositionX2);
            PlayerPrefs.SetFloat("PlayerPosition2Y", PlayerPositionY2);
        }
        else if(Raum == 3)
        {
            PlayerPositionX3 = (float)transform.position.x;
            PlayerPositionY3 = (float)transform.position.y;
            PlayerPrefs.SetFloat("PlayerPosition3X", PlayerPositionX3);
            PlayerPrefs.SetFloat("PlayerPosition3Y", PlayerPositionY3);
        }
        else if(Raum == 4)
        {
            PlayerPositionX4 = (float)transform.position.x;
            PlayerPositionY4 = (float)transform.position.y;
            PlayerPrefs.SetFloat("PlayerPosition4X", PlayerPositionX4);
            PlayerPrefs.SetFloat("PlayerPosition4Y", PlayerPositionY4);
        }
        else if(Raum == 5)
        {
            PlayerPositionX5 = (float)transform.position.x;
            PlayerPositionY5 = (float)transform.position.y;
            PlayerPrefs.SetFloat("PlayerPosition5X", PlayerPositionX5);
            PlayerPrefs.SetFloat("PlayerPosition5Y", PlayerPositionY5);
        }
        
        // Aktualisiere Spielergesundheit
        PlayerHealth = gameObject.GetComponent<DamageController>().PlayerHealth;  
        
        // Sprint-Funktion
        if(Input.GetKey(KeyCode.LeftShift))
        {
           moveSpeed = 17f;
        }
        else
        {
            moveSpeed = 10;
        }
        
        // Bewegungssteuerung
        if (canMove)
        {
            speedX = Input.GetAxis("Horizontal");
            speedY = Input.GetAxis("Vertical");
            Vector2 moveDirection = new Vector2(speedX * moveSpeed, speedY * moveSpeed);
            PlayerRigidbody.linearVelocity += moveDirection * Time.deltaTime;

            // Aktualisiere Animations-Parameter
            if (isMoving = speedX != 0 || speedY != 0)
            {
                animator.SetFloat("moveX", speedX);
                animator.SetFloat("moveY", speedY);
            }
            animator.SetBool("isMoving", isMoving);
        }
        
        // Schwertangriff
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Swordattack");
        }

        // Bestimme Blickrichtung basierend auf Bewegung
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
        
        // Bogenangriff
        if (Input.GetMouseButton(1) && BogenQuestErledigt)
        {
            holdTime += Time.deltaTime;
            LockMovement();
            
            // Trigger nur beim ersten Drücken setzen, nicht in jedem Frame
            if (holdTime <= Time.deltaTime)
            {
                // Finde den nächsten Gegner und drehe dich in seine Richtung
                FindNearestEnemy();
                if (nearestEnemy != null)
                {
                    // Berechne Richtung zum Gegner
                    Vector2 directionToEnemy = (nearestEnemy.position - transform.position).normalized;
                    
                    // Setze die Bewegungsrichtung für die Animation
                    animator.SetFloat("moveX", directionToEnemy.x);
                    animator.SetFloat("moveY", directionToEnemy.y);
                    
                    // Aktualisiere die Blickrichtung
                    if (Mathf.Abs(directionToEnemy.x) < Mathf.Abs(directionToEnemy.y))
                    {
                        if (directionToEnemy.y > 0)
                        {
                            gameObject.BroadcastMessage("FacingTop", true);
                        }
                        else
                        {
                            gameObject.BroadcastMessage("FacingBot", true);
                        }
                    }
                    else
                    {
                        if (directionToEnemy.x < 0)
                        {
                            gameObject.BroadcastMessage("FacingLeft", true);
                        }
                        else
                        {
                            gameObject.BroadcastMessage("FacingRight", true);
                        }
                    }
                }
                
                BowAttack = true;
            }
            
            animator.SetBool("BowAttack", BowAttack);
            
            // Schieße Pfeil nach Halten der Taste
            if (holdTime >= HOLD_TIME_LIMIT)
            {
                ShootArrowAnimation();
                holdTime = 0;
                // Rechte Maustaste muss losgelassen und erneut gedrückt werden
                StartCoroutine(DisableShootingBriefly());
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            holdTime = 0;   
            UnlockMovement();
            animator.SetBool("BowAttack", false);
        }
        
        // Aktualisiere nächsten Gegner
        FindNearestEnemy();
    }
    
    // Sperrt Spielerbewegung
    void LockMovement()
    {
        canMove = false;
    }
    
    // Entsperrt Spielerbewegung
    void UnlockMovement()
    {
        canMove = true;
    }
    
    // Findet den nächsten Gegner in Reichweite
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
    
    // Deaktiviert kurzzeitig das Schießen
    IEnumerator DisableShootingBriefly()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }
    
    // Führt Pfeilschuss-Animation aus
    void ShootArrowAnimation()
    {
        // Nur schießen, wenn ein Gegner in Reichweite ist und Schießen erlaubt ist
        if (nearestEnemy == null || !canShoot)
        {
            if (nearestEnemy == null)
            {
                Debug.Log("Kein Gegner in Reichweite zum Schießen");
            }
            return; // Frühzeitig beenden
        }

        // Erstelle Pfeil und richte ihn zum Gegner aus
        GameObject pfeil = Instantiate(pfeilPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (nearestEnemy.position - transform.position).normalized;
        Debug.Log("Schieße auf Gegner: " + nearestEnemy.name);

        // Rotiere Pfeil in Richtung des Gegners
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pfeil.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Füge Geschwindigkeit zum Pfeil hinzu
        float arrowSpeed = 10f; // Geschwindigkeit des Pfeils
        Rigidbody2D pfeilRb = pfeil.GetComponent<Rigidbody2D>();
        if (pfeilRb != null)
        {
            pfeilRb.linearVelocity = direction * arrowSpeed;
        }

        // Aktiviere Collider nach kurzer Verzögerung
        Collider2D pfeilCollider = pfeil.GetComponent<Collider2D>();
        if (pfeilCollider != null)
        {
            pfeilCollider.enabled = false;
            StartCoroutine(EnableColliderAfterDelay(pfeilCollider, 0.09f)); 
        }
    }
    
    // Aktiviert Collider nach Verzögerung
    IEnumerator EnableColliderAfterDelay(Collider2D collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }

    // Markiert Bogen-Quest als erledigt
    public void BogenQuest()
    {
       BogenQuestErledigt = true;
    }
    
    // Markiert Holzfäller-Quest als erledigt
    public void HolzfällerQuestErledigt()
    {
        AxtQuestErledigt = true;
    }
}