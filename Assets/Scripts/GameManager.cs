// using UnityEngine;
// using UnityEngine.SceneManagement;
// using TMPro;

// public enum GameState   {
//     Intro,
//     Playing,
//     Dead
// }

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance;

//     public GameState State = GameState.Intro;
//     public float PlayStartTime;
//     public int Lives = 3;

//     // 알코올 농도 추가
//     public int Alcohol = 0;

//     [Header("Game Settings")]
//     public float timeLimit = 30f; // 제한시간 (초)
//     private float currentTime; // 현재 시간
//     private bool stairSpawned = false;
//     [Header("References")]
//     public GameObject IntroUI;
//     public GameObject DeadUI;
//     public GameObject BallSpawner;
//     // public GameObject StairPrefab;    // 계단 프리팹
//     // public Transform StairSpawnPoint; // 계단 생성 위치

//     // 점수 계산을 위한 공 개수 추가
//     public int BallNumber;
//     // 공 개수 텍스트 추가
//     public TMP_Text BallNumberText;
//     public Player PlayerScript;
//     public TMP_Text timerText;
    

//     void Awake()    {
//         if(Instance == null)    {
//             Instance = this;
//         }
//     }
//     void Start()
//     {
//         if (SceneManager.GetActiveScene().name != "Stage1" && SceneManager.GetActiveScene().name != "Stage3"){
//             Destroy(gameObject); // Stage1 씬이 아니면 제거
//         }
//         IntroUI.SetActive(true);
//         currentTime = 0f;
//     }

//     // 점수 계산 관련 함수들
//     float CalculateScore()   {
//         // return Time.time - PlayStartTime;
//         return BallNumber;
//     }

//     // 점수 계산 로직
//     // void SaveHighScore()    {
//     //     int score = Mathf.FloorToInt(CalculateScore());
//     //     int currentHightScore = PlayerPrefs.GetInt("highScore");
//     //     if(score > currentHightScore)   {
//     //         PlayerPrefs.SetInt("highScore", score);
//     //         PlayerPrefs.Save();
//     //     }
//     // }

//     // int GetHighScore(){
//     //     return PlayerPrefs.GetInt("highScore");
//     // }

//     // 알코올 값 증가, 최대 5까지 제한
//     void drink_soju()
//     {
//         GameManager.Instance.Alcohol = Mathf.Clamp(GameManager.Instance.Alcohol + 1, 0, 5);
//     }


//     void Update()
//     {
//         if(State == GameState.Playing)  {
//             currentTime += Time.deltaTime;
//             if (currentTime >= timeLimit)
//             {
//                 currentTime = timeLimit;
//                 if (!stairSpawned) OnTimeUp(); // 제한시간이 끝났을 때 처리
//             }

//             // 제한시간 UI 업데이트
//             if (timerText != null)
//             {
//                 timerText.text = Mathf.CeilToInt(currentTime).ToString();
//             }
//         } //else if (State == GameState.Dead) {
//         //     timerText.text = "High Score: " + GetHighScore();
//         // }

//         //Ball 개수 뜨게..
//         if (BallNumberText != null) 
//         {
//             BallNumberText.text = BallNumber.ToString();
//         }

//         if(State == GameState.Intro && Input.GetKeyDown(KeyCode.Space)) {
//             State = GameState.Playing;
//             IntroUI.SetActive(false);
//             PlayStartTime = Time.time;
//         }
//         if(State == GameState.Playing && Lives == 0)    {
//             PlayerScript.KillPlayer();
//             if(BallSpawner != null) {
//                 BallSpawner.SetActive(false);
//             }
//             DeadUI.SetActive(true);
//             // SaveHighScore();
//             State = GameState.Dead;
//         }
//         if(State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))  {
//             SceneManager.LoadScene("Select");
//         }
//     }
//     private void OnTimeUp()
//     {
//         Debug.Log("Time's up!"); // 제한시간 종료 처리
//         stairSpawned = true;
//         if (BallSpawner != null)
//         {
//             BallSpawner.SetActive(false);
//         }
//         // GameObject stair = Instantiate(StairPrefab, StairSpawnPoint.position, Quaternion.identity);
//         // Mover stairMover = stair.GetComponent<Mover>();
//         // if (stairMover != null)
//         // {
//         //     stairMover.SetStopCallback(OnStairStopped); // 계단 멈춤 시 실행될 콜백 설정
//         // }
//     }

//     // private void OnStairStopped(Vector3 stairPosition)
//     // {
//     //     Debug.Log("Stair stopped. Moving player.");
//     //     PlayerScript.StartMovingToStair(stairPosition); // 플레이어 이동 시작
//     // }
// }
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
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

    // 흔들림 방지 플래그 추가
    public bool PreventShake = false;

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

    // 점수 계산을 위한 공 개수 추가
    public int BallNumber;
    // 공 개수 텍스트 추가
    public TMP_Text BallNumberText;
    public Player PlayerScript;
    public TMP_Text timerText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Stage1" && SceneManager.GetActiveScene().name != "Stage3")
        {
            Destroy(gameObject); // Stage1 씬이 아니면 제거
        }
        IntroUI.SetActive(true);
        currentTime = 0f;
    }

    // 점수 계산 관련 함수들
    float CalculateScore()
    {
        return BallNumber; // 현재 공 개수를 기준으로 점수 계산
    }

    // 알코올 값 증가, 최대 5까지 제한
    public void DrinkSoju()
    {
        Alcohol = Mathf.Clamp(Alcohol + 1, 0, 5);
    }

    // 알코올 값 감소, 최소 0까지 제한
    public void DrinkCondition()
    {
        Alcohol = Mathf.Clamp(Alcohol - 1, 0, 5);
        PreventShake = true; // 흔들림 방지 플래그 활성화
    }

    void Update()
    {
        if (State == GameState.Playing)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeLimit)
            {
                currentTime = timeLimit;
                if (!stairSpawned) OnTimeUp(); // 제한시간이 끝났을 때 처리
            }

            // 제한시간 UI 업데이트
            if (timerText != null)
            {
                timerText.text = Mathf.CeilToInt(currentTime).ToString();
            }
        }

        // Ball 개수 텍스트 업데이트
        if (BallNumberText != null)
        {
            BallNumberText.text = BallNumber.ToString();
        }

        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            PlayStartTime = Time.time;
        }
        if (State == GameState.Playing && Lives == 0)
        {
            PlayerScript.KillPlayer();
            if (BallSpawner != null)
            {
                BallSpawner.SetActive(false);
            }
            DeadUI.SetActive(true);
            State = GameState.Dead;
        }
        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
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
    }
}
