using UnityEngine;
using DigitalRuby.LightningBolt;


public class ElectricitySystem : MonoBehaviour
{
    public GameObject lightningBoltPrefab;
    public Transform startPoint;
    public Transform endPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (lightningBoltPrefab != null && startPoint != null && endPoint != null)
            {
                GameObject bolt = Instantiate(lightningBoltPrefab);
                LightningBoltScript script = bolt.GetComponent<LightningBoltScript>();

                // Asigna directamente los puntos de inicio y fin
                script.StartPosition = startPoint.position;
                script.EndPosition = endPoint.position;
            }
            else
            {
                Debug.LogError("Faltan referencias en el Inspector.");
            }
        }
    }
}