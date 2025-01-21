using UnityEngine;
using UnityEngine.SceneManagement;

public enum SelectState
{
    Intro,
    CharacterSelection
}

public class SelectManger : MonoBehaviour
{
    [Header("References")]
    public GameObject IntroUI;
    public GameObject CharacterSelectionUI; // 캐릭터 선택 UI
    public GameObject[] characterOptions; // 캐릭터 옵션 배열
    public GameObject arrow; // 화살표 오브젝트
    public Vector3[] arrowPositions; // 화살표 위치 배열
    private int selectedCharacterIndex = 0; // 선택된 캐릭터의 인덱스
    private SelectState state = SelectState.Intro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Select"){
            Destroy(gameObject);
        }
        state = SelectState.Intro;
        IntroUI.SetActive(true);
        CharacterSelectionUI.SetActive(false);
        UpdateCharacterSelection();
        UpdateArrowPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SelectState.Intro) {
            HandleIntroState();
        }
        else if (state == SelectState.CharacterSelection) {
            HandleCharacterSelectionState();
        }
    }
    // Intro 상태 처리
    private void HandleIntroState() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            state = SelectState.CharacterSelection; // Intro 상태에서 캐릭터 선택 상태로 전환
            IntroUI.SetActive(false);
            CharacterSelectionUI.SetActive(true);
        }
    }
    // 캐릭터 선택 상태 처리
    private void HandleCharacterSelectionState()    {
        // 좌우 화살표로 캐릭터 선택
        if (Input.GetKeyDown(KeyCode.LeftArrow))    {
            selectedCharacterIndex = (selectedCharacterIndex - 1 + characterOptions.Length) % characterOptions.Length;
            UpdateCharacterSelection();
            UpdateArrowPosition();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))  {
            selectedCharacterIndex = (selectedCharacterIndex + 1) % characterOptions.Length;
            UpdateCharacterSelection();
            UpdateArrowPosition();
        }

        // Space 키를 눌러 Stage1으로 이동
        if (Input.GetKeyDown(KeyCode.Space))    {
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex); // 선택한 캐릭터 저장
            SceneManager.LoadScene("Stage1"); // Stage1으로 이동
        }
    }

    // 선택된 캐릭터를 활성화하고 나머지는 비활성화
    private void UpdateCharacterSelection() {
        for (int i = 0; i < characterOptions.Length; i++)   {
            characterOptions[i].SetActive(i == selectedCharacterIndex);
        }
    }
    private void UpdateArrowPosition()
    {
        if (arrow != null && arrowPositions.Length > selectedCharacterIndex)
        {
            arrow.transform.position = arrowPositions[selectedCharacterIndex];
        }
    }
}
