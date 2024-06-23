using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ButtonInCabinCrafting : MonoBehaviour
{
    public List<Button> buttons;
    private GameObject player;
    private GameObject craftingZone;
    private bool isPlayerInCraftingZone;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CheckSceneAndToggleButton(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckSceneAndToggleButton(scene, mode);
    }

    void CheckSceneAndToggleButton(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Cabin")
        {
            FindPlayerAndCraftingZone();
            ToggleButtons(player != null && craftingZone != null && isPlayerInCraftingZone);
        }
        else
        {
            ToggleButtons(false);
        }
    }

    void FindPlayerAndCraftingZone()
    {
        player = GameObject.Find("Player");
        craftingZone = GameObject.Find("CraftingZone");

        if (craftingZone != null)
        {
            CraftingZoneTrigger trigger = craftingZone.GetComponent<CraftingZoneTrigger>();
            if (trigger == null)
            {
                trigger = craftingZone.AddComponent<CraftingZoneTrigger>();
                trigger.Initialize(this);
            }
        }
    }

    public void SetPlayerInCraftingZone(bool isInZone)
    {
        isPlayerInCraftingZone = isInZone;
        CheckSceneAndToggleButton(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void ToggleButtons(bool isActive)
    {
        foreach (Button button in buttons)
        {
            if (button != null && button.gameObject != null)
            {
                button.gameObject.SetActive(isActive);
            }
        }
    }
}

public class CraftingZoneTrigger : MonoBehaviour
{
    private ButtonInCabinCrafting buttonInCabinCrafting;

    public void Initialize(ButtonInCabinCrafting buttonInCabinCrafting)
    {
        this.buttonInCabinCrafting = buttonInCabinCrafting;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            buttonInCabinCrafting.SetPlayerInCraftingZone(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            buttonInCabinCrafting.SetPlayerInCraftingZone(false);
        }
    }
}
