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

    public float speed = 3;
    public int live = 5;
    public TextMeshProUGUI scoreLiveText;
    public GameObject portalWin;
    int points;
    public TextMeshProUGUI textGameOverWin;
    public TextMeshProUGUI timerText;
    private bool winGameOverAble;
    float timerRecord;
    

    // Start is called before the first frame update
    void Start()
    {
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
        timerText.text = "Timer: " + timerRecord.ToString("F0");


        if (Input.GetKeyDown(KeyCode.R) && winGameOverAble)
        {
            SceneManager.LoadScene(0);        
        }
      
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalMove);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalMove);

        scoreLiveText.text = "Score: " + points + "\nLives: " + live;

       

        if (live <= 0)
        {
            winGameOverAble = true;
            textGameOverWin.color = Color.red;
            textGameOverWin.text = "You Lost\nPress R to restart";
            timerText.text = "Record: " + timerRecord.ToString("F0");
            textGameOverWin.enabled = true;
            scoreLiveText.enabled = false;
           
            Time.timeScale = 0f;

        }

        if (points >= 5)
        {
            portalWin.gameObject.SetActive(true);
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
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            winGameOverAble = true;
            textGameOverWin.color = Color.green;
            textGameOverWin.text = "You Won\nPress R to restart";
            timerText.text = "Record: " + timerRecord.ToString("F0");
            textGameOverWin.enabled = true;
            scoreLiveText.enabled = false;
           
            Time.timeScale = 0f;
        }
        if (collision.gameObject.CompareTag("Restart"))
        {
            winGameOverAble = true;
            textGameOverWin.color = Color.red;
            textGameOverWin.text = "You Fell\nPress R to restart";
            timerText.text = "Record: " + timerRecord.ToString("F0");
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
}
