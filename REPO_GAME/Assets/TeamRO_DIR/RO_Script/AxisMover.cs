using System.Collections;
using UnityEngine;

public class AxisMover : MonoBehaviour
{
    public enum MoveAxis { X, Y, Z }

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
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Left-clicked on: " + gameObject.name);
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

        Vector3 startPos = transform.localPosition;
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
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        transform.localPosition = endPos;

        if (!toggle)
        {
            yield return new WaitForSeconds(holdTime);

            // Return to original position
            startPos = transform.localPosition;
            endPos = originalPos;

            elapsedTime = 0f;
            while (elapsedTime < moveSpeed)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / moveSpeed);
                transform.localPosition = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }

            transform.localPosition = endPos;
        }

        moving = false;
    }
}
