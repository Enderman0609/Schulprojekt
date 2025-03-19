using UnityEngine;

public class Bossbar : MonoBehaviour
{
    public int Health;
    private UnityEngine.UI.Image bossbar;
    
    private void Awake()
    {
        // Initialize the healthbar by getting the Image component
        bossbar = GetComponent<UnityEngine.UI.Image>();
        
        // If the Image is on a child object, use this instead:
        // healthbar = GetComponentInChildren<UnityEngine.UI.Image>();
    }
    
    public void UpdateBossbar(int Health)
    {
        bossbar.fillAmount = Health / 1200f; // Convert to float to avoid integer division
    }
}