using UnityEngine;

public class OverworldZuHöhle : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Collider2D>().enabled = false;
    }
    private void Start()
    {
        Invoke("EnableCollider", 3f);
    }
    private void EnableCollider()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Collider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            sceneController.SceneHöhle();
        }
    }
}
