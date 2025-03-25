using UnityEngine;

// Diese Klasse steuert die Anzeige der Boss-Lebensleiste
public class Bossbar : MonoBehaviour
{
    // Speichert die aktuelle Gesundheit des Bosses
    public int Health;
    // Referenz auf die UI-Bildkomponente für die Lebensleiste
    private UnityEngine.UI.Image bossbar;
    
    // Wird beim Initialisieren des Objekts aufgerufen
    private void Awake()
    {
        // Initialisiert die Lebensleiste durch Abrufen der Image-Komponente
        bossbar = GetComponent<UnityEngine.UI.Image>();
        
        // Falls das Image auf einem untergeordneten Objekt ist, verwende stattdessen:
        // healthbar = GetComponentInChildren<UnityEngine.UI.Image>();
    }
    
    // Aktualisiert die Anzeige der Boss-Lebensleiste basierend auf der aktuellen Gesundheit
    public void UpdateBossbar(int Health)
    {
        // Berechnet den Füllstand der Lebensleiste (Gesundheit geteilt durch Maximalwert)
        bossbar.fillAmount = Health / 1200f; // Umwandlung in Float, um Ganzzahldivision zu vermeiden
    }
}