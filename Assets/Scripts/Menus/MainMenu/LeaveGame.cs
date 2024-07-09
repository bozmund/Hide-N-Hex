using CraftingSystem;
using Player;
using Scriptable_Objects;
using Scriptable_Objects.WorldTime;
using UnityEngine;
using UnityEngine.UIElements;

namespace MainMenu
{
    public class LeaveGameButtonHandler : MonoBehaviour
    {
        public EnableStartButtonStatus enableStartButtonStatus;
        public HealthValue healthValue;  
        public SuspicionValue suspicionValue; 
        public PlayerPosition playerPosition;
        public VolumeValue volumeValue;
        public MainInventory mainInventory;
        public PotionInHand potionInHand;
        public CatCalled catCalled;
        public RecipeData recipeData;
        public WorldTimeSo worldTimeSO;
        public NPCWaypoints nPCWaypointsFirst;
        public NPCWaypoints nPCWaypointsSecond;
        public NPCWaypoints nPCWaypointsThird;
        public NPCWaypoints nPCWaypointsFourth;
        public NPCWaypoints nPCWaypointsFifth;
        public NPCWaypoints nPCWaypointsSixth;
        public NPCWaypoints pathToForest;

        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            var leaveGameButton = root.Q<Button>("LeaveGame");
            leaveGameButton.clicked += LeaveGame;
        }

        private void LeaveGame()
        {
            // Save each ScriptableObject before quitting
            SaveManager.SaveData(enableStartButtonStatus);
            SaveManager.SaveData(healthValue);
            SaveManager.SaveData(suspicionValue);
            SaveManager.SaveData(playerPosition);
            SaveManager.SaveData(volumeValue);
            SaveManager.SaveData(mainInventory);
            SaveManager.SaveData(potionInHand);
            SaveManager.SaveData(catCalled);
            SaveManager.SaveData(recipeData);
            SaveManager.SaveData(worldTimeSO);
            SaveManager.SaveData(nPCWaypointsFirst);
            SaveManager.SaveData(nPCWaypointsSecond);
            SaveManager.SaveData(nPCWaypointsThird);
            SaveManager.SaveData(nPCWaypointsFourth);
            SaveManager.SaveData(nPCWaypointsFifth);
            SaveManager.SaveData(nPCWaypointsSixth);
            SaveManager.SaveData(pathToForest);
            Application.Quit();
        }
    }
}
