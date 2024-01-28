using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class CustomPostProcessing : MonoBehaviour
{
    public Material postProcessingMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, postProcessingMaterial);
    }
}