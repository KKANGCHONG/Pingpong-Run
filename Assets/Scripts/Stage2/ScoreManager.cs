using UnityEngine;
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
    private bool gameEnded = false;

    void Start()
    {
        // 제한시간을 랜덤으로 설정
        remainingTime = Random.Range(minTimeLimit, maxTimeLimit);
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
        SceneManager.LoadScene("Stage3");
    }
    private void GameOver()
    {
        if (gameEnded) return;

        gameEnded = true;
        Debug.Log("Game Over! Time's up!");
        SceneManager.LoadScene("Select");
    }
}
