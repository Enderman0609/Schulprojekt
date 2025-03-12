using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public int Health;
    private UnityEngine.UI.Image healthbar;
    
    private void Awake()
    {
        // Initialize the healthbar by getting the Image component
        healthbar = GetComponent<UnityEngine.UI.Image>();
        
        // If the Image is on a child object, use this instead:
        // healthbar = GetComponentInChildren<UnityEngine.UI.Image>();
    }
    
    public void UpdateHealthbar(int Health)
    {
        healthbar.fillAmount = Health / 50f; // Convert to float to avoid integer division
    }
}