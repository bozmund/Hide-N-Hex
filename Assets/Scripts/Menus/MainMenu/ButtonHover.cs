using UnityEngine;
using UnityEngine.UIElements;

namespace MainMenu
{
    public class ButtonHoverHandler : MonoBehaviour
    {
        private float startGameButtonHoverWidth = 300f;
        private float startGameButtonHoverHeight = 100f;

        private float settingsButtonHoverWidth = 300f;
        private float settingsButtonHoverHeight = 100f;

        private float leaveGameButtonHoverWidth = 300f;
        private float leaveGameButtonHoverHeight = 100f;

        private float buttonOriginalWidth = 287f;
        private float buttonOriginalHeight = 60f;


        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            var startGameButton = root.Q<Button>("StartGame");
            var settingsButton = root.Q<Button>("Settings");
            var leaveGameButton = root.Q<Button>("LeaveGame");

            startGameButton.RegisterCallback<MouseOverEvent>(evt => OnButtonHover(evt, startGameButton, startGameButtonHoverWidth, startGameButtonHoverHeight));
            startGameButton.RegisterCallback<MouseOutEvent>(evt => OnButtonHoverEnd(evt, startGameButton));

            settingsButton.RegisterCallback<MouseOverEvent>(evt => OnButtonHover(evt, settingsButton, settingsButtonHoverWidth, settingsButtonHoverHeight));
            settingsButton.RegisterCallback<MouseOutEvent>(evt => OnButtonHoverEnd(evt, settingsButton));

            leaveGameButton.RegisterCallback<MouseOverEvent>(evt => OnButtonHover(evt, leaveGameButton, leaveGameButtonHoverWidth, leaveGameButtonHoverHeight));
            leaveGameButton.RegisterCallback<MouseOutEvent>(evt => OnButtonHoverEnd(evt, leaveGameButton));
        }

        private void OnButtonHover(MouseOverEvent evt, Button button, float hoverWidth, float hoverHeight)
        {
            button.style.width = hoverWidth;
            button.style.height = hoverHeight;
        }

        private void OnButtonHoverEnd(MouseOutEvent evt, Button button)
        {
            button.style.width = buttonOriginalWidth;
            button.style.height = buttonOriginalHeight;
        }
    }
}
