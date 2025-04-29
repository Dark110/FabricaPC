using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float velocidadRotacionMouse = 2f;
    public float fuerzaSalto = 5f;
    public float gravedad = -9.81f;

    [Header("Configuración del Mouse")]
    public Transform camaraJugador;
    private float rotacionX = 0f;

    private CharacterController controller;
    private Vector3 velocidadVertical;
    private bool estaEnSuelo;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MoverJugador();
        RotarVista();
    }

    void MoverJugador()
    {
        estaEnSuelo = controller.isGrounded;

        if (estaEnSuelo && velocidadVertical.y < 0)
            velocidadVertical.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;
        controller.Move(mover * velocidad * Time.deltaTime);

        // Salto
        if (Input.GetButtonDown("Jump") && estaEnSuelo)
            velocidadVertical.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);

        // Gravedad
        velocidadVertical.y += gravedad * Time.deltaTime;
        controller.Move(velocidadVertical * Time.deltaTime);
    }

    void RotarVista()
    {
        float mouseX = Input.GetAxis("Mouse X") * velocidadRotacionMouse;
        float mouseY = Input.GetAxis("Mouse Y") * velocidadRotacionMouse;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f); // Evita rotar más de lo debido

        camaraJugador.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
