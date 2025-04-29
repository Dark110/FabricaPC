using UnityEngine;

public class MoverEnX : MonoBehaviour
{
    public float velocidad = 2f;
    public bool moverALaDerecha = true;

    void Update()
    {
        float direccion = moverALaDerecha ? 1f : -1f;
        transform.Translate(Vector3.right * direccion * velocidad * Time.deltaTime);
    }
}
