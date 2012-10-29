using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {
	
	public GameObject bat;
	private Transform batStartingTransform;
	
	
	// Use this for initialization
	void Start () {
		batStartingTransform = bat.transform;
	}
	
	public IEnumerator SwingBat(){
		Quaternion newRotation = batStartingTransform.localRotation;
		newRotation.y = -91;
		
		
		
		
		
		
		
		
		
		
		
		
		
		iTween.RotateBy(bat,new Vector3(0,-0.5f,0),.1F);
		//TweenRotation.Begin(bat,0.1F,newRotation);
		yield return new WaitForSeconds(.4F);
		ResetBat();
	}
	void ResetBat(){
		Debug.Log("resetting BAT");	
		Debug.Log(batStartingTransform.localEulerAngles);
		iTween.RotateBy(bat,new Vector3(0,0.5F,0),.2F);
		//iTween.RotateTo(bat,batStartingTransform.localEulerAngles,.2F);
		//TweenRotation.Begin(bat,0.2F,batStartingTransform.localRotation);
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
