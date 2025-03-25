using UnityEngine;

public class Holzquest : MonoBehaviour
{
    // Speichert den Status der Axt-Quest
    public bool AxtQuestErledigt;
    
    // Wird jeden Frame aufgerufen
    void Update()
    {
        // Liest den Quest-Status aus den PlayerPrefs (1 = erledigt, 0 = nicht erledigt)
        AxtQuestErledigt = PlayerPrefs.GetInt("AxtQuestErledigt", 0) == 1;
    }
    
    // Wird ausgelöst, wenn ein Objekt mit diesem Collider in Berührung kommt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Prüft, ob der Spieler das Objekt berührt hat
        if (collision.gameObject.CompareTag("Player"))
        {
            // Wenn die Axt-Quest erledigt ist, wird dieses Objekt zerstört
            if (AxtQuestErledigt == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
