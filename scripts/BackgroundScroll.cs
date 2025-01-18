using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast should the texture scroll?")]
    public float scrollSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Refereneces")]
    public MeshRenderer meshRenderer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }
}
