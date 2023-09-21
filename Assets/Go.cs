using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - player.transform.position;
    }
}
