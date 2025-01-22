using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro를 사용하기 위해 필요

public class ScoreManager : MonoBehaviour
{
    public TMP_Text leftScoreText; // 왼쪽 점수 텍스트
    public TMP_Text rightScoreText; // 오른쪽 점수 텍스트

    private int leftScore = 0; // 왼쪽 점수
    private int rightScore = 0; // 오른쪽 점수
    public int maxScore = 6; // 최대 점수

    [Header("Game Settings")]
    public float minTimeLimit = 60f; // 제한시간 최소값
    public float maxTimeLimit = 120f; // 제한시간 최대값
    private float remainingTime; // 남은 시간
    [Header("References")]
    public GameObject SelectionUI; // 선택지 UI
    public GameObject DeadUI; // 선택지 UI
    public TMP_Text selectionMessage; // 선택 메시지 UI
    public GameObject arrow; // 화살표 오브젝트
    public GameObject ball;
    public Vector3[] arrowPositions;
    public Transform[] buttonPositions; // 버튼 위치 배열 (Transform)

    private int selectedButtonIndex = 0; // 현재 선택된 버튼 인덱스

    private bool gameEnded = false;

    void Start()
    {
        // 제한시간을 랜덤으로 설정
        remainingTime = Random.Range(minTimeLimit, maxTimeLimit);
        SelectionUI.SetActive(false);
        DeadUI.SetActive(false);
        UpdateArrowPosition();
    }

    void Update()
    {
        if (!gameEnded)
        {
            // 남은 시간 감소
            remainingTime -= Time.deltaTime;

            // 제한시간이 0 이하가 되면 게임 오버 처리
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                GameOver();
            }
        }
        else if (SelectionUI.activeSelf)
        {
            HandleArrowNavigation();
        }
    }

    // 점수 추가 메서드
    public void AddScore(bool isRightSide)
    {
        if (!isRightSide)
        {
            rightScore++;
            rightScoreText.text = rightScore.ToString();
        }
        else
        {
            leftScore++;
            leftScoreText.text = leftScore.ToString();
        }

        // 점수 체크
        CheckWinCondition();
    }

    // 승리 조건 확인
    private void CheckWinCondition()
    {
        if (leftScore >= maxScore)
        {
            Debug.Log("Left Player Wins!");
            // 게임 종료 로직 추가
            EndGame("Left Player");
        }
        else if (rightScore >= maxScore)
        {
            Debug.Log("Right Player Wins!");
            // 게임 종료 로직 추가
            EndGame("Right Player");
        }
    }

    private void EndGame(string winner)
    {
        if (gameEnded) return;

        gameEnded = true;
        Debug.Log($"{winner} wins the game!");
        // 게임 재시작 또는 다른 처리
        ShowSelectionUI(winner);
    }
    private void GameOver()
    {
        if (gameEnded) return;

        gameEnded = true;
        DeadUI.SetActive(true);
        StartCoroutine(LoadSceneAfterDelay("Select", 2f));
    }
    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    private void ShowSelectionUI(string message)
    {
        SelectionUI.SetActive(true); // 선택지 UI 활성화

        if (ball != null)   {
            ball.SetActive(false); // 공 숨기기
        }
        UpdateArrowPosition();
    }
    public void PlayAgain()
    {
        SelectionUI.SetActive(false);

        if (ball != null){
            ball.SetActive(true);
            ResetBall();
        }
        ResetGame();
    }

    public void ProceedToStage3()
    {
        SceneManager.LoadScene("Stage3");
    }

    private void ResetGame()
    {
        leftScore = 0;
        rightScore = 0;
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
        gameEnded = false;
        
    }
    private void ResetBall()
    {
        if (ball != null)
        {
            ball.transform.position = Vector3.zero; // 공의 위치를 초기화
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            Ball ballScript = ball.GetComponent<Ball>();
            if (ballRb != null && ballScript != null)
            {
                ballRb.linearVelocity = Vector2.zero; // 공의 속도를 초기화
                // 랜덤 방향으로 초기 속도 설정
                bool isRight = UnityEngine.Random.value >= 0.5f;
                float xVelocity = isRight ? 1f : -1f;
                float yVelocity = UnityEngine.Random.Range(-1f, 1f);

                ballRb.linearVelocity = new Vector2(xVelocity, yVelocity).normalized * ballScript.startingSpeed; // 초기 속도 적용
            }
        }
    }
    private void HandleArrowNavigation()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedButtonIndex = (selectedButtonIndex - 1 + buttonPositions.Length) % buttonPositions.Length;
            UpdateArrowPosition();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedButtonIndex = (selectedButtonIndex + 1) % buttonPositions.Length;
            UpdateArrowPosition();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selectedButtonIndex == 0)
            {
                PlayAgain();
            }
            else if (selectedButtonIndex == 1)
            {
                ProceedToStage3();
            }
        }
    }

    private void UpdateArrowPosition()
    {
        if (arrow != null && arrowPositions.Length > 0)
        {
            arrow.transform.position = arrowPositions[selectedButtonIndex];
        }
    }
}
