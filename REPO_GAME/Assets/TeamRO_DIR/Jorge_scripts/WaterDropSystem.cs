using System.Collections;
using UnityEngine;

public class WaterDropSystem : MonoBehaviour
{
    [Header("Control de Gotas")]
    [Range(0.5f, 10f)]
    public float intervaloMinimo = 2f;

    [Range(0.5f, 10f)]
    public float intervaloMaximo = 5f;

    [Range(1, 20)]
    public int gotasPorVez = 3;

    [Header("Configuraci�n")]
    public bool iniciarAutomaticamente = true;
    public ParticleSystem sistemaGotas;

    [Header("Sonido (Opcional)")]
    public AudioSource audioGotas;
    public AudioClip[] sonidosGota;

    private bool goteando = false;
    private Coroutine corutinaGotas;

    void Awake()
    {
        if (sistemaGotas == null)
        {
            sistemaGotas = GetComponent<ParticleSystem>();
        }

        if (sistemaGotas == null)
        {
            Debug.LogError("No se encontr� ParticleSystem para gotas en " + gameObject.name);
            enabled = false;
            return;
        }

        ConfigurarSistemaGotas();
    }

    void Start()
    {
        if (iniciarAutomaticamente)
        {
            IniciarGotas();
        }
    }

    void ConfigurarSistemaGotas()
    {
        var main = sistemaGotas.main;
        main.playOnAwake = false;

        // Desactivar emisi�n autom�tica para control manual
        var emission = sistemaGotas.emission;
        emission.enabled = false;

        sistemaGotas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void IniciarGotas()
    {
        if (!goteando)
        {
            goteando = true;
            corutinaGotas = StartCoroutine(GotearContinuamente());
            Debug.Log("Iniciando gotas de agua");
        }
    }

    public void DetenerGotas()
    {
        if (goteando)
        {
            goteando = false;

            if (corutinaGotas != null)
            {
                StopCoroutine(corutinaGotas);
                corutinaGotas = null;
            }

            sistemaGotas.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Debug.Log("Deteniendo gotas de agua");
        }
    }

    IEnumerator GotearContinuamente()
    {
        while (goteando)
        {
            // Tiempo aleatorio entre gotas
            float tiempoEspera = Random.Range(intervaloMinimo, intervaloMaximo);
            yield return new WaitForSeconds(tiempoEspera);

            // Emitir gotas
            EmitirGotas();
        }
    }

    void EmitirGotas()
    {
        if (sistemaGotas != null)
        {
            // Cantidad aleatoria de gotas
            int cantidad = Random.Range(1, gotasPorVez + 1);
            sistemaGotas.Emit(cantidad);

            // Reproducir sonido si est� configurado
            ReproducirSonidoGota();
        }
    }

    void ReproducirSonidoGota()
    {
        if (audioGotas != null && sonidosGota != null && sonidosGota.Length > 0)
        {
            // Seleccionar sonido aleatorio
            AudioClip sonidoAleatorio = sonidosGota[Random.Range(0, sonidosGota.Length)];

            // Variar el pitch para m�s variedad
            audioGotas.pitch = Random.Range(0.8f, 1.2f);
            audioGotas.PlayOneShot(sonidoAleatorio);
        }
    }

    // M�todos p�blicos
    public void CambiarIntervalo(float min, float max)
    {
        intervaloMinimo = min;
        intervaloMaximo = max;
    }

    public void CambiarCantidadGotas(int cantidad)
    {
        gotasPorVez = cantidad;
    }

    public void AlternarGotas()
    {
        if (goteando)
        {
            DetenerGotas();
        }
        else
        {
            IniciarGotas();
        }
    }

    public void GotaManual()
    {
        EmitirGotas();
    }
}