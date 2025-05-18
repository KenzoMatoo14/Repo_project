using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace KeypadSystem // You can change this to whatever name you prefer
{
    public class SimpleKeypadChecker : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted;

        [Header("Buttons to Press")]
        [SerializeField] private List<SimpleButton> buttons = new List<SimpleButton>();

        [Header("Visuals")]
        [SerializeField] private string accessGrantedText = "OK";
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f;

        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f);
        [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f, 1f);

        [Header("Components")]
        [SerializeField] private Renderer panelMesh;
        [SerializeField] private TMP_Text keypadDisplayText;

        private bool accessGranted = false;

        private void Awake()
        {
            if (panelMesh == null)
                Debug.LogError("SimpleKeypadChecker: panelMesh is not assigned!");
            if (keypadDisplayText == null)
                Debug.LogError("SimpleKeypadChecker: keypadDisplayText is not assigned!");

            foreach (SimpleButton btn in buttons)
            {
                if (btn != null)
                    btn.RegisterWithKeypad(this);
                else
                    Debug.LogWarning("SimpleKeypadChecker: One of the buttons in the list is null!");
            }

            if (panelMesh != null)
                panelMesh.material.SetColor("_EmissionColor", screenNormalColor * screenIntensity);

            UpdateKeypadDisplay();
        }

        public void NotifyButtonPressedFromButton()
        {
            if (accessGranted)
                return;

            foreach (SimpleButton btn in buttons)
            {
                if (btn == null || !btn.IsPressed)
                {
                    UpdateKeypadDisplay();
                    return;
                }
            }

            accessGranted = true;
            if (keypadDisplayText != null)
                keypadDisplayText.text = accessGrantedText;
            if (panelMesh != null)
                panelMesh.material.SetColor("_EmissionColor", screenGrantedColor * screenIntensity);

            onAccessGranted?.Invoke();
        }

        public void UpdateKeypadDisplay()
        {
            int pressedCount = 0;
            foreach (SimpleButton btn in buttons)
            {
                if (btn != null && btn.IsPressed)
                    pressedCount++;
            }

            if (keypadDisplayText != null)
                keypadDisplayText.text = $"{pressedCount}/{buttons.Count}";
        }
    }
}
