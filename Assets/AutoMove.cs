using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MoveDown", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void MoveDown()
    {
        transform.Translate(Vector3.down);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("kill"))
        {
            Vector3 direction = collision.transform.position - transform.position;
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();

            rbEnemy.AddForce(direction * 15f, ForceMode.Impulse);

        }
    }

}
