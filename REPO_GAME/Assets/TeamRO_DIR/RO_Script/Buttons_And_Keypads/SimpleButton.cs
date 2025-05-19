using UnityEngine;

namespace KeypadSystem
{
    public class SimpleButton : MonoBehaviour
    {
        [SerializeField] private string buttonId;

        [SerializeField] private SimpleKeypadChecker keypad;

        public bool IsPressed { get; private set; }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Right-click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform == transform)
                    {
                        PressButton();
                    }
                }
            }
        }

        public void RegisterWithKeypad(SimpleKeypadChecker targetKeypad)
        {
            keypad = targetKeypad;
        }

        public void PressButton()
        {
            if (IsPressed) return;

            IsPressed = true;
            keypad.UpdateKeypadDisplay();
        }

        public void ResetButton()
        {
            IsPressed = false;
        }
    }
}
