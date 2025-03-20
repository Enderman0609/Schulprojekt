using UnityEngine;

public class Holzquest : MonoBehaviour
{

    public bool AxtQuestErledigt;
    void Update()
    {
        AxtQuestErledigt = PlayerPrefs.GetInt("AxtQuestErledigt",0) == 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (AxtQuestErledigt==true)
            {
                Destroy(gameObject);
            }
 
        }
    }
}
