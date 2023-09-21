using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public GameObject player;
    Vector3 offset = new Vector3 (-0.25f, 4.03f, 0.3399999f);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        //transform.rotation = player.transform.rotation;
    }
}
