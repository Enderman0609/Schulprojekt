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
    public int maxMobsPerWave = 2; 
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
    if (BossHealth <= 800 && Welle == 0)
            {
                SpawnMobs();
                Debug.Log("Welle 1");
                Welle = 1;
            }
    if (BossHealth <= 600 && Welle == 1)
            {
                SpawnMobs();
                Debug.Log("Welle 2");
                Welle = 2;
            }
    if (BossHealth <= 400 && Welle == 2)
            {
                SpawnMobs();
                Debug.Log("Welle 3");
                Welle = 3;
            }
    if (BossHealth <= 200 && Welle == 3)
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
        // Anzahl der zu spawnenden Mobs basierend auf dem Gesundheitsschwellenwert
        // Je niedriger die Gesundheit, desto mehr Mobs
        int mobsToSpawn = Mathf.Min(maxMobsPerWave, Mathf.RoundToInt(maxMobsPerWave * (maxHealth / BossHealth)));

        for (int i = 0; i < mobsToSpawn; i++)
        {
            // Abwechselnd Skelette und Slimes spawnen
            GameObject prefabToSpawn = (i % 2 == 0) ? skeletonPrefab : slimePrefab;

            // Zufällige Position im Umkreis des Bosses
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPosition = transform.position + new Vector3(randomDirection.x, 0, randomDirection.y) * spawnRadius;

            // Mob spawnen
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}