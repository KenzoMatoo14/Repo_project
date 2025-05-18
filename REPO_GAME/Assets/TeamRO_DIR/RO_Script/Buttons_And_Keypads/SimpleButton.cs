using UnityEngine;

namespace KeypadSystem
{
    public class SimpleButton : MonoBehaviour
    {
        [SerializeField] private string buttonId;

        private SimpleKeypadChecker keypad;

        public bool IsPressed { get; private set; }

        public void RegisterWithKeypad(SimpleKeypadChecker targetKeypad)
        {
            keypad = targetKeypad;
        }

        public void PressButton()
        {
            if (IsPressed) return;

            IsPressed = true;
            keypad?.AddInput(buttonId);
        }

        public void ResetButton()
        {
            IsPressed = false;
        }
    }
}
