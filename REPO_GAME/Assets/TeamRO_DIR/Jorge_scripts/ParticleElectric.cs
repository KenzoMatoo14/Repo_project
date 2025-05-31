using UnityEngine;

public class ParticleElectric : MonoBehaviour
{
    public GameObject particlePrefab; // Prefab del sistema de part�culas
    private GameObject currentParticles; // Referencia a las part�culas instanciadas
    private bool isEffectActive = false; // Controla si el efecto est� activo
    private float effectTimer = 0f; // Temporizador del efecto
    public float effectTime = 1f;

    void Update()
    {
        // Si el efecto est� activo, cuenta el tiempo y lo desactiva despu�s de 1 segundo
        if (isEffectActive)
        {
            effectTimer += Time.deltaTime;

            if (effectTimer >= effectTime)
            {
                StopEffect();
            }
        }
    }

    // M�todo llamado cuando ocurre una colisi�n
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene un tag (opcional, ajusta seg�n tu juego)
        if (other.CompareTag("Player"))
        {
            StartEffect();
        }
    }

    // Activa el efecto de part�culas
    public void StartEffect()
    {
        if (!isEffectActive)
        {
            Debug.Log("PARTICLE INSTANTIATED");
            // Instancia las part�culas y las guarda en currentParticles
            currentParticles = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            isEffectActive = true;
            effectTimer = 0f; // Reinicia el temporizador
        }
    }

    // Detiene el efecto y destruye las part�culas
    private void StopEffect()
    {
        if (currentParticles != null)
        {
            Destroy(currentParticles); // Destruye las part�culas
        }
        isEffectActive = false;
    }
}