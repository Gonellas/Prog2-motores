 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPMov : MonoBehaviour
{
    public CharacterController controler;
    public float velocidad = 5f;
    public float correr = 6f;

    public bool estaCorriendo = false;

    public float gravedad = -20f;
    public float alturaDeSalto = 3f;

    public Transform groundCheck;
    public float distanciaPiso = 0.3f;
    public LayerMask groundMask;

    //[SerializeField] GameObject pepeCamina;
    //[SerializeField] GameObject pepeCorre;

    bool banderaCaminar = false;
    bool banderaCorrer = false;

    Vector3 rapidez;

    bool estamosEnPasto;

    float duracion = 0f;
    float tiempoEspera = 4.5f;

    //public AudioSource Clip;


    void Start()
    {


    }


    void Update()
    {

        estamosEnPasto = Physics.CheckSphere(groundCheck.position, distanciaPiso, groundMask);
        if (estamosEnPasto && rapidez.y < 0)
        {
            rapidez.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        //move.Normalize();
        if ((x < 0 && x > 0) || (z < 0 && z > 0))
        {
            move.Normalize();
        }

        controler.Move(move * velocidad * Time.deltaTime);
        if (x != 0 || z != 0)
        {
            banderaCaminar = true;
        }
        else
        {
            banderaCaminar = false;
        }


        if (Input.GetKey(KeyCode.LeftShift) && estaCorriendo == false)
        {
            controler.Move(move * correr * Time.deltaTime);

            duracion = duracion + Time.deltaTime;

            banderaCorrer = true;

            if (duracion >= 4)
            {
                tiempoEspera = 4.5f;
                //print("tiempo de espera" + tiempoEspera);

                estaCorriendo = true;
                //Clip.Play();
            }
        }
        else
        {
            banderaCorrer = false;
        }

        if (estaCorriendo == true)
        {
            tiempoEspera = tiempoEspera - Time.deltaTime;
            //print("resta" + tiempoEspera);
            if (tiempoEspera <= 0f)
            {

                estaCorriendo = false;
                duracion = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && estamosEnPasto)
        {
            rapidez.y = Mathf.Sqrt(alturaDeSalto * -1 * gravedad);
        }

        rapidez.y += gravedad * Time.deltaTime;

        controler.Move(rapidez * Time.deltaTime);

        //Deteccion();

    }
    /*
    void Deteccion()
    {
        if (banderaCaminar == true)
        {
            pepeCamina.SetActive(true);
        }
        else
        {
            pepeCamina.SetActive(false);
        }

        if (banderaCorrer == true)
        {
            pepeCorre.SetActive(true);
        }
        else
        {
            pepeCorre.SetActive(false);
        }

    }
    */
}
