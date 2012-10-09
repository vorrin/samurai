using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Scanlines")]
public class Scanlines: ImageEffectBase {
	
	
	public float scanlineInterval = 10f;
	public float scanlineHeight = 5f;
	public float scanlineMultiplier = 0.9f;
	public float scanlineShift = 0.01f;
	
	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		
		material.SetFloat("_ScanlineInterval", scanlineInterval);
		material.SetFloat("_ScanlineHeight", scanlineHeight);
		material.SetFloat("_ScanlineMultiplier", scanlineMultiplier);
		material.SetFloat("_ScanlineShift", scanlineShift);
		
		Graphics.Blit (source, destination, material);
	}
	
}