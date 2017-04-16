using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
	public string level;
	// Use this for initialization
	void Start () {
		StartCoroutine(Delay());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator Delay(){
		yield return new WaitForSeconds (5);
		Application.LoadLevel (level);
	}
}
