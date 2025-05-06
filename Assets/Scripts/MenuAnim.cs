using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    public float zoomSpeed = 0.5f;    // velocidad del zoom
    public float zoomAmount = 0.05f;  // cuanto se hace el zoom (5%)

    private Vector3 initialScale;
    private float time;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        time += Time.deltaTime * zoomSpeed;
        float scaleFactor = 1 + Mathf.Sin(time) * zoomAmount;
        transform.localScale = initialScale * scaleFactor;
    }
}
