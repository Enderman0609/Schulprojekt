using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    public int Raum = 1;
    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("PlayerHealth", playerControls.PlayerHealth);
        PlayerPrefs.SetInt("BogenQuestErledigt", playerControls.BogenQuestErledigt ? 1 : 0);
        PlayerPrefs.SetInt("AxtQuestErledigt", playerControls.AxtQuestErledigt ? 1 : 0);
        PlayerPrefs.SetInt("AktuellerRaum", Raum);
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
        PlayerPrefs.Save();
        Debug.Log("PlayerHealth: " + playerControls.PlayerHealth);
        Debug.Log("AktuellerRaum: " + Raum);
    }
    public void SceneOverworld()
    {
        Raum = 1;
        SavePlayerData();
        SceneManager.LoadSceneAsync(0);
    }
    public void SceneHöhle()
    {
        Raum = 2;
        SavePlayerData();
        SceneManager.LoadSceneAsync(1);
        
    }
    public void SceneBoss()
    {
        Raum = 3;
        SavePlayerData();
        SceneManager.LoadSceneAsync(2); 
    }
    public void SceneBirke()
    {
        Raum = 4;
        SavePlayerData();
        SceneManager.LoadSceneAsync(3);
    }
    public void SceneFichte()
    {
        Raum = 5;
        SavePlayerData();
        SceneManager.LoadSceneAsync(4);
    }
}
