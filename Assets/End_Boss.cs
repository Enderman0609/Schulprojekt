using UnityEngine;

public class End_Boss : MonoBehaviour
{
    public float stompCooldown = 10f;
    private float Welle = 0;
    public float BossHealth;
    public float maxHealth;
    public GameObject shockwavePrefab;
    public float shockwaveGrowthRate = 5f;
    public float shockwaveMaxSize = 10f;
    public float stompAnimationDuration = 1f;
    private float nextStompTime;
    private bool isStomping = false;
    private GameObject currentShockwave;
    private Animator animator;
    public GameObject skeletonPrefab; // Das Skelett-Prefab
    public GameObject slimePrefab; // Das Slime-Prefab
    public float spawnRadius = 5f; // Radius um den Boss, in dem Mobs spawnen
    public int maxMobsPerWave = 1; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextStompTime = Time.time + stompCooldown;
        animator = GetComponent<Animator>();
        maxHealth = gameObject.GetComponent<Damage>().Health;
    }
    void Update()
    {
    BossHealth = gameObject.GetComponent<Damage>().Health;    // Prüfen, ob es Zeit für einen neuen Stampf-Angriff ist
    if (Time.time >= nextStompTime && !isStomping)
        {
            StartStomp();
        }
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

            


        // Wenn eine aktive Schockwelle existiert, diese vergrößern
        if (currentShockwave != null)
        {
            GrowShockwave();
        }
    }

    void StartStomp()
    {
        isStomping = true;
        animator.SetTrigger("Stomp");
    }

    void CreateShockwave()
    {
        // Schockwelle am Boden erzeugen
        Debug.Log(transform.position);
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 1.5f;

        currentShockwave = Instantiate(shockwavePrefab, spawnPosition, Quaternion.identity);

        // Cooldown für nächsten Stampf setzen
        nextStompTime = Time.time + stompCooldown;
    }

    void GrowShockwave()
    {
        // Schockwelle vergrößern
        currentShockwave.transform.localScale += new Vector3(1, 0, 1) * shockwaveGrowthRate * Time.deltaTime;

        // Schockwelle löschen, wenn Maximalgröße erreicht
        if (currentShockwave.transform.localScale.x >= shockwaveMaxSize)
        {
            Destroy(currentShockwave);
            currentShockwave = null;
            isStomping = false;
        }
    }
    void SpawnMobs()
    {
        // Anzahl der zu spawnenden Mobs basierend auf der Welle
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




        for (int i = 0; i < mobsToSpawn; i++)
        {
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

            // Zufällige Position im Umkreis des Bosses
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPosition = transform.position + new Vector3(randomDirection.x * spawnRadius, randomDirection.y * spawnRadius, 0.5f);

            // Mob spawnen
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}