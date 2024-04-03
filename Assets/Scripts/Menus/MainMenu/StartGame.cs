using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace MainMenu
{
    public class StartGameButtonHandler : MonoBehaviour
    {
        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            var startGameButton = root.Q<Button>("StartGame");
            startGameButton.clicked += StartGame;
        }

        private static void StartGame()
        {
            SceneManager.LoadScene("OutsideTheCabin");
        }
    }
}
