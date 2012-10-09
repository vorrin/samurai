using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Dither")]
public class Dither : ImageEffectBase {
	
	
	[HideInInspector]
	Texture2D thresholdMap;
	public Vector2 blockSize = new Vector2(2f,2f);
	
	public enum MapEnum {Bayer4x4, Bayer8x8};
	
	public MapEnum ditherType; //this can't be changed live, currently. To enable this, uncomment the line in OnRenderImage
	

	void OnEnable () {
		SetThresholdMap();
	}
	
	void SetThresholdMap() {
		
		if (ditherType == MapEnum.Bayer4x4) {
			thresholdMap = new Texture2D(4,4, TextureFormat.Alpha8, false);
			thresholdMap.SetPixels( new Color[16]{new Color(0f,0f,0f,0.0625f), new Color(0f,0f,0f,0.5625f), new Color(0f,0f,0f, 0.1875f), new Color(0f,0f,0f, 0.6875f),
			                       	new Color(0f,0f,0f, 0.8125f), new Color(0f,0f,0f, 0.3125f), new Color(0f,0f,0f, 0.9375f), new Color(0f,0f,0f, 0.4375f),
			                       	new Color(0f,0f,0f, 0.25f), new Color(0f,0f,0f, 0.75f), new Color(0f,0f,0f, 0.125f), new Color(0f,0f,0f, 0.625f),
			                       	new Color(0f,0f,0f,1.0f), new Color(0f,0f,0f, 0.5f), new Color(0f,0f,0f, 0.875f), new Color(0f,0f,0f, 0.375f)});
		}
		else if (ditherType == MapEnum.Bayer8x8){
			//Bayer 8 * 8
			thresholdMap = new Texture2D(8,8, TextureFormat.Alpha8, false);
			thresholdMap.SetPixels(new Color[64]{new Color(0f,0f,0f,0.0f), new Color(0f,0f,0f, 0.5f), new Color(0f,0f,0f, 0.125f), new Color(0f,0f,0f, 0.625f), new Color(0f,0f,0f, 0.03125f), new Color(0f,0f,0f, 0.53125f), new Color(0f,0f,0f, 0.15625f), new Color(0f,0f,0f, 0.65625f), new Color(0f,0f,0f, 0.75f), new Color(0f,0f,0f, 0.25f), new Color(0f,0f,0f, 0.875f), new Color(0f,0f,0f, 0.375f), new Color(0f,0f,0f, 0.78125f), new Color(0f,0f,0f, 0.28125f), new Color(0f,0f,0f, 0.90625f), new Color(0f,0f,0f, 0.40625f), new Color(0f,0f,0f, 0.1875f), new Color(0f,0f,0f, 0.6875f), new Color(0f,0f,0f, 0.0625f), new Color(0f,0f,0f, 0.5625f), new Color(0f,0f,0f, 0.21875f), new Color(0f,0f,0f, 0.71875f), new Color(0f,0f,0f, 0.09375f), new Color(0f,0f,0f, 0.59375f), new Color(0f,0f,0f, 0.9375f), new Color(0f,0f,0f, 0.4375f), new Color(0f,0f,0f, 0.8125f), new Color(0f,0f,0f, 0.3125f), new Color(0f,0f,0f, 0.96875f), new Color(0f,0f,0f, 0.46875f), new Color(0f,0f,0f, 0.84375f), new Color(0f,0f,0f, 0.34375f), new Color(0f,0f,0f, 0.046875f), new Color(0f,0f,0f, 0.546875f), new Color(0f,0f,0f, 0.171875f), new Color(0f,0f,0f, 0.671875f), new Color(0f,0f,0f, 0.015625f), new Color(0f,0f,0f, 0.515625f), new Color(0f,0f,0f, 0.140625f), new Color(0f,0f,0f, 0.640625f), new Color(0f,0f,0f, 0.796875f), new Color(0f,0f,0f, 0.296875f), new Color(0f,0f,0f, 0.921875f), new Color(0f,0f,0f, 0.421875f), new Color(0f,0f,0f, 0.765625f), new Color(0f,0f,0f, 0.265625f), new Color(0f,0f,0f, 0.890625f), new Color(0f,0f,0f, 0.390625f), new Color(0f,0f,0f, 0.234375f), new Color(0f,0f,0f, 0.734375f), new Color(0f,0f,0f, 0.109375f), new Color(0f,0f,0f, 0.609375f), new Color(0f,0f,0f, 0.203125f), new Color(0f,0f,0f, 0.703125f), new Color(0f,0f,0f, 0.078125f), new Color(0f,0f,0f, 0.578125f), new Color(0f,0f,0f, 0.984375f), new Color(0f,0f,0f, 0.484375f), new Color(0f,0f,0f, 0.859375f), new Color(0f,0f,0f, 0.359375f), new Color(0f,0f,0f, 0.953125f), new Color(0f,0f,0f, 0.453125f), new Color(0f,0f,0f, 0.828125f), new Color(0f,0f,0f, 0.328125f)});
		}
    
		
		thresholdMap.Apply();
		thresholdMap.filterMode = FilterMode.Point;
		thresholdMap.wrapMode = TextureWrapMode.Clamp;
		material.SetTexture("_ThresholdMap", thresholdMap);
		material.SetVector("_ThresholdMapSize", new Vector2 (thresholdMap.width, thresholdMap.height));
		
	}

	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		
		//SetThresholdMap(); //Uncomment this line to change dither type at runtime
		material.SetVector("_BlockSize", blockSize);
		Graphics.Blit (source, destination, material);
	}
}