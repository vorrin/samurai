using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Separation")]
public class ColorSeparation : ImageEffectBase {

	
	public Vector2 rOffset = new Vector2(0.02f, 0f);
	public Vector2 gOffset = new Vector2(0f, 0f);
	public Vector2 bOffset = new Vector2(0f, 0.02f);
	
	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		
		
		material.SetVector("_rOffset", rOffset);
		material.SetVector("_gOffset", gOffset);
		material.SetVector("_bOffset", bOffset);
		Graphics.Blit (source, destination, material, 0);
	}
}

