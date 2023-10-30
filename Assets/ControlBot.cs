using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlBot : MonoBehaviour
{
    public int hp;
    private GameObject jugador;
    public int rapidez;
    public ControlJuego controlJuego;
    void Start()
    {
        hp = 100;
        jugador = GameObject.Find("Player");
        controlJuego = GameObject.Find("GameManager").GetComponent<ControlJuego>();
    }
    public void recibirDaño()
    {
        hp = hp - 25; 
        if (hp <= 0)
        {
            controlJuego.EnemyDestroy();
            Destroy(this.gameObject);

        }


    }
    private void Update()
    {       
        transform.LookAt(jugador.transform);
        transform.Translate(rapidez * Vector3.forward * Time.deltaTime);            
    }

    private void OnCollisionEnter(Collision collision) 
    { 
        if (collision.gameObject.CompareTag("Bala"))
        {
            recibirDaño();              
        }
        if (collision.gameObject.CompareTag("RestartCollider"))
        {
            Destroy(gameObject);
        }
    }
}

