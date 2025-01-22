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
    public AudioClip[] characterAudioClips; // 캐릭터별 오디오 클립 배열
    private AudioSource audioSource; // 오디오 재생용 AudioSource
    public GameObject arrow; // 화살표 오브젝트
    public Vector3[] arrowPositions; // 화살표 위치 배열
    private int selectedCharacterIndex = 0; // 선택된 캐릭터의 인덱스
    private SelectState state = SelectState.Intro;
    private bool isAudioPlaying = false; // 오디오 재생 여부
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Select"){
            Destroy(gameObject);
        }
        state = SelectState.Intro;
        IntroUI.SetActive(true);
        CharacterSelectionUI.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        UpdateCharacterSelection();
        UpdateArrowPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAudioPlaying) return;
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
            PlaySelectedCharacterAudio();
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
    private void PlaySelectedCharacterAudio()
    {
        if (characterAudioClips.Length > selectedCharacterIndex && characterAudioClips[selectedCharacterIndex] != null)
        {
            isAudioPlaying = true; // 오디오 재생 중으로 설정
            audioSource.clip = characterAudioClips[selectedCharacterIndex];
            audioSource.Play();
            StartCoroutine(WaitForAudioToFinish());
        }
        else
        {
            // 오디오가 없으면 바로 Stage1으로 이동
            SceneManager.LoadScene("Stage1");
        }
    }

    private System.Collections.IEnumerator WaitForAudioToFinish()
    {
        yield return new WaitForSeconds(audioSource.clip.length); // 오디오 재생 시간만큼 대기
        SceneManager.LoadScene("Stage1"); // Stage1 씬으로 이동
    }
}
