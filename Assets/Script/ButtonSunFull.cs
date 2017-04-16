using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class ButtonSunFull : MonoBehaviour {
	public GameObject script;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTouchDown(){
		script.GetComponent<ARButtonScript> ().loadInfoPlanet (name); // memanggil funtion loadinfoplanet di script ARButtonScript dan mengirinkan nama objek sebagai parameter
		GetComponent<AudioSource> ().Play (); // menjalankan audio informasi planet
	}
}
