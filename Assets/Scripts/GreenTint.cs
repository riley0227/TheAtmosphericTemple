using UnityEngine;

public class TintObjectGreen : MonoBehaviour
{
    public Color tintColor = Color.green; // Tint color
    [Range(0, 1)] public float tintStrength = 0.25f; // 0 is no tint, 1 is full tint

    private Renderer objectRenderer;
    private Color originalColor;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            // Save the original color
            originalColor = objectRenderer.material.color;

            // Apply the tint with the specified strength
            ApplyTint();
        }
    }

    public void ApplyTint()
    {
        // Blend between the original color and the tint color
        if (objectRenderer != null)
        {
            Color blendedColor = Color.Lerp(originalColor, tintColor, tintStrength);
            objectRenderer.material.color = blendedColor;
        }
    }

    public void SetTintStrength(float newStrength)
    {
        tintStrength = Mathf.Clamp01(newStrength); // Clamp to keep between 0 and 1
        ApplyTint();
    }
}
