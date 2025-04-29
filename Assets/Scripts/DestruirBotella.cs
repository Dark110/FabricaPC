using UnityEngine;

public class DestruirBotella : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra tiene el tag "Botella"
        if (other.CompareTag("Botella"))
        {
            // Destruir el objeto con el tag "Botella"
            Destroy(other.gameObject);
        }
    }
}
