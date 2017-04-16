using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptJawaban : MonoBehaviour {
	public string TextJawaban;
	public int NoSoal;
	public int nojawaban = 0;

	public Texture enableButton;
	public Texture offButton;
	public Texture falseButton;
	public Texture RightButton;

	public GameObject PemuatSoal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void jawaban(){
		SoalScript Soal = PemuatSoal.GetComponent<SoalScript> ();
		Soal.SimpanJawaban [nojawaban] = !Soal.SimpanJawaban [nojawaban];

		if (Soal.SimpanJawaban [nojawaban]) {
			GetComponent<RawImage> ().texture = enableButton;
		} else {
			GetComponent<RawImage>().texture = offButton;
		}
	}

	public void setJawaban(int noSoal, int noJawaban){
		NoSoal = noSoal;
		this.nojawaban = noJawaban;
	}
}
