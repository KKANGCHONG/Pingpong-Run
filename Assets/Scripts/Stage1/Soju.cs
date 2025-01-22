using UnityEngine;

public class Soju : MonoBehaviour
{
    public Sprite Cup_Empty;
    public Sprite Cup_1;
    public Sprite Cup_2;
    public Sprite Cup_3;
    public Sprite Cup_4;
    public Sprite Cup_5;
    public SpriteRenderer SpriteRenderer;

    public int Alcohol_Number;

    void Start()
    {
        
    }

    void Update()
    {
        // 현재 Alcohol 값을 기준으로 Sprite 변경
        switch (GameManager.Instance.Alcohol)
        {
            case 0:
                SpriteRenderer.sprite = Cup_Empty;
                break;
            case 1:
                SpriteRenderer.sprite = Cup_1;
                break;
            case 2:
                SpriteRenderer.sprite = Cup_2;
                break;
            case 3:
                SpriteRenderer.sprite = Cup_3;
                break;
            case 4:
                SpriteRenderer.sprite = Cup_4;
                break;
            case 5:
                SpriteRenderer.sprite = Cup_5;
                break;
        }
    }
}
