using UnityEngine;

public class End_Boss : MonoBehaviour
{
    // Cooldown-Zeit zwischen den Stampf-Angriffen
    public float stompCooldown = 10f;
    // Speichert die aktuelle Welle der Gegner-Spawns
    private float Welle = 0;
    // Aktuelle Gesundheit des Bosses
    public float BossHealth;
    // Maximale Gesundheit des Bosses
    public float maxHealth;
    // Prefab für die Schockwelle, die beim Stampfen erzeugt wird
    public GameObject shockwavePrefab;
    // Wachstumsrate der Schockwelle pro Sekunde
    public float shockwaveGrowthRate = 5f;
    // Maximale Größe der Schockwelle
    public float shockwaveMaxSize = 10f;
    // Dauer der Stampf-Animation
    public float stompAnimationDuration = 1f;
    // Zeitpunkt für den nächsten Stampf-Angriff
    private float nextStompTime;
    // Gibt an, ob der Boss gerade stampft
    private bool isStomping = false;
    // Referenz auf die aktuelle Schockwelle
    private GameObject currentShockwave;
    // Referenz auf die Animator-Komponente
    private Animator animator;
    // Prefab für Skelett-Gegner
    public GameObject skeletonPrefab;
    // Prefab für Schleim-Gegner
    public GameObject slimePrefab;
    // Radius um den Boss, in dem Gegner spawnen
    public float spawnRadius = 5f;
    // Maximale Anzahl von Gegnern pro Welle
    public int maxMobsPerWave = 1; 

    // Initialisierung beim Start
    void Start()
    {
        // Setzt den Zeitpunkt für den ersten Stampf-Angriff
        nextStompTime = Time.time + stompCooldown;
        // Holt die Animator-Komponente
        animator = GetComponent<Animator>();
        // Speichert die maximale Gesundheit des Bosses
        maxHealth = gameObject.GetComponent<DamageController>().Health;
    }

    // Wird jeden Frame aufgerufen
    void Update()
    {
        // Aktualisiert die aktuelle Gesundheit des Bosses
        BossHealth = gameObject.GetComponent<DamageController>().Health;
        
        // Prüft, ob es Zeit für einen neuen Stampf-Angriff ist
        if (Time.time >= nextStompTime && !isStomping)
        {
            StartStomp();
        }
        
        // Spawnt Gegner basierend auf der Gesundheit des Bosses
        if (BossHealth <= 1000 && Welle == 0)
        {
            SpawnMobs();
            Debug.Log("Welle 1");
            Welle = 1;
        }
        if (BossHealth <= 700 && Welle == 1)
        {
            SpawnMobs();
            Debug.Log("Welle 2");
            Welle = 2;
        }
        if (BossHealth <= 500 && Welle == 2)
        {
            SpawnMobs();
            Debug.Log("Welle 3");
            Welle = 3;
        }
        if (BossHealth <= 300 && Welle == 3)
        {
            SpawnMobs();
            Debug.Log("Welle 4");
            Welle = 4;
        }

        // Vergrößert die aktive Schockwelle, falls vorhanden
        if (currentShockwave != null)
        {
            GrowShockwave();
        }
    }

    // Startet den Stampf-Angriff und die Animation
    void StartStomp()
    {
        isStomping = true;
        animator.SetTrigger("Stomp");
    }

    // Erzeugt eine neue Schockwelle
    void CreateShockwave()
    {
        // Berechnet die Position für die Schockwelle
        Debug.Log(transform.position);
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 1.5f;

        // Instanziiert die Schockwelle
        currentShockwave = Instantiate(shockwavePrefab, spawnPosition, Quaternion.identity);

        // Setzt den Cooldown für den nächsten Stampf
        nextStompTime = Time.time + stompCooldown;
    }

    // Vergrößert die aktuelle Schockwelle
    void GrowShockwave()
    {
        // Erhöht die Größe der Schockwelle
        currentShockwave.transform.localScale += new Vector3(1, 0, 1) * shockwaveGrowthRate * Time.deltaTime;

        // Zerstört die Schockwelle, wenn sie die maximale Größe erreicht hat
        if (currentShockwave.transform.localScale.x >= shockwaveMaxSize)
        {
            Destroy(currentShockwave);
            currentShockwave = null;
            isStomping = false;
        }
    }

    // Spawnt Gegner basierend auf der aktuellen Welle
    void SpawnMobs()
    {
        // Bestimmt die Anzahl der zu spawnenden Gegner je nach Welle
        int mobsToSpawn;
        switch (Welle)
        {
            case 1:
                mobsToSpawn = 2;
                break;
            case 2:
                mobsToSpawn = 3; 
                break;
            case 3:
                mobsToSpawn = 4;
                break;
            case 4:
                mobsToSpawn = 5;
                break;
            default:
                mobsToSpawn = 2;
                break;
        }

        // Spawnt die festgelegte Anzahl von Gegnern
        for (int i = 0; i < mobsToSpawn; i++)
        {
            // Wählt das zu spawnende Prefab basierend auf der Welle
            GameObject prefabToSpawn;
            switch (Welle)
            {
                case 1:
                    prefabToSpawn = slimePrefab; // Nur Slimes in Welle 1
                    break;
                case 2:
                    prefabToSpawn = skeletonPrefab; // Nur Skelette in Welle 2 
                    break;
                case 3:
                    prefabToSpawn = (i % 2 == 0) ? skeletonPrefab : slimePrefab; // Mix in Welle 3
                    break;
                case 4:
                    prefabToSpawn = (i < 3) ? skeletonPrefab : slimePrefab; // 3 Skelette, 2 Slimes in Welle 4
                    break;
                default:
                    prefabToSpawn = slimePrefab;
                    break;
            }

            // Berechnet eine zufällige Position im Umkreis des Bosses
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPosition = transform.position + new Vector3(randomDirection.x * spawnRadius, randomDirection.y * spawnRadius, -0.5f);

            // Instanziiert den Gegner an der berechneten Position
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}