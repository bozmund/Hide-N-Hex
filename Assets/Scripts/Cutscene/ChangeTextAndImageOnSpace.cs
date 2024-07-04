using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeTextAndImageOnSpace : MonoBehaviour
{
    public Text uiText;
    public GameObject uiImage1; // Reference to the first UI Image GameObject
    public GameObject uiImage2; // Reference to the second UI Image GameObject
    public GameObject uiImage3; // Reference to the third UI Image GameObject
    public GameObject secondBackground;
    public HealthValue healthValue; // Reference to the HealthValue ScriptableObject
    public SuspicionValue suspicionValue; // Reference to the SuspicionValue ScriptableObject
    public PotionInHand potionInHand; // Reference to the PotionInHand ScriptableObject
    public PlayerPosition playerPosition; // Reference to the PlayerPosition ScriptableObject
    public MainInventory mainInventory; // Reference to the MainInventory ScriptableObject
    public EnableStartButtonStatus enableStartButtonStatus; // Reference to the EnableStartButtonStatus ScriptableObject

    private int pressCount = 0;

    void Start()
    {
        if (uiText == null)
        {
            Debug.LogError("No Text component assigned in the Inspector.");
        }

        if (uiImage1 == null || uiImage2 == null || uiImage3 == null)
        {
            Debug.LogError("One or more Image GameObjects are not assigned in the Inspector.");
        }

        if (healthValue == null)
        {
            Debug.LogError("No HealthValue ScriptableObject assigned in the Inspector.");
        }

        if (suspicionValue == null)
        {
            Debug.LogError("No SuspicionValue ScriptableObject assigned in the Inspector.");
        }

        if (potionInHand == null)
        {
            Debug.LogError("No PotionInHand ScriptableObject assigned in the Inspector.");
        }

        if (playerPosition == null)
        {
            Debug.LogError("No PlayerPosition ScriptableObject assigned in the Inspector.");
        }

        if (mainInventory == null)
        {
            Debug.LogError("No MainInventory ScriptableObject assigned in the Inspector.");
        }

        if (enableStartButtonStatus == null)
        {
            Debug.LogError("No EnableStartButtonStatus ScriptableObject assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressCount++;
            switch (pressCount)
            {
                case 1:
                    uiText.text = "In the world you can find various collectible plants. Some of them are common and some are uncommon. They are useful for crafting potions.";
                    if (uiImage1 != null)
                    {
                        uiImage1.SetActive(false);
                    }
                    if (uiImage2 != null)
                    {
                        uiImage2.SetActive(false);
                    }
                    if (uiImage3 != null)
                    {
                        uiImage3.SetActive(true);
                    }
                    break;

                case 2:
                    uiText.text = "You will have the main inventory always at your disposal. By clicking \"I\" you will be able to see the entire inventory and the recipes for the potions.";
                    if (uiImage3 != null)
                    {
                        uiImage3.SetActive(false);
                    }
                    break;

                case 3:
                    uiText.text = "Some recipes will be locked and you will need to collect the recipes items so you can unlock the recipes. Left click on the item to see its description and right click on it so you can drop the item.";
                    break;

                case 4:
                    uiText.text = "In some areas you will be able to make a interaction by clicking \"F\". WASD is used for moving.";
                    break;

                case 5:
                    uiText.text = "Only when near the furnace you will be able to craft your potions. Good luck in your journey!";
                    if (secondBackground != null)
                    {
                        secondBackground.SetActive(true);
                    }
                    break;

                case 6:
                    if (healthValue != null)
                    {
                        healthValue.fillAmount = 1; // Set fillAmount to 1
                    }
                    if (suspicionValue != null)
                    {
                        suspicionValue.fillAmount = 0; // Set fillAmount to 0
                    }
                    if (potionInHand != null)
                    {
                        potionInHand.potionName = ""; // Set potionName to empty
                    }
                    if (playerPosition != null)
                    {
                        playerPosition.sceneName = ""; // Set sceneName to empty
                        playerPosition.position = Vector3.zero; // Set position to (0, 0, 0)
                    }
                    if (mainInventory != null)
                    {
                        foreach (var slot in mainInventory.slots)
                        {
                            slot.itemName = ""; // Set itemName to empty
                            slot.count = 0; // Set count to 0
                        }
                    }
                    if (enableStartButtonStatus != null)
                    {
                        enableStartButtonStatus.StartButtonStatus = "enabled"; // Set StartButtonStatus to "enabled"
                    }
                    SceneManager.LoadScene("OutsideTheCabin");
                    break;

                default:
                    break;
            }
        }
    }
}
