using UnityEngine;

public class ToggleInGameMenu : MonoBehaviour
{
    // Reference to the in-game menu GameObject
    public GameObject inGameMenu;
    public PotionInHand potionInHand;

    // Update is called once per frame
    void Update()
    {
        // Check if the Esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the visibility of the in-game menu
            inGameMenu.SetActive(!inGameMenu.activeSelf);
            potionInHand.potionName = null;
        }
    }
}
