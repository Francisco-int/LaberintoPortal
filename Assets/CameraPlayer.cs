using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraPlayer : MonoBehaviour
{
    public GameObject player;
   // public GameObject winWall;
   // public float speedLookAt;
    Vector3 offset = new Vector3 (-0.25f, 4.03f, 0.3399999f);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        
    }

    /*public void LookAtWall()
    {
        transform.LookAt(winWall.transform.position * Time.deltaTime * speedLookAt);
        StartCoroutine(LookAtPlayer());
    }
    IEnumerator LookAtPlayer()
    {
        yield return new WaitForSeconds(3);
        transform.LookAt(player.transform.position);
    }*/

}
