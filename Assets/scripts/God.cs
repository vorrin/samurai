using UnityEngine;
using System.Collections;

public class God : MonoBehaviour {
	
	public GameObject timer;
	private AudioSource audio;
	public AudioClip win;
	public AudioClip soso;
	public AudioClip lose;
	public Appletest apple;
	public GameObject omino;
	
	// Use this for initialization
	void Start () {
		timer = transform.FindChild("Timer").gameObject;
		//timer.BroadcastMessage("Reset",Random.Range(0,3));
		audio = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetMouseButtonDown(0)) {
			timer.BroadcastMessage("Hit");
			omino.BroadcastMessage("SwingBat");
		}
		if ( Input.GetMouseButtonDown(1)) {
			apple.rigidbody.AddForce(new Vector3(1,100,1));
		}
		
	
	}
	
	IEnumerator TimeLeft(float timeLeft) {
		
		if (Mathf.Abs(timeLeft) < 0.02){
			audio.clip = win;
			audio.Play();
			apple.Hit ();
		}
		else if (Mathf.Abs(timeLeft) < 0.1){
			audio.clip = soso;
			audio.Play();
			apple.Hit ();
		}
		else {
			audio.clip = lose;
			audio.Play();
		}
		yield return new WaitForSeconds(0.9F);
		float tmpNewTime = Random.Range(0.5F,3F);
			
		//Debug.Log(tmpNewTime);
		timer.BroadcastMessage("Reset",tmpNewTime);
		apple.BroadcastMessage("Reset");
		
		
	}
	
	public void TempReset(float time){
		timer.BroadcastMessage("Reset",time);
	}
	
}
