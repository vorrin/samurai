using UnityEngine;
using System.Collections;

public class OminoController : MonoBehaviour {
	
	private GameObject testa;
	public int headStrength = 100;
	
	// Use this for initialization
	void Start () {
		testa = transform.Find("Head").gameObject;
	}
	
	void SwingBat(){
		Debug.Log("adding a force");
		testa.rigidbody.AddForce(Random.Range(-headStrength,headStrength),Random.Range(-headStrength,headStrength),Random.Range(-headStrength,headStrength));	
		//testa.rigidbody.AddForce(1000,0,0);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
