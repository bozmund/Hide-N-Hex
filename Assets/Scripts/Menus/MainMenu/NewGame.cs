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
            // Find the GameObject named "BackgroundMusicCutscene"
            GameObject musicObject = GameObject.Find("BackgroundMusicMain");

            // Check if the object exists
            if (musicObject != null)
            {
                // Destroy the GameObject
                Destroy(musicObject);
                Debug.Log("BackgroundMusic has been destroyed.");
            }

            SceneManager.LoadScene("InitialCutscene");
        }
    }
}
