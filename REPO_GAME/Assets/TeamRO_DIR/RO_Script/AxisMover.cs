using System.Collections;
using UnityEngine;

public class AxisMover : MonoBehaviour
{
    public enum MoveAxis { X, Y, Z }

    [Header("Target Settings")]
    [Tooltip("Assign the object you want to move.")]
    public Transform target;

    [Header("Movement Settings")]
    public MoveAxis axis = MoveAxis.Y;
    public float offset = 0.25f;
    public float moveSpeed = 0.1f;
    public float holdTime = 0.1f;
    public bool toggle = false;

    private bool moving = false;
    private bool moved = false;
    private Vector3 originalPos;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("AxisMover: No target assigned.");
            enabled = false;
            return;
        }

        originalPos = target.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Right-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Right-clicked on: " + gameObject.name);
                    if (!moving)
                    {
                        StartCoroutine(MoveSmooth());
                    }
                }
            }
        }
    }

    private Vector3 GetOffsetVector()
    {
        switch (axis)
        {
            case MoveAxis.X: return Vector3.right * offset;
            case MoveAxis.Y: return Vector3.up * offset;
            case MoveAxis.Z: return Vector3.forward * offset;
            default: return Vector3.zero;
        }
    }

    private IEnumerator MoveSmooth()
    {
        moving = true;

        Vector3 startPos = target.localPosition;
        Vector3 endPos;

        if (toggle && moved)
        {
            Debug.Log("Moving back");
            endPos = originalPos;
            moved = false;
        }
        else
        {
            Debug.Log("Moving forward");
            endPos = originalPos + GetOffsetVector();
            moved = true;
        }

        float elapsedTime = 0f;
        while (elapsedTime < moveSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveSpeed);
            target.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        target.localPosition = endPos;

        if (!toggle)
        {
            yield return new WaitForSeconds(holdTime);

            // Return to original position
            startPos = target.localPosition;
            endPos = originalPos;

            elapsedTime = 0f;
            while (elapsedTime < moveSpeed)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / moveSpeed);
                target.localPosition = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }

            target.localPosition = endPos;
        }

        moving = false;
    }
}
