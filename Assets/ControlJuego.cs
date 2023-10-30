using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlJuego : MonoBehaviour 
{ 
    public GameObject jugador; 
    public GameObject bot; 
    public List<GameObject> listaEnemigos;
    float tiempoRestante;
    public Transform spawnerPoint;
    int cantEnemy;
    float moveInY;
    public Text timerText;
    public Text enemyCountText;
    public Text gameOverWin;
    public Text restartGame;

    public float rangeX;
        public float rangeY;
    public Collider changeCollider;
    public int currentsEnemy;
    public ControlBot controlBot;
    public GameObject[] floors;
    int floorToDestroy;


    void Start() 
    {
        Time.timeScale = 1;
        gameOverWin.enabled = false;
        restartGame.enabled = false;
        floorToDestroy = 0;
        changeCollider = GameObject.Find("ChangeFloor").GetComponent<Collider>();
        changeCollider.enabled = false;
        cantEnemy = 12;
        moveInY = 0.6f;
        ComenzarJuego(); 
    }
    void Update()
    {
        if (tiempoRestante <= 0)
        {
            gameOverWin.enabled = true;
            restartGame.enabled = true;
            gameOverWin.color = Color.red;
            gameOverWin.text = "Game Over";
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
                   Time.timeScale = 0;

        }
        
        timerText.text = tiempoRestante.ToString("F0");
        enemyCountText.text = "Enemies: " + currentsEnemy.ToString("F0");


        if (currentsEnemy <= 0)
        {
            changeCollider.enabled = true;
            Debug.Log(floorToDestroy);
            if(floorToDestroy == 3)
            {
                gameOverWin.enabled = true;
                restartGame.enabled = true;
                gameOverWin.color = Color.green;
                gameOverWin.text = "You Win";
                Time.timeScale = 0;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(0);
                }

            }
        }
    }
        
        public void EnemyDestroy()
    {
        currentsEnemy--;
    }

    void ComenzarJuego()
    { 
        foreach (GameObject item in listaEnemigos) 
        {
            Destroy(item);
        }
        controlBot.rapidez = 2;
        for (int i = 0; i < cantEnemy; i++)
        {
            Vector3 spawnerRange = new Vector3(Random.Range(rangeX, -rangeX), moveInY, Random.Range(rangeY, -rangeY));
            listaEnemigos.Add(Instantiate(bot, spawnerRange, Quaternion.identity));
        }
        currentsEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;

        StartCoroutine(ComenzarCronometro(60)); 
    }

    public void LevelFloor()
    {
        moveInY += 4;
        cantEnemy -= 2;
        jugador.transform.position = new Vector3(7, transform.position.y + moveInY, 9);
        controlBot.rapidez += 2;
        for (int i = 0; i < cantEnemy; i++)
        {
            Vector3 spawnerRange = new Vector3(Random.Range(rangeX, -rangeX), jugador.transform.position.y, Random.Range(rangeY, -rangeY));
            listaEnemigos.Add(Instantiate(bot, spawnerRange, Quaternion.identity));
        }
        currentsEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Destroy(floors[floorToDestroy]);
        floorToDestroy++;
        changeCollider.enabled = false;
    }
    
    public IEnumerator ComenzarCronometro(float valorCronometro = 60) 
    {
        tiempoRestante = valorCronometro; 
        while (tiempoRestante > 0)
        { 
            Debug.Log("Restan " + tiempoRestante + " segundos.");
            yield return new WaitForSeconds(1.0f); 
            tiempoRestante--; 
        } 
    }
}