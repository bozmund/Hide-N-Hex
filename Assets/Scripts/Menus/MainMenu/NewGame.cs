using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class NewGameButtonHandler : MonoBehaviour
    {
        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            var newGameButton = root.Q<Button>("NewGame");
            newGameButton.clicked += NewGame;
        }

        private static void NewGame()
        {
            SceneManager.LoadScene("InitialCutscene");
        }
    }
}
