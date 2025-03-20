using UnityEngine;

public class Holzfälleraxt : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
        
        Destroy(gameObject);
        collider.gameObject.GetComponent<PlayerControls>().HolzfällerQuestErledigt();
        Debug.Log("HolzfällerQuestErledigt");
        }
    }
}