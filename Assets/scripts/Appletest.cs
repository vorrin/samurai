using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Appletest : MonoBehaviour {
	public AnimationCurve heightCurve;
	public AnimationCurve curve;
	private Vector3 hitPoint; 
	private Vector3 startingPoint;
	private Vector3 destVect;
	float distance;
	public float flyingTime;
	public God god;
	public float distanceTimeCoefficent = 3; 
	private float ownTime;
	public GameObject pointToHit;
	//private float timeCoefficent;
	
	// Use this for initialization
	void Start () {
		Debug.Log(pointToHit);
		god = (God) FindObjectOfType(typeof(God));
		if (pointToHit.transform.position != Vector3.zero) hitPoint = pointToHit.transform.position;
		else hitPoint = Camera.mainCamera.transform.position;
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
		Debug.DrawLine(Camera.mainCamera.transform.position,debugVec);
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
	
	private Vector3 debugVec = Vector3.zero;
	
	public void Hit(){
		Debug.Log("GHT");
		animation.enabled = false;
		Vector3 baseVector = Camera.mainCamera.transform.forward;
		//baseVector = new Vector3( Random.Range(-0.7F,0.7F)  ,Random.Range(-0.2F,1.2F) * 1 ,Random.Range(0.7F,1.3F) * 1); 
		Vector3 worldMousePos = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
		//Ray ray = new Ray(Camera.mainCamera.transform.position,  new Vector3( worldMousePos.x  ,worldMousePos.y,1000));
		
//		baseVector = new Vector3( Input.mousePosition.x  ,Input.mousePosition.y ,Random.Range(0.7F,1.3F) * 1); 
		baseVector = ( worldMousePos - Camera.mainCamera.transform.position) * 1000 ;//new Vector3( worldMousePos.x  ,worldMousePos.y,20);
		Debug.Log(baseVector);
		
		debugVec = baseVector;
		rigidbody.AddForce( baseVector);
		rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		
		Debug.Log("Camera left and right world space at z = 1.58 " + Camera.mainCamera.ViewportToWorldPoint(new Vector3(0F,0.5F,1.58F)) + " and " + Camera.mainCamera.ViewportToWorldPoint(new Vector3(1F,0.5F,1.58F)));
		
	}
	
	
	public void Reset(){
		rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

		transform.position = new Vector3(Random.Range(-6.5F,6.5F),Random.Range(0.25F,2.7F),Random.Range(-2.3F,1.5F));
		startingPoint = transform.position;
		destVect = hitPoint - startingPoint;
		distance = destVect.magnitude;
		flyingTime = distance/distanceTimeCoefficent;

		ownTime = 0;
		god.TempReset(flyingTime);
		rigidbody.velocity = Vector3.zero;
		animation.enabled = true;
		Debug.Log(flyingTime + " hai " + distance);

	}
	
	
}

