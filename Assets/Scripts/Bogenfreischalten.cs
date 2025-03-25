using UnityEngine;

// Diese Klasse steuert das Freischalten des Bogens im Spiel
public class Bogenfreischalten : MonoBehaviour
{
    // Speichert, ob die Bogen-Quest bereits erledigt wurde
    public bool BogenQuestErledigt = false;
    
    // Wird beim Start aufgerufen
    void Start()
    {
       // Deaktiviert den Collider zunächst
       GetComponent<Collider2D>().enabled = false;
       // Aktiviert den Collider nach 3 Sekunden
       Invoke("EnableCollider", 3f);
    }
    
    // Aktiviert den Collider des Objekts
    void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;  
    }
    
    // Wird aufgerufen, wenn ein anderes Objekt mit dem Collider kollidiert
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Überprüft, ob der Spieler mit dem Objekt kollidiert ist
        if (collider.gameObject.CompareTag("Player"))
        {
        // Zerstört dieses Objekt
        Destroy(gameObject);
        // Ruft die BogenQuest-Methode im PlayerControls-Skript des Spielers auf
        collider.gameObject.GetComponent<PlayerControls>().BogenQuest();
        // Gibt eine Debug-Nachricht aus
        Debug.Log("Bogenfreischalten");
        }
    }
}
