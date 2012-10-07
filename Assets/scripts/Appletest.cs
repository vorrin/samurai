using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Appletest : MonoBehaviour {
	public AnimationCurve heightCurve;
	public AnimationCurve curve;
	private Vector3 cameraPoint; 
	private Vector3 startingPoint;
	private Vector3 destVect;
	float distance;
	public float flyingTime;
	public God god;
	public float distanceTimeCoefficent = 3; 
	private float ownTime;
	//private float timeCoefficent;
	
	// Use this for initialization
	void Start () {
		god = (God) FindObjectOfType(typeof(God));
		cameraPoint = Camera.mainCamera.transform.position;
		Reset();
		
		
		
		
	}
	
/*	float ResetCurvesToDistance(float distance){
		float step = (distance / distanceTimeCoefficent)/10;
		int counter = 0;
		List<AnimationCurve> curves = new List<AnimationCurve>();
		curves.Add(heightCurve);
		curves.Add(curve);
		float longestTime = 0;
		
		//AnimationCurve[] curves = new AnimationCurve[heightCurve,curve];
		for (int j = 0; j < curves.Count; j++){
			AnimationCurve currentCurve = curves[j];
			AnimationCurve tmpCurve = new AnimationCurve();
			for (int i = 0; i< currentCurve.length; i++){
				Keyframe currentKey =  currentCurve.keys[i];
				currentKey.time = currentKey.time * step;
				longestTime = currentKey.time;
				//currentCurve.MoveKey(counter,currentKey);
				tmpCurve.AddKey(currentKey);
				//currentCurve.keys[counter] = currentKey;
				counter++;		
			}
			counter = 0;
			for (int k = 0; k < currentCurve.length; k++){
			//	currentCurve.RemoveKey(k);
				currentCurve.MoveKey(k,tmpCurve.keys[k]);
			}
		}
		
		//Returns the time it will take to arrive:
		return longestTime;
		
	}
	
	*/
	
	// Update is called once per frame
	void Update () {
		
		ownTime += Time.deltaTime/flyingTime;
		float curveMoment = curve.Evaluate(ownTime);
		if (curveMoment >= 1){
			return;
		}
		float heightMoment = heightCurve.Evaluate(ownTime);
		Vector3 tmpPos = startingPoint + destVect* curveMoment ;
		float height = tmpPos.y + (2 * heightMoment);
		transform.position = new Vector3(tmpPos.x, height, tmpPos.z);		
	}
	
	public void Hit(){
		Debug.Log("GHT");
		animation.enabled = false;
		Vector3 baseVector = Camera.mainCamera.transform.forward;
		baseVector = new Vector3( Random.Range(-0.7F,0.7F)  ,Random.Range(-0.2F,1.2F) * 1 ,Random.Range(0.7F,1.3F) * 1); 
		Debug.Log(baseVector);
		rigidbody.AddForce( baseVector * 1000);
		
	}
	
	
	public void Reset(){
		transform.position = new Vector3(Random.Range(-6.5F,6.5F),Random.Range(0.25F,2.7F),Random.Range(-2.3F,1.5F));
		startingPoint = transform.position;
		destVect = cameraPoint - startingPoint;
		distance = destVect.magnitude;
		flyingTime = distance/distanceTimeCoefficent;

		ownTime = 0;
		god.TempReset(flyingTime);
		rigidbody.velocity = Vector3.zero;
		animation.enabled = true;
		Debug.Log(flyingTime + " hai " + distance);

	}
	
	
}
