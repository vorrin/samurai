using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Pixelated")]
public class Pixelated : ImageEffectBase {
	
	public float pixelWidth = 5.0f;
	public float pixelHeight = 5.0f;
	

	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		
		material.SetFloat("_PixelWidth", pixelWidth);
		material.SetFloat("_PixelHeight", pixelHeight);
		
		Graphics.Blit (source, destination, material, 0);
	}
	
}