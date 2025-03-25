using UnityEngine;

public class Holzfälleraxt : MonoBehaviour
{
    // Wird aufgerufen, wenn ein Objekt mit diesem Collider in Berührung kommt
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Prüft, ob der Spieler das Objekt berührt hat
        if (collider.gameObject.CompareTag("Player"))
        {
            // Entfernt die Axt aus der Szene
            Destroy(gameObject);
            // Markiert die Holzfäller-Quest als erledigt im PlayerControls-Skript
            collider.gameObject.GetComponent<PlayerControls>().HolzfällerQuestErledigt();
            // Gibt eine Bestätigungsmeldung in der Konsole aus
            Debug.Log("HolzfällerQuestErledigt");
        }
    }
}