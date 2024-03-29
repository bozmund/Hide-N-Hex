using UnityEngine;
using UnityEngine.UIElements;

public class LeaveGameButtonHandler : MonoBehaviour
{
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button leaveGameButton = root.Q<Button>("LeaveGame");
        leaveGameButton.clicked += LeaveGame;
    }

    private void LeaveGame()
    {
        Application.Quit();
    }
}
