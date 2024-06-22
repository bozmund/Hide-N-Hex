using UnityEngine;

public class CollectRecipe : MonoBehaviour
{
    // Public variables to assign the GameObjects in the Inspector
    public GameObject unlockPurification;
    public GameObject unlockUsefulness;
    public GameObject unlockClothing;

    public GameObject unlockMindVision;
    public GameObject unlockToxicGas;
    public GameObject unlockLiquidFlame;

    public GameObject unlockUselessness;
    public GameObject unlockCatcalling;

    public GameObject unlockMight;
    public GameObject unlockRepair;

    public GameObject unlockHolyGrail;

    // Recipe GameObjects to save state
    public GameObject recipeUsefulnessClothingAndPurification;
    public GameObject recipeLiquidFlameToxicGasAndMindVision;
    public GameObject recipeUselessnessAndCatcalling;
    public GameObject recipeMightAndRepair;
    public GameObject recipeHolyGrail;

    private void Start()
    {
        LoadStates();
    }

    private void OnDisable()
    {
        SaveStates();
    }

    // This function is called when the player collides with another collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Recipe"
        if (collision.gameObject.CompareTag("Recipe"))
        {
            // Set the recipe GameObject to inactive
            collision.gameObject.SetActive(false);

            // Save the state of the collided recipe GameObject
            SaveState(collision.gameObject);

            // Check the name of the collided recipe GameObject and deactivate the corresponding GameObjects
            switch (collision.gameObject.name)
            {
                case "recipeUsefulnessClothingAndPurification":
                    SetInactive(unlockPurification, unlockUsefulness, unlockClothing);
                    break;
                case "recipeLiquidFlameToxicGasAndMindVision":
                    SetInactive(unlockMindVision, unlockToxicGas, unlockLiquidFlame);
                    break;
                case "recipeUselessnessAndCatcalling":
                    SetInactive(unlockUselessness, unlockCatcalling);
                    break;
                case "recipeMightAndRepair":
                    SetInactive(unlockMight, unlockRepair);
                    break;
                case "recipeHolyGrail":
                    if (unlockHolyGrail != null)
                    {
                        unlockHolyGrail.SetActive(false);
                    }
                    break;
            }
        }
    }

    // Helper method to set multiple GameObjects to inactive
    private void SetInactive(params GameObject[] gameObjects)
    {
        foreach (var obj in gameObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
                SaveState(obj); // Save the state of unlock GameObjects
            }
        }
    }

    private void SaveStates()
    {
        SaveState(unlockPurification);
        SaveState(unlockUsefulness);
        SaveState(unlockClothing);
        SaveState(unlockMindVision);
        SaveState(unlockToxicGas);
        SaveState(unlockLiquidFlame);
        SaveState(unlockUselessness);
        SaveState(unlockCatcalling);
        SaveState(unlockMight);
        SaveState(unlockRepair);
        SaveState(unlockHolyGrail);

        // Save state of recipe GameObjects
        SaveState(recipeUsefulnessClothingAndPurification);
        SaveState(recipeLiquidFlameToxicGasAndMindVision);
        SaveState(recipeUselessnessAndCatcalling);
        SaveState(recipeMightAndRepair);
        SaveState(recipeHolyGrail);
    }

    private void LoadStates()
    {
        LoadState(unlockPurification);
        LoadState(unlockUsefulness);
        LoadState(unlockClothing);
        LoadState(unlockMindVision);
        LoadState(unlockToxicGas);
        LoadState(unlockLiquidFlame);
        LoadState(unlockUselessness);
        LoadState(unlockCatcalling);
        LoadState(unlockMight);
        LoadState(unlockRepair);
        LoadState(unlockHolyGrail);

        // Load state of recipe GameObjects
        LoadState(recipeUsefulnessClothingAndPurification);
        LoadState(recipeLiquidFlameToxicGasAndMindVision);
        LoadState(recipeUselessnessAndCatcalling);
        LoadState(recipeMightAndRepair);
        LoadState(recipeHolyGrail);
    }

    private void SaveState(GameObject obj)
    {
        if (obj != null)
        {
            int state = obj.activeSelf ? 1 : 0;
            PlayerPrefs.SetInt(obj.name, state);
            Debug.Log($"Saved state of {obj.name} as {state}");
        }
    }

    // Debugging LoadState method
    private void LoadState(GameObject obj)
    {
        if (obj != null && PlayerPrefs.HasKey(obj.name))
        {
            bool isActive = PlayerPrefs.GetInt(obj.name) == 1;
            obj.SetActive(isActive);
            Debug.Log($"Loaded state of {obj.name} as {isActive}");
        }
        else
        {
            Debug.Log($"No saved state found for {obj.name}");
        }
    }
}
