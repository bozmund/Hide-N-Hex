using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Bars
{
    public class ButtonInCabinCrafting : MonoBehaviour
    {
        public List<Button> buttons;
        private GameObject _player;
        private GameObject _craftingZone;
        private bool _isPlayerInCraftingZone;

        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            CheckSceneAndToggleButton(SceneManager.GetActiveScene());
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            CheckSceneAndToggleButton(scene);
        }

        void CheckSceneAndToggleButton(Scene scene)
        {
            if (scene.name == "Cabin")
            {
                FindPlayerAndCraftingZone();
                ToggleButtons(_player != null && _craftingZone != null && _isPlayerInCraftingZone);
            }
            else
            {
                ToggleButtons(false);
            }
        }

        void FindPlayerAndCraftingZone()
        {
            _player = GameObject.Find("Player");
            _craftingZone = GameObject.Find("CraftingZone");

            if (_craftingZone == null) return;
            var trigger = _craftingZone.GetComponent<CraftingZoneTrigger>();
            if (trigger != null) return;
            trigger = _craftingZone.AddComponent<CraftingZoneTrigger>();
            trigger.Initialize(this);
        }

        public void SetPlayerInCraftingZone(bool isInZone)
        {
            _isPlayerInCraftingZone = isInZone;
            CheckSceneAndToggleButton(SceneManager.GetActiveScene());
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
        private ButtonInCabinCrafting _buttonInCabinCrafting;

        public void Initialize(ButtonInCabinCrafting buttonInCabinCrafting)
        {
            _buttonInCabinCrafting = buttonInCabinCrafting;
            var component = GetComponent<Collider2D>();
            if (component != null)
            {
                component.isTrigger = true;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Player")
            {
                _buttonInCabinCrafting.SetPlayerInCraftingZone(true);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == "Player")
            {
                _buttonInCabinCrafting.SetPlayerInCraftingZone(false);
            }
        }
    }
}