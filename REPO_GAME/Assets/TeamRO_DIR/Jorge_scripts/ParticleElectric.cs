using UnityEngine;

public class ParticleElectric : MonoBehaviour
{
    public GameObject particlePrefab; // Prefab del sistema de part�culas
    private GameObject currentParticles; // Referencia a las part�culas instanciadas
    private bool isEffectActive = false; // Controla si el efecto est� activo
    private float effectTimer = 0f; // Temporizador del efecto

    void Update()
    {
        // Si el efecto est� activo, cuenta el tiempo y lo desactiva despu�s de 1 segundo
        if (isEffectActive)
        {
            effectTimer += Time.deltaTime;

            if (effectTimer >= 1f)
            {
                StopEffect();
            }
        }
    }

    // M�todo llamado cuando ocurre una colisi�n
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisiona tiene un tag (opcional, ajusta seg�n tu juego)
        if (collision.gameObject.CompareTag("Player"))
        {
            StartEffect();
        }
    }

    // Activa el efecto de part�culas
    private void StartEffect()
    {
        if (!isEffectActive)
        {
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