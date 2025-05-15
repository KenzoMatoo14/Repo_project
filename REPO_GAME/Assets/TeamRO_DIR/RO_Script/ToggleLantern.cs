using UnityEngine;

public class ToggleLantern : MonoBehaviour
{
    [SerializeField] private GameObject UVLightObject;
    [SerializeField] private GameObject spotLightObject;

    private void Start()
    {
        // Initialize both lights to off state
        SetLightState(UVLightObject, false);
        SetLightState(spotLightObject, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleLight(ref UVLightObject, ref spotLightObject, "UV light");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleLight(ref spotLightObject, ref UVLightObject, "Spotlight");
        }
    }

    private void ToggleLight(ref GameObject lightToToggle, ref GameObject otherLight, string lightName)
    {
        if (lightToToggle != null)
        {
            bool newState = !lightToToggle.activeSelf;
            SetLightState(lightToToggle, newState);

            // Turn off the other light
            if (otherLight != null && otherLight.activeSelf)
            {
                SetLightState(otherLight, false);
            }

            Debug.Log($"{lightName} toggled: {(newState ? "ON" : "OFF")}");
        }
        else
        {
            Debug.LogWarning($"{lightName} object not assigned!");
        }
    }

    private void SetLightState(GameObject lightObject, bool state)
    {
        if (lightObject != null)
        {
            lightObject.SetActive(state);
        }
    }
}