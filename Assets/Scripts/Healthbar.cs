using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public int PlayerHealth;
    private UnityEngine.UI.Image healthbar;
    
    private void Awake()
    {
        // Initialize the healthbar by getting the Image component
        healthbar = GetComponent<UnityEngine.UI.Image>();
        
    }
    
    public void UpdateHealthbar(int Health)
    {
        healthbar.fillAmount = Health / 120f; // Convert to float to avoid integer division
    }
}