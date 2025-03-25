using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls; // Referenz auf die Spielersteuerung
    public int Raum = 1; // Speichert den aktuellen Raum/Szene des Spielers

    public void SavePlayerData()
    {
        // Speichern des Spieler-Gesundheitszustands
        PlayerPrefs.SetInt("PlayerHealth", playerControls.PlayerHealth);
        // Speichern des Quest-Status (als 1 für true, 0 für false)
        PlayerPrefs.SetInt("BogenQuestErledigt", playerControls.BogenQuestErledigt ? 1 : 0);
        PlayerPrefs.SetInt("AxtQuestErledigt", playerControls.AxtQuestErledigt ? 1 : 0);
        // Speichern des aktuellen Raums
        PlayerPrefs.SetInt("AktuellerRaum", Raum);
        // Speichern der Spielerpositionen für verschiedene Räume
        PlayerPrefs.SetFloat("PlayerPosition1X", playerControls.PlayerPositionX1);
        PlayerPrefs.SetFloat("PlayerPosition1Y", playerControls.PlayerPositionY1);
        PlayerPrefs.SetFloat("PlayerPosition2X", playerControls.PlayerPositionX2);
        PlayerPrefs.SetFloat("PlayerPosition2Y", playerControls.PlayerPositionY2);
        PlayerPrefs.SetFloat("PlayerPosition3X", playerControls.PlayerPositionX3);
        PlayerPrefs.SetFloat("PlayerPosition3Y", playerControls.PlayerPositionY3);
        PlayerPrefs.SetFloat("PlayerPosition4X", playerControls.PlayerPositionX4);
        PlayerPrefs.SetFloat("PlayerPosition4Y", playerControls.PlayerPositionY4);
        PlayerPrefs.SetFloat("PlayerPosition5X", playerControls.PlayerPositionX5);
        PlayerPrefs.SetFloat("PlayerPosition5Y", playerControls.PlayerPositionY5);
        // Speichern der Daten auf die Festplatte
        PlayerPrefs.Save();
        // Debug-Ausgaben zur Überprüfung
        Debug.Log("PlayerHealth: " + playerControls.PlayerHealth);
        Debug.Log("AktuellerRaum: " + Raum);
    }
    public void SceneOverworld()
    {
        Raum = 1; // Setzt den aktuellen Raum auf Overworld
        SavePlayerData(); // Speichert Spielerdaten vor dem Szenenwechsel
        SceneManager.LoadSceneAsync(0); // Lädt die Overworld-Szene
    }
    public void SceneHöhle()
    {
        Raum = 2; // Setzt den aktuellen Raum auf Höhle
        SavePlayerData(); // Speichert Spielerdaten vor dem Szenenwechsel
        SceneManager.LoadSceneAsync(1); // Lädt die Höhlen-Szene
        
    }
    public void SceneBoss()
    {
        Raum = 3; // Setzt den aktuellen Raum auf Boss-Raum
        SavePlayerData(); // Speichert Spielerdaten vor dem Szenenwechsel
        SceneManager.LoadSceneAsync(2); // Lädt die Boss-Szene
    }
    public void SceneBirke()
    {
        Raum = 4; // Setzt den aktuellen Raum auf Birken-Bereich
        SavePlayerData(); // Speichert Spielerdaten vor dem Szenenwechsel
        SceneManager.LoadSceneAsync(3); // Lädt die Birken-Szene
    }
    public void SceneFichte()
    {
        Raum = 5; // Setzt den aktuellen Raum auf Fichten-Bereich
        SavePlayerData(); // Speichert Spielerdaten vor dem Szenenwechsel
        SceneManager.LoadSceneAsync(4); // Lädt die Fichten-Szene
    }
}
