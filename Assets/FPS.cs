using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPS : MonoBehaviour
{

    public float rapidezDesplazamiento = 10.0f;
    public Camera camaraPrimeraPersona;
    public GameObject proyectil;
    ControlJuego controlJuego;

    // Start is called before the first frame update
    void Start()
    {
        controlJuego = GameObject.Find("GameManager").GetComponent<ControlJuego>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    void Update()
    {
        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * rapidezDesplazamiento;
        float movimientoCostados = Input.GetAxis("Horizontal") * rapidezDesplazamiento;

        movimientoAdelanteAtras *= Time.deltaTime; movimientoCostados *= Time.deltaTime;

        transform.Translate(movimientoCostados, 0, movimientoAdelanteAtras);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camaraPrimeraPersona.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            GameObject pro;
            pro = Instantiate(proyectil, ray.origin, transform.rotation);
            Rigidbody rb = pro.GetComponent<Rigidbody>();
            rb.AddForce(camaraPrimeraPersona.transform.forward * 15, ForceMode.Impulse);
            Destroy(pro, 5);

            if ((Physics.Raycast(ray, out hit) == true) && hit.distance < 5)
            {
                Debug.Log("El rayo tocó al objeto: " + hit.collider.name);
            }
            if (hit.collider.name.Substring(0, 3) == "Bot")
            {
                GameObject objetoTocado = GameObject.Find(hit.transform.name);
                ControlBot scriptObjetoTocado = (ControlBot)objetoTocado.GetComponent(typeof(ControlBot));
                if (scriptObjetoTocado != null)
                {
                    scriptObjetoTocado.recibirDaño();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RestartCollider"))
         {
            SceneManager.LoadScene(0);
        }
    }
}
