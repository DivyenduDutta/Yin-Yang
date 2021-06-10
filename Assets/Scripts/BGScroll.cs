using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private float scroll_speed = 0.07f;

    private MeshRenderer meshRenderer;

    private Vector2 savedOffset;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        savedOffset = meshRenderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    private void Update()
    {
        float y = Time.time * scroll_speed;
        Vector2 offset = new Vector2(0, y);

        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    private void OnDisable()
    {
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
