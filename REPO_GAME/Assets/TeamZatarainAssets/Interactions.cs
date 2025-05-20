using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("Configuración")]
    public float interactionDistance = 3f; 
    public LayerMask interactableLayer; 
    public string interactableTag = "Interactable"; 

    [Header("Debug")]
    public bool showDebugRay = true;
    public Color rayColor = Color.green; 

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            TryInteract();
        }
    }

    public void TryInteract()
    {
        
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

       
        if (showDebugRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, rayColor, 1f);
        }

       
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            
            if (hit.collider.CompareTag(interactableTag)) 
            {
                InteractWithObject(hit.collider.gameObject);
            }
        }
    }

    void InteractWithObject(GameObject obj)
    {
        // Ejemplos de interacción (personaliza según las necesidades de sus equipos :)  ):
        
        // 1. Activar/Desactivar objeto
        // obj.SetActive(!obj.activeSelf);
        
        // 2. Ejecutar animación
        // Animator anim = obj.GetComponent<Animator>();
        // if(anim != null) anim.SetTrigger("Activate");
        
        // 3. Mostrar mensaje
        Debug.Log("Interactuando con: " + obj.name);
        
        // 4. Recoger item (desaparecer objeto)
        // Destroy(obj);
        

        //ESTA ES LA QUE LA MAYORIA VA A NECESITAR 
        // 5. Llamar método específico si existe
        // obj.SendMessage("OnPlayerInteract", SendMessageOptions.DontRequireReceiver);
    }

    
    bool QuickInteractCheck()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out var hit, interactionDistance) && 
               hit.collider.CompareTag(interactableTag);
    }
}