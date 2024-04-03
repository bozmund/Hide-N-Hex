using UnityEngine;
using UnityEngine.UIElements;

namespace MainMenu
{
    public class LeaveGameButtonHandler : MonoBehaviour
    {
        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            var leaveGameButton = root.Q<Button>("LeaveGame");
            leaveGameButton.clicked += LeaveGame;
        }

        private static void LeaveGame()
        {
            Application.Quit();
        }
    }
}
