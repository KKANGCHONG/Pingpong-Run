using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManger : MonoBehaviour
{
    [Header("References")]
    public GameObject IntroUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Select"){
            Destroy(gameObject);
        }
        IntroUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("Stage1");
        }
    }
}
