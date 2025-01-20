using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState   {
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State = GameState.Intro;
    public float PlayStartTime;
    public int Lives = 3;
    [Header("Game Settings")]
    public float timeLimit = 60f; // 제한시간 (초)
    private float currentTime; // 현재 시간
    private bool stairSpawned = false;
    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public GameObject BallSpawner;
    public GameObject StairPrefab;    // 계단 프리팹
    public Transform StairSpawnPoint; // 계단 생성 위치
    public Player PlayerScript;
    public TMP_Text timerText;
    

    void Awake()    {
        if(Instance == null)    {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Stage1"){
            Destroy(gameObject); // Stage1 씬이 아니면 제거
        }
        IntroUI.SetActive(true);
        currentTime = 0f;
    }

    float CalculateScore()   {
        return Time.time - PlayStartTime;
    }

    void SaveHighScore()    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHightScore = PlayerPrefs.GetInt("highScore");
        if(score > currentHightScore)   {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore(){
        return PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        if(State == GameState.Playing)  {
            currentTime += Time.deltaTime;
            if (currentTime >= timeLimit)
            {
                currentTime = timeLimit;
                if (!stairSpawned) OnTimeUp(); // 제한시간이 끝났을 때 처리
            }

            // 제한시간 UI 업데이트
            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
            }
        } else if (State == GameState.Dead) {
            timerText.text = "High Score: " + GetHighScore();
        }
        if(State == GameState.Intro && Input.GetKeyDown(KeyCode.Space)) {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            BallSpawner.SetActive(true);
            PlayStartTime = Time.time;
        }
        if(State == GameState.Playing && Lives == 0)    {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            if(BallSpawner != null) {
                BallSpawner.SetActive(false);
            }
            DeadUI.SetActive(true);
            SaveHighScore();
            State = GameState.Dead;
        }
        if(State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))  {
            SceneManager.LoadScene("Select");
        }
    }
    private void OnTimeUp()
    {
        Debug.Log("Time's up!"); // 제한시간 종료 처리
        stairSpawned = true;
        EnemySpawner.SetActive(false);
        FoodSpawner.SetActive(false);
        GoldenSpawner.SetActive(false);
        if (BallSpawner != null)
        {
            BallSpawner.SetActive(false);
        }
        GameObject stair = Instantiate(StairPrefab, StairSpawnPoint.position, Quaternion.identity);
        Mover stairMover = stair.GetComponent<Mover>();
        if (stairMover != null)
        {
            stairMover.SetStopCallback(OnStairStopped); // 계단 멈춤 시 실행될 콜백 설정
        }
    }

    private void OnStairStopped(Vector3 stairPosition)
    {
        Debug.Log("Stair stopped. Moving player.");
        PlayerScript.StartMovingToStair(stairPosition); // 플레이어 이동 시작
    }
}
