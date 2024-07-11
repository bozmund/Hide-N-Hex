using UnityEngine;

public class EmptyHand : MonoBehaviour
{
    public PotionInHand potionInHand; // Public reference to the PotionInHand GameObject

    private void Start()
    {
        potionInHand.potionName = ""; 
    }
    void Update()
    {
        // Check if the 'X' key is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Set the potionInHand reference to null
            potionInHand.potionName = "";
            Debug.Log("Potion in hand has been set to null.");
        }
    }
}