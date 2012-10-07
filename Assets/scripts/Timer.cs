using UnityEngine;
using System.Collections;


public class Timer : MonoBehaviour {
	
	private UILabel timeLabel;
	private float time = 1F;
	private bool paused = false;
	private God god;
	private TweenColor colorTween;
	private TweenScale scaleTween;
	
	
	// Use this for initialization
	void Start () {
		
		timeLabel = gameObject.GetComponentInChildren<UILabel>();
		god = transform.parent.gameObject.GetComponent<God>();
		colorTween = gameObject.GetComponentInChildren<TweenColor>();
		scaleTween = gameObject.GetComponentInChildren<TweenScale>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (paused) return;
		time -= Time.deltaTime;
		string roundedTime = time.ToString("F2");
		timeLabel.text = roundedTime;
	}
	
	void Hit() {
		paused = ! paused;
		god.BroadcastMessage("TimeLeft",time);
				
	}
	
	public void Reset(float countdown) {
		time = countdown;
		paused = false;
		//Debug.Log(tweener.from);
		timeLabel.color = colorTween.from;
		timeLabel.transform.localScale = new Vector3(1,1,1);
		colorTween.duration = countdown;
		colorTween.enabled = true;
		scaleTween.duration = countdown;
		scaleTween.enabled = true;
		
		
	}
	
	
}
