using UnityEngine;

public class Destroy : MonoBehaviour
{
  // Diese Klasse ermöglicht das Zerstören eines Spielobjekts
  
  // Entfernt das aktuelle Spielobjekt aus der Szene
  public void DestroyObject()
  {
    Destroy(gameObject);
  }
}
