using UnityEngine;

public class ParticleElectric : MonoBehaviour
{
    public GameObject particlePrefab; // Prefab del sistema de partículas
    private GameObject currentParticles; // Referencia a las partículas instanciadas
    private bool isEffectActive = false; // Controla si el efecto está activo
    private float effectTimer = 0f; // Temporizador del efecto

    void Update()
    {
        // Si el efecto está activo, cuenta el tiempo y lo desactiva después de 1 segundo
        if (isEffectActive)
        {
            effectTimer += Time.deltaTime;

            if (effectTimer >= 1f)
            {
                StopEffect();
            }
        }
    }

    // Método llamado cuando ocurre una colisión
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisiona tiene un tag (opcional, ajusta según tu juego)
        if (collision.gameObject.CompareTag("Player"))
        {
            StartEffect();
        }
    }

    // Activa el efecto de partículas
    private void StartEffect()
    {
        if (!isEffectActive)
        {
            // Instancia las partículas y las guarda en currentParticles
            currentParticles = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            isEffectActive = true;
            effectTimer = 0f; // Reinicia el temporizador
        }
    }

    // Detiene el efecto y destruye las partículas
    private void StopEffect()
    {
        if (currentParticles != null)
        {
            Destroy(currentParticles); // Destruye las partículas
        }
        isEffectActive = false;
    }
}