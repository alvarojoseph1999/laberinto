using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController controller;
    private GameObject camara;

    [Header("Estadisticas Normales")]
    [SerializeField] private float velocidad;
    [SerializeField] private float velCorriendo;
    [SerializeField] private float alturaDeSalto;
    [SerializeField] private float tiempoAlGirar;

    [Header("Datos sobre el piso")]
    [SerializeField] private Transform detectaPiso;
    [SerializeField] private float distanciaPiso;
    [SerializeField] private LayerMask mascaraPiso;

    float velocidadGiro;
    float gravedad = -9.81f;
    Vector3 velocity;
    bool tocaPiso;

    Animator anim;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        camara = GameObject.FindGameObjectWithTag("MainCamera");
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        tocaPiso = Physics.CheckSphere(detectaPiso.position, distanciaPiso, mascaraPiso);

        if (tocaPiso && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && tocaPiso)
        {
            velocity.y = Mathf.Sqrt(alturaDeSalto * -2 * gravedad);
        }

        velocity.y += gravedad * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direccion = new Vector3(horizontal, 0, vertical).normalized;

        
        if(direccion.magnitude <= 0)
        {
            anim.SetFloat("Movimientos", 0, 0.1f, Time.deltaTime);
        }

        if (direccion.magnitude >= 0.1f)
        {
            float objetivoAngulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + camara.transform.eulerAngles.y;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, objetivoAngulo, ref velocidadGiro, tiempoAlGirar);
            transform.rotation = Quaternion.Euler(0, angulo, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                controller.Move(mover.normalized * velCorriendo * Time.deltaTime);

                anim.SetFloat("Movimientos", 1, 0.1f, Time.deltaTime);
            }
            else
            {
                Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                controller.Move(mover.normalized * velocidad * Time.deltaTime);

                anim.SetFloat("Movimientos", 0.5f, 0.1f, Time.deltaTime);


            }
        }
    }
}
