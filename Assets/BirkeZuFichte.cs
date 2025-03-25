using UnityEngine;

public class BirkeZuFichte : MonoBehaviour
{
    // Deaktiviert den Collider beim Start des Spiels
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Collider2D>().enabled = false;
    }
    
    // Startet einen Timer, um den Collider nach 3 Sekunden zu aktivieren
    private void Start()
    {
        Invoke("EnableCollider", 3f);
    }
    
    // Aktiviert den Collider als Trigger nach Ablauf des Timers
    private void EnableCollider()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Collider2D>().enabled = true;
    }
    
    // Referenz auf den SceneController für Szenenwechsel
    [SerializeField] private SceneController sceneController;
    
    // Wechselt zur Fichten-Szene, wenn der Spieler den Trigger berührt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sceneController.SceneFichte();
        }
    }
}
