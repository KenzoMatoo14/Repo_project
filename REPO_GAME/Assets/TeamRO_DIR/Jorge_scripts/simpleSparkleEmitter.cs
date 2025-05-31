using UnityEngine;

public class SimpleSparkEmitter : MonoBehaviour
{
    [Header("Configuraci�n Simple")]
    public float cadaCuantosSegundos = 2f;
    public int cuantasChispas = 10;

    private ParticleSystem particles;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();

        // Desactivar emisi�n autom�tica
        var emission = particles.emission;
        emission.enabled = false;

        // Comenzar a emitir chispas cada X segundos
        InvokeRepeating("HacerChispas", 0f, cadaCuantosSegundos);
    }

    void HacerChispas()
    {
        particles.Emit(cuantasChispas);
        Debug.Log($"�Chispas! ({cuantasChispas} part�culas)");
    }

    // Para detener/reanudar
    public void PararChispas()
    {
        CancelInvoke("HacerChispas");
    }

    public void ReanudarChispas()
    {
        InvokeRepeating("HacerChispas", 0f, cadaCuantosSegundos);
    }
}