using UnityEngine;

[ExecuteAlways]
public class Reveal : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private GameObject spotLightObject;

    private Light spotLight;

    private void OnEnable()
    {
        if (spotLightObject != null)
            spotLight = spotLightObject.GetComponent<Light>();
    }

    private void Update()
    {
        if (mat == null || spotLightObject == null || spotLight == null)
            return;

        if (!spotLightObject.activeInHierarchy || !spotLight.enabled)
        {
            // Apaga el efecto de revelado si la luz está apagada o desactivada
            mat.SetFloat("_LightAngle", 0f); // o un valor negativo
            return;
        }

        if (spotLight.type != LightType.Spot)
        {
            Debug.LogWarning("La luz asignada no es un spotlight.");
            return;
        }

        // Pasamos parámetros al shader
        mat.SetVector("_LightPos", spotLight.transform.position);
        mat.SetVector("_LightDir", -spotLight.transform.forward);
        mat.SetFloat("_LightAngle", spotLight.spotAngle);
    }
}
