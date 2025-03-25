using UnityEngine;

public class ResetKnopf : MonoBehaviour
{
    // Referenz auf den SceneController für Szenenwechsel
    [SerializeField] private SceneController sceneController;
    [SerializeField] private PlayerControls playerControls;
    
    void Update()
    {
        // Prüft, ob die Escape-Taste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Löscht alle gespeicherten PlayerPrefs-Daten
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            
            // Setzt die Spielerposition auf den Ursprung zurück
            playerControls.transform.position = new Vector2(0, 0);
            
            // Setzt alle gespeicherten Spielerpositionen für alle Räume zurück
            PlayerPrefs.SetFloat("PlayerPosition1X", 0);
            PlayerPrefs.SetFloat("PlayerPosition1Y", 0);
            PlayerPrefs.SetFloat("PlayerPosition2X", 0);
            PlayerPrefs.SetFloat("PlayerPosition2Y", 0);
            PlayerPrefs.SetFloat("PlayerPosition3X", 0);
            PlayerPrefs.SetFloat("PlayerPosition3Y", 0);
            PlayerPrefs.SetFloat("PlayerPosition4X", 0);
            PlayerPrefs.SetFloat("PlayerPosition4Y", 0);
            PlayerPrefs.SetFloat("PlayerPosition5X", 0);
            PlayerPrefs.SetFloat("PlayerPosition5Y", 0);
            
            // Setzt den Quest-Fortschritt zurück
            PlayerPrefs.SetInt("AxtQuestErledigt", 0);
            PlayerPrefs.SetInt("BogenQuestErledigt", 0);
            
            // Setzt den aktuellen Raum auf den Startraum zurück
            PlayerPrefs.SetInt("AktuellerRaum", 1);
            
            // Setzt die Spielergesundheit auf den Maximalwert zurück
            PlayerPrefs.SetInt("PlayerHealth", 120);
            
            // Speichert alle Änderungen
            PlayerPrefs.Save();
            
        }
    }
}
