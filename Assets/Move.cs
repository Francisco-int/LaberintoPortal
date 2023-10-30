using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
   // public CameraPlayer cameraPlayer;
    public float speed = 3;
    public int live = 5;
    public TextMeshProUGUI scoreLiveText;
    public GameObject portalWin;
    int points;
    public TextMeshProUGUI textGameOverWin;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI newRecordText;
    public TextMeshProUGUI RecordText;
    public TextMeshProUGUI portalOpen;
    private bool winGameOverAble;
    public float timerRecord;
    public float bestRecord;
    public bool saverAble;
    Vector3 startPos;
    public GameObject[] livesObjects;
    // Start is called before the first frame update
    void Start()
    {
        portalOpen.enabled = false;
        RecordText.enabled = false;
        newRecordText.enabled = false;
        saverAble = true;
        startPos = transform.position;  
        timerRecord = 0f;
        scoreLiveText.enabled = true;
        winGameOverAble = false;
        textGameOverWin.enabled = false;
        points = 0;
        portalWin.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timerRecord += Time.deltaTime;
        timerText.text = "Timer: " + timerRecord.ToString("F2") + "\nBest Record: " + bestRecord.ToString("F2");


        if (Input.GetKeyDown(KeyCode.R) && winGameOverAble)
        {
            Time.timeScale = 1f;
            
            ResetScene();
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    WinScene();
        //}

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalMove);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalMove);

        scoreLiveText.color = Color.blue;
        scoreLiveText.text = "Score: " + points + "\nLives: " + live;

       

        if (live <= 0)
        {
            winGameOverAble = true;
            textGameOverWin.color = Color.red;
            textGameOverWin.text = "You Lost\nPress R to restart";
            timerText.text = "Record: " + timerRecord.ToString("F2");
            textGameOverWin.enabled = true;
            scoreLiveText.enabled = false;

           
            Time.timeScale = 0f;
        }

        if (points >= 5)
        {
            portalWin.gameObject.SetActive(true);
            portalOpen.enabled = true;
           // cameraPlayer.LookAtWall();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            live--;
        }
        if (collision.gameObject.CompareTag("Enemy1"))
        {
            live -= 3;
        }
        if (collision.gameObject.CompareTag("Enemy2"))
        {
            live -= 5;
        }
        if (collision.gameObject.CompareTag("Live"))
        {
            live += 2;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            WinScene();
        }
        if (collision.gameObject.CompareTag("Restart"))
        {
            winGameOverAble = true;
            textGameOverWin.color = Color.red;
            textGameOverWin.text = "You Fell\nPress R to restart";
            timerText.text = "Record: " + timerRecord.ToString("F2");
            textGameOverWin.enabled = true;
            scoreLiveText.enabled = false;
           
            Time.timeScale = 0f;
        }
        if (collision.gameObject.CompareTag("Point"))
        {
            points++;
            collision.gameObject.SetActive(false);
        }
    }
    public void ResetScene()
    {
        
        portalOpen.enabled = false;
        portalWin.gameObject.SetActive(false);
        foreach (var lives in livesObjects)
        {
            lives.gameObject.SetActive(true);
        }
        RecordText.enabled = false;
        timerText.enabled = true;
        newRecordText.enabled = false;
        transform.position = startPos;
        timerRecord = 0f;
        scoreLiveText.enabled = true;
        winGameOverAble = false;
        textGameOverWin.enabled = false;
        points = 0;
        portalWin.gameObject.SetActive(false);
        live = 5;
    }
    private void WinScene()
    {        
        winGameOverAble = true;
        textGameOverWin.color = Color.green;
        textGameOverWin.text = "You Won\nPress R to restart";
        timerText.enabled = false;
        RecordText.enabled = true;
        RecordText.text = "Record: " + timerRecord.ToString("F2");
        textGameOverWin.enabled = true;
        scoreLiveText.enabled = false;
        if (saverAble == true)
        {
            bestRecord = timerRecord;
        }
        saverAble = false;

        if (timerRecord < bestRecord)
        {
            bestRecord = timerRecord;
            timerText.enabled = false;
            RecordText.enabled = false;
            newRecordText.enabled = true;
            newRecordText.color = Color.green;
            newRecordText.text = "NEW Record: " + timerRecord.ToString("F2");
        }

        Time.timeScale = 0f;
    }
}
