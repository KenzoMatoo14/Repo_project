using UnityEngine;

public class ParticleSmoke : MonoBehaviour
{
    
    private float distanciaActivacion = 1f;
    private string tagObjetivo = "Player"; // Tag del objeto que activar� las part�culas

    
    public GameObject particlePrefab; // Prefab del sistema de part�culas
    private GameObject currentParticles; // Instancia actual del prefab
    

  
    public bool usarTriggerCollider = false; // Alternativamente usar collider trigger
    public bool mostrarGizmos = true;

  
    private bool particulasActivas = false;
    Transform objetivoTransform;
    void Start()
    {
        // Encontrar el objeto objetivo por tag
        GameObject objetivo = GameObject.FindGameObjectWithTag(tagObjetivo);
        if (objetivo != null)
        {
            objetivoTransform = objetivo.transform;
        }
        else
        {
            Debug.LogWarning("No se encontr� objeto con tag '" + tagObjetivo + "'");
        }
    }

    void Update()
    {
        if (!usarTriggerCollider)
        {
            VerificarDistancia();
        }
    }

    void VerificarDistancia()
    {
        float distancia = Vector3.Distance(transform.position, objetivoTransform.position);

        if (distancia <= distanciaActivacion && !particulasActivas)
        {
            ActivarParticulas();
        }
        else if (distancia > distanciaActivacion && particulasActivas)
        {
            DesactivarParticulas();
        }
    }

    void ActivarParticulas()
    {
        if (!particulasActivas)
        {
            // Instancia las part�culas y las guarda en currentParticles
            currentParticles = Instantiate(particlePrefab, transform.position, Quaternion.Euler(-90,0,0));
            particulasActivas = true;
            
        }
    }

    void DesactivarParticulas()
    {
        if (currentParticles != null)
        {
            Destroy(currentParticles); // Destruye las part�culas
        }
        particulasActivas = false;
    }
    
    // Visualizaci�n en el editor
    void OnDrawGizmosSelected()
    {
        if (mostrarGizmos)
        {
            Gizmos.color = particulasActivas ? Color.green : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, distanciaActivacion);
        }
    }
    }

    

   

