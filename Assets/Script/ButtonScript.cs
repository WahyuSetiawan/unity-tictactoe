using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class ButtonScript : MonoBehaviour {
	public Texture bgKeluar;
	public Texture bgMenuUtama;
	public Texture bgIntruksi;
	public Texture bgLoading;
	public Texture bgKuis;

	public Texture MuteOff;
	public Texture MuteOffPress;
	public Texture MuteOn;
	public Texture MuteOnPress;

	public GameObject MenuUtama;

	public GameObject BackgroundObject;
	public GameObject ObjectMute;

	public GameObject SoundGameObject;

	bool muteActivStatus;

	/*
	 * 
	 * Untuk Button Mulai
	 * 
	 */

	public void Start(){
		Debug.Log (PlayerPrefs.GetFloat ("prefsVolume")); //menampilkan debug untuk penyimpanan database volume music (mute atau tidak)
		SoundGameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("prefsVolume"); // set music pada pertama scene akan diaktifkan mute atau tidaknya music background
		//SoundGameObject.GetComponent<AudioSource>().volume = 0; 
	}

	public void ButtonMulai(){
		BackgroundObject.GetComponent<RawImage> ().texture = bgLoading; //mengganti  background latar belakang dengan backgoand loading untuk menutupi lama starup system
		MenuUtama.SetActive (false); //menonaktifkan panel menu utama
		PlayerPrefs.SetFloat ("prefsVolume", SoundGameObject.GetComponent<AudioSource>().volume); // menyimpan kedalam database volume music
		Application.LoadLevel ("AR"); // memangil atau meload scene ar
	}

	/*
	 * 
	 * Untuk Button Instruksi
	 * 
	 */

	public GameObject MenuInstruksi;

	public void ButtonKeluarInstruksi(){
		MenuInstruksi.SetActive (false); // menonaktifkan panel instruksi
		MenuUtama.SetActive (true); // mengaktifkan panel menuutama
		
		BackgroundObject.GetComponent<RawImage> ().texture = bgMenuUtama; // menggantikan background ke background menu utama
	}

	public void ButtonInstruksi(){
		MenuInstruksi.SetActive (true); // menngaktifkan panel instruksi
		MenuUtama.SetActive (false);// mengaktifkan panel menu utama

		BackgroundObject.GetComponent<RawImage> ().texture = bgIntruksi; // menganti background dengan backround instruksi
	}



	/*
	 * 
	 * Untuk Button Mute dan Konigurasinya
	 * 
	 */

	public void ButtonMute(){
		Debug.Log ("Mute");
		Button but = ObjectMute.GetComponent<Button>(); // mengantikan gambar pada tombol mute dengan gambar mute

		if (muteActivStatus) { // menentukan mute aktif atau tidak
			SoundGameObject.GetComponent<AudioSource>().volume = 1; // menaikan volume ke volume penuh
			ObjectMute.GetComponent<RawImage>().texture = MuteOff; // mengatikan gambar tombol volume dengan mute
		} else {
			SoundGameObject.GetComponent<AudioSource>().volume = 0; // menurunkan volume ke mute
			ObjectMute.GetComponent<RawImage>().texture = MuteOn; // mengantikan gambar tombol audio ke nonmute
		}

		muteActivStatus = !muteActivStatus; // mengantik niali variable mute
	}

	/*
	 * 
	 * Untuk Button Keluar
	 * 
	 */


	public GameObject ModelKeluar;

	public void ButtonKeluarYa(){
		Application.Quit (); // mematikan atau keluar dari aplikasi
	}

	public void ButtonKeluarTidak(){
		ModelKeluar.SetActive (false); // menonaktifkan panel keluar
		MenuUtama.SetActive (true); // mengaktifkan panel menuutama

		BackgroundObject.GetComponent<RawImage> ().texture = bgMenuUtama; // mengantikan background ke background menuutama
	}

	public void ButtonKeluar(){
		ModelKeluar.SetActive (true); // mengaktifkan panel keluar
		MenuUtama.SetActive (false); // menonaktfkan panel menuutama
		
		BackgroundObject.GetComponent<RawImage> ().texture = bgKeluar; // mengantikan background ke background keluar
	}

	/*
	 * 
	 * 
	 * Untuk kuis 
	 * 
	 */

	public GameObject MenuKuis;
	public GameObject Kuis;

	KuisScript scriptKuis;

	public void ButtonKuis(){
		MenuUtama.SetActive (false);

		MenuKuis.SetActive (true);
		
		scriptKuis = Kuis.GetComponent<KuisScript> ();
		scriptKuis.loadKuis ();
		BackgroundObject.GetComponent<RawImage> ().texture = bgKuis;
	}
	
	public void ButtonCloseKuis(){
		MenuUtama.SetActive (true);
		MenuKuis.SetActive (false);

		scriptKuis = Kuis.GetComponent<KuisScript> ();
		scriptKuis.clearSoal ();
		BackgroundObject.GetComponent<RawImage> ().texture = bgMenuUtama;
	}

}
