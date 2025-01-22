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

    // 알코올 농도 추가
    public int Alcohol = 0;

    [Header("Game Settings")]
    public float timeLimit = 30f; // 제한시간 (초)
    private float currentTime; // 현재 시간
    private bool stairSpawned = false;
    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject BallSpawner;
    // public GameObject StairPrefab;    // 계단 프리팹
    // public Transform StairSpawnPoint; // 계단 생성 위치
    public Player PlayerScript;
    public TMP_Text timerText;
    

    void Awake()    {
        if(Instance == null)    {
            Instance = this;
        }
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Stage1" && SceneManager.GetActiveScene().name != "Stage3"){
            Destroy(gameObject); // Stage1 씬이 아니면 제거
        }
        IntroUI.SetActive(true);
        currentTime = 0f;
    }

    float CalculateScore()   {
        return Time.time - PlayStartTime;
    }

    // 점수 계산 로직
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

    // 알코올 값 증가, 최대 5까지 제한
    void drink_soju()
    {
        GameManager.Instance.Alcohol = Mathf.Clamp(GameManager.Instance.Alcohol + 1, 0, 5);
    }


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
            PlayStartTime = Time.time;
        }
        if(State == GameState.Playing && Lives == 0)    {
            PlayerScript.KillPlayer();
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
        if (BallSpawner != null)
        {
            BallSpawner.SetActive(false);
        }
        // GameObject stair = Instantiate(StairPrefab, StairSpawnPoint.position, Quaternion.identity);
        // Mover stairMover = stair.GetComponent<Mover>();
        // if (stairMover != null)
        // {
        //     stairMover.SetStopCallback(OnStairStopped); // 계단 멈춤 시 실행될 콜백 설정
        // }
    }

    // private void OnStairStopped(Vector3 stairPosition)
    // {
    //     Debug.Log("Stair stopped. Moving player.");
    //     PlayerScript.StartMovingToStair(stairPosition); // 플레이어 이동 시작
    // }
}
