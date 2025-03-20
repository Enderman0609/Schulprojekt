using UnityEngine;

public class ResetKnopf : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            sceneController.SceneOverworld();
            GetComponent<DamageController>().PlayerHealth = 120;
            GetComponent<PlayerControls>().transform.position = new Vector2(0, 0);
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
            PlayerPrefs.SetInt("AxtQuestErledigt", 0);
            PlayerPrefs.SetInt("BogenQuestErledigt", 0);
            PlayerPrefs.SetInt("AktuellerRaum", 1);
            PlayerPrefs.SetInt("PlayerHealth", 120);
            PlayerPrefs.Save();

        }
    }
}
