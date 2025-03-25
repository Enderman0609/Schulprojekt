using UnityEngine;

public class Healthbar : MonoBehaviour
{
    // Speichert den aktuellen Gesundheitswert des Spielers
    public int PlayerHealth;
    // Referenz auf die UI-Bildkomponente, die als Gesundheitsbalken dient
    private UnityEngine.UI.Image healthbar;
    
    private void Awake()
    {
        // Initialisiert den Gesundheitsbalken durch Abrufen der Image-Komponente
        healthbar = GetComponent<UnityEngine.UI.Image>();
        
    }
    
    public void UpdateHealthbar(int Health)
    {
        // Aktualisiert die Füllmenge des Balkens basierend auf dem Gesundheitswert
        // Teilt durch 120f (maximale Gesundheit), um einen Wert zwischen 0 und 1 zu erhalten
        healthbar.fillAmount = Health / 120f; // Konvertierung zu float, um Ganzzahldivision zu vermeiden
    }
}