using UnityEngine;

public class TintObjectBrown : MonoBehaviour
{
    // bleow is to tint the dispaearing platforms brown
    public Color tintColor = new Color(0.545f, 0.271f, 0.075f);// Tint color
    [Range(0, 1)] public float tintStrength = 0.25f; // tint strength

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

    // set and call apply tint
    public void SetTintStrength(float newStrength)
    {
        tintStrength = Mathf.Clamp01(newStrength); 
        ApplyTint();
    }
}
