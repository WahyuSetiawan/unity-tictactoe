using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic ;

public class ARButtonScript : MonoBehaviour {
	/*
	 * membuat dinamik variable untuk menyimpan informasi untuk planet
	 * 
	 * */
	[System.Serializable]
	public class Planet{
		public string Title;
		public Texture picture;
		[TextArea(3,10)]
		public string text1;
	}

	public List<Planet> planets = new List<Planet>();

	public GameObject info;
	public GameObject textTitle ;
	public GameObject image;
	public GameObject textInfo;


	/*
	 * 
	 * Button info planet touch
	 * 
	 * 
	 */

	public void loadInfoPlanet(string title){
		foreach (Planet planet in planets){ // mengulangi semua planet variable 
			if(title  == planet.Title){ // membandingkan nama objek dan nama title sama atau tidak
				info.SetActive (true); // panel informasi diaktifkan atau dimunculkan
				textTitle.GetComponent<Text>().text = planet.Title; // mengantikan nama teks title dengan value title
				image.GetComponent<RawImage>().texture  = planet.picture; // mengantikan image dengan image planet
				textInfo.GetComponent<Text >().text = planet.text1; // mengatikan teks informasi dengan value informasi
			}
		}
	}

	public void CloseInfo(){
		info.SetActive (false); // menonaktifkan panel informasi
		textTitle.GetComponent<Text>().text = ""; // mengantikan text title dengan teks kosong
		textInfo.GetComponent<Text >().text = ""; // mengantikan text info dengan teks kosong
	}
	

	// Use this for initialization
	void Start () {
		this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("prefsVolume"); // mengantikan atau menset nilai dengan nilai volume yang ada di database
		this.GetComponent<AudioSource> ().Play (); // music dimulai
	}
	
	void Update ()
	{
		if (Application.platform == RuntimePlatform.Android) { // mengecek android jalan atau tidak
			if (Input.GetKeyDown (KeyCode.Escape)) { // apabila tombol keluar ditekan
				Application.LoadLevel ("MenuUtama"); // mejalankan atau meload menuutama
			}
		}
	}
}
