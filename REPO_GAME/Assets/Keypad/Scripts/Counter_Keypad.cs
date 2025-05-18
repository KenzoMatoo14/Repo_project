using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NavKeypad
{
    public class Counter_Keypad : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted;
        [SerializeField] private UnityEvent onAccessDenied;

        public UnityEvent OnAccessGranted => onAccessGranted;
        public UnityEvent OnAccessDenied => onAccessDenied;

        [Header("Button Config")]
        [Tooltip("Number of unique buttons that must be pressed")]
        [SerializeField] private int requiredUniquePresses = 6;

        [Tooltip("List of valid button references")]
        [SerializeField] private List<Counter_KeypadButton> validButtons = new List<Counter_KeypadButton>();

        [Header("Enter Button")]
        [SerializeField] private Counter_KeypadButton enterButton;

        [Header("Visuals")]
        [SerializeField] private string accessGrantedText = "OK";
        [SerializeField] private string accessDeniedText = "X";
        [SerializeField] private string waitingForEnterText = "Press Enter";
        [SerializeField] private float displayResultTime = 1f;
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f;

        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f);
        [SerializeField] private Color screenDeniedColor = new Color(1f, 0f, 0f, 1f);
        [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f, 1f);
        [SerializeField] private Color screenWaitingColor = new Color(0.5f, 0.5f, 1f, 1f);

        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx;
        [SerializeField] private AudioClip accessDeniedSfx;
        [SerializeField] private AudioClip accessGrantedSfx;
        [SerializeField] private AudioClip enterPressedSfx;

        [Header("Component References")]
        [SerializeField] private Renderer panelMesh;
        [SerializeField] private TMP_Text keypadDisplayText;
        [SerializeField] private AudioSource audioSource;

        private HashSet<string> pressedButtons = new HashSet<string>();
        private bool accessWasGranted = false;
        private bool displayingResult = false;
        private bool waitingForEnter = false;

        private void Awake()
        {
            InitializeKeypad();
        }

        private void InitializeKeypad()
        {
            if (validButtons.Count < requiredUniquePresses)
            {
                Debug.LogWarning($"Not enough valid buttons assigned. Need {requiredUniquePresses}, have {validButtons.Count}");
            }

            
            panelMesh.material.SetColor("_EmissionColor", screenNormalColor * screenIntensity);
        }

        private void Start()
        {
            ClearState();
        }

        public void AddInput(string input)
        {
            Debug.Log("Current input: " + input);

            if (displayingResult || accessWasGranted) return;

            // Handle enter button press
            if (input == "enter")
            {
                HandleEnterPress();
                return;
            }

            // Handle regular button press
            if (!waitingForEnter && !pressedButtons.Contains(input))
            {
                audioSource?.PlayOneShot(buttonClickedSfx);
                pressedButtons.Add(input);
                UpdateDisplay();

                if (pressedButtons.Count >= requiredUniquePresses)
                {
                    ReadyForEnter();
                }
            }
        }

        private void HandleEnterPress()
        {
            if (!waitingForEnter) return;

            audioSource?.PlayOneShot(enterPressedSfx);

            bool granted = pressedButtons.Count >= requiredUniquePresses;
            StartCoroutine(DisplayResultRoutine(granted));
        }

        private void ReadyForEnter()
        {
            waitingForEnter = true;
            keypadDisplayText.text = waitingForEnterText;
            panelMesh.material.SetColor("_EmissionColor", screenWaitingColor * screenIntensity);
        }

        private void UpdateDisplay()
        {
            keypadDisplayText.text = $"{pressedButtons.Count}/{requiredUniquePresses}";
        }

        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true;

            if (granted)
                AccessGranted();
            else
                AccessDenied();

            yield return new WaitForSeconds(displayResultTime);

            displayingResult = false;

            if (!granted)
            {
                ClearState();
            }
        }

        private void AccessGranted()
        {
            accessWasGranted = true;
            keypadDisplayText.text = accessGrantedText;
            panelMesh.material.SetColor("_EmissionColor", screenGrantedColor * screenIntensity);
            audioSource?.PlayOneShot(accessGrantedSfx);
            onAccessGranted?.Invoke();
        }

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText;
            panelMesh.material.SetColor("_EmissionColor", screenDeniedColor * screenIntensity);
            audioSource?.PlayOneShot(accessDeniedSfx);
            onAccessDenied?.Invoke();
        }

        private void ClearState()
        {
            Debug.Log("CLEAR STATE CALLED");
            pressedButtons.Clear();
            waitingForEnter = false;
            keypadDisplayText.text = $"0/{requiredUniquePresses}";
            keypadDisplayText.ForceMeshUpdate();
            panelMesh.material.SetColor("_EmissionColor", screenNormalColor * screenIntensity);
        }
    }
}