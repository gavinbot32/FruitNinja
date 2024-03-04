using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Vars")]
    public int points;
    public int lives;
    public bool isRunning;
    public float pointMult;

    [Header("Fruit Vars")]
    public GameObject[] fruits;
    public GameObject[] special_fruits;
    public Vector2 spawnMinMax;
    private Vector2 defaultSpawnMinMax;
    private float spawnCooldown;

    [Header("UI Components")]
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI[] lifeList;
    private List<TextMeshProUGUI> livesObjects;
    public GameObject gameOverScreen;
    public TextMeshProUGUI gameoverPoints;
    public GameObject comboObject;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI highscoreText;
    public GameObject stopwatch;
    public GameObject dblPointsObj;
    public GameObject stopwatchUI;
    public GameObject dblPointsUI;
    public Color grayColor;
    public Color redColor;

    [Header("Components")]
    private SwordCursor sword;


    private void Awake()
    {
       sword = FindObjectOfType<SwordCursor>();
        defaultSpawnMinMax = spawnMinMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        newGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (spawnCooldown > 0)
            {
                spawnCooldown -= Time.deltaTime;
            }
            if (spawnCooldown <= 0)
            {
                spawnFruit();
            }
        }
    }

    private void spawnFruit()
    {
        spawnCooldown = Random.Range(spawnMinMax.x, spawnMinMax.y);
        Vector2 spawnPoint = new Vector2(Random.Range(-6f, 6f), -6f);

        if (Random.Range(0,101) <= 5)
        {
            Instantiate(special_fruits[Random.Range(0, special_fruits.Length)], spawnPoint, Quaternion.identity);
        }
        else
        {
            Instantiate(fruits[Random.Range(0, fruits.Length)], spawnPoint, Quaternion.identity);
        }
    }

    public void gameOver()
    {
        if (!isRunning)
        {
            return;
        }

        if (PlayerPrefs.HasKey("Highscore"))
        {
            if (points > PlayerPrefs.GetInt("Highscore",0))
            {
                PlayerPrefs.SetInt("Highscore", points);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", points);
        }

        PlayerPrefs.Save();

        highscoreText.text = PlayerPrefs.GetInt("Highscore",0).ToString();
        isRunning = false;
        gameoverPoints.text = points.ToString();
        lives = 0;
        foreach(TextMeshProUGUI life in livesObjects)
        {
            life.color = grayColor;
        }
        gameOverScreen.SetActive(true);
        pointText.gameObject.SetActive(false);
        dblPointsUI.SetActive(false);
        stopwatchUI.SetActive(false);
        gameOverScreen.GetComponent<Animator>().SetTrigger("GameOver");
    }

    public void newGame()
    {
        lives = 3;
        points = 0;
        pointMult = 1f;
        spawnMinMax = defaultSpawnMinMax;
        livesObjects = new List<TextMeshProUGUI>();
        foreach (TextMeshProUGUI life in lifeList)
        {
            livesObjects.Add(life);
            life.color = redColor;
        }
        dblPointsUI.SetActive(false);
        stopwatchUI.SetActive(false);
        gameOverScreen.SetActive(false);
        pointText.gameObject.SetActive(true);
        gameoverPoints.text = "0";
        sword.newGame();
        uiUpdate();
        StartCoroutine(startRoutine());
    }

    public void addPoints(int amount)
    {
        points += (int)((amount * sword.comboMultiplier) * pointMult);
        uiUpdate();
    }

    public void looseLife()
    {
        if(lives <= 0)
        {
            gameOver();
            return;
        }
        lives -= 1;
        TextMeshProUGUI life = livesObjects[livesObjects.Count - 1];
        livesObjects.Remove(life);
        life.color = grayColor;
        if(lives <= 0)
        {
            gameOver();
        }
    }

    public void uiUpdate()
    {
        pointText.text = points.ToString();
        if(sword.comboPoints >= 3)
        {
            comboObject.gameObject.SetActive(true);
        }
        else
        {
            comboObject.gameObject.SetActive(false);
        }
        comboText.text = sword.comboPoints.ToString();

    }

    IEnumerator startRoutine()
    {
        startText.gameObject.SetActive(true);
        startText.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(4);
        startText.gameObject.SetActive(false);
        isRunning = true;
    }

  

}
