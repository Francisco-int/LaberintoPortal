using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] points;
    public float rangeRespawnX;
    public float rangeRespawnZ;
    public float CountDownTime;
    public float ChangePosTime;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangePointsPos", 1, ChangePosTime);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ChangePointsPos()
    {
        int mountToRespawn = Random.Range(1, points.Length);

        for (int i = 0; i < mountToRespawn; i++)
        {
            int pointToRespawn = Random.Range(0, points.Length);

            if (!points[pointToRespawn].activeInHierarchy)
            {
                points[pointToRespawn].transform.position = randomPos();
                points[pointToRespawn].gameObject.SetActive(true);
                Debug.Log("Enable");
            }
        }
        StartCoroutine(CountDown());
            

    }
    Vector3 randomPos()
    {
            Vector3 randomPosPoint = new Vector3(Random.Range(rangeRespawnX, -rangeRespawnX),
                                                -2.856158f,
                                                 Random.Range(rangeRespawnZ, -rangeRespawnZ));
            Debug.Log("Pos");
            return randomPosPoint;       
    }

    IEnumerator CountDown()
    {
        Debug.Log("CountDown");
        yield return new WaitForSeconds(CountDownTime);

        int mountToDisable = Random.Range(0, points.Length);
        Debug.Log("Mount to disable: " + mountToDisable);
        for (int i = 0; i < mountToDisable; i++)
        {
            int pointToDisable = Random.Range(0, points.Length);
            Debug.Log("Point disable: " + pointToDisable);

            if (points[pointToDisable].activeInHierarchy)
            {
                points[pointToDisable].gameObject.SetActive(false);
                Debug.Log("Disable");
            }
        }
        /*foreach (var point in points)
        {            
            point.gameObject.SetActive(false);
            Debug.Log("Disable");
        }*/
    }

}
