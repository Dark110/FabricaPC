using UnityEngine;

public class InteractuarConObjeto : MonoBehaviour
{
    public Camera camara;
    public Transform holder;
    public float distanciaRaycast = 3f; // Distancia máxima del raycast
    public float velocidadLerp = 5f; // Velocidad de movimiento al agarrar el objeto
    public KeyCode teclaInteractuar = KeyCode.E; // Tecla para interactuar (agarrar)
    public KeyCode teclaSoltar = KeyCode.Q; // Tecla para soltar el objeto (modificada a Q)

    private GameObject objetoActual; // Objeto que estamos interactuando
    private bool objetoAgarrado = false; // Si el objeto está siendo sostenido
    private Vector3 posicionOriginal; // Posición original del objeto cuando lo agarramos
    private Rigidbody objetoRigidbody; // Referencia al Rigidbody del objeto
    private MoverEnX moverEnXScript; // Referencia al script MoverEnX

    void Update()
    {
        // Raycast desde la cámara hacia adelante
        RaycastHit hit;
        Ray ray = camara.ScreenPointToRay(Input.mousePosition);

        // Si el raycast colisiona con un objeto en el layer "Interactuable"
        if (Physics.Raycast(ray, out hit, distanciaRaycast, LayerMask.GetMask("Interactuable")))
        {
            // Si presionamos la tecla para interactuar y el objeto no está siendo sostenido
            if (Input.GetKeyDown(teclaInteractuar) && !objetoAgarrado)
            {
                AgarrarObjeto(hit.collider.gameObject);
            }
        }

        // Si el objeto está agarrado, hacemos el Lerp para moverlo hacia el holder
        if (objetoAgarrado && objetoActual != null)
        {
            MoverObjetoConLerp();
        }

        // Soltar el objeto si se presiona la tecla "Q"
        if (Input.GetKeyDown(teclaSoltar) && objetoAgarrado)
        {
            SoltarObjeto();
        }
    }

    void AgarrarObjeto(GameObject objeto)
    {
        objetoActual = objeto;
        objetoAgarrado = true;

        // Guardar la posición original del objeto
        posicionOriginal = objeto.transform.position;

        // Hacer que el objeto sea hijo del holder
        objeto.transform.SetParent(holder);

        // Obtener el Rigidbody del objeto
        objetoRigidbody = objeto.GetComponent<Rigidbody>();

        // Verificar si el objeto tiene el script MoverEnX
        moverEnXScript = objeto.GetComponent<MoverEnX>();

        // Si tiene el script MoverEnX, desactivarlo
        if (moverEnXScript != null)
        {
            moverEnXScript.enabled = false;
        }

        if (objetoRigidbody != null)
        {
            // Desactivar la gravedad del objeto al ser agarrado
            objetoRigidbody.useGravity = false;

            // Si tiene kinematic activado, lo desactivamos
            if (objetoRigidbody.isKinematic)
            {
                objetoRigidbody.isKinematic = false;
            }
        }
    }

    void MoverObjetoConLerp()
    {
        // Usamos Lerp para mover el objeto suavemente hacia la posición del holder
        objetoActual.transform.position = Vector3.Lerp(objetoActual.transform.position, holder.position, velocidadLerp * Time.deltaTime);
    }

    void SoltarObjeto()
    {
        if (objetoActual != null)
        {
            objetoActual.transform.SetParent(null);

            if (objetoRigidbody != null)
            {
                objetoRigidbody.useGravity = true;
                objetoRigidbody.isKinematic = false;

                // Establecer velocidad y rotación a cero
                objetoRigidbody.velocity = Vector3.zero;
                objetoRigidbody.angularVelocity = Vector3.zero;
            }

            objetoActual = null;
            objetoAgarrado = false;
            objetoRigidbody = null;
        }
    }

    // Dibujar el raycast en la escena con un gizmo
    void OnDrawGizmos()
    {
        // Si la cámara no está definida, no hacemos nada
        if (camara == null)
            return;

        // Crear un rayo desde la cámara hacia adelante
        Ray ray = camara.ScreenPointToRay(Input.mousePosition);

        // Dibujar una línea desde la cámara hacia donde el raycast colisionaría
        Gizmos.color = Color.red; // Color del raycast en la escena
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * distanciaRaycast);
    }
}
