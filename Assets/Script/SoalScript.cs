using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SoalScript : MonoBehaviour {

	public GameObject Soal;
	public GameObject NoSoal;
	public int noSoalInt;

	GameObject pemuatsoal;

	public List<GameObject> Jawaban = new List<GameObject>();
	public List<bool> SimpanJawaban = new List<bool>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetTextSoal(string TextSoal, int No){
		Soal.GetComponent<Text> ().text = TextSoal;
		NoSoal.GetComponent<Text> ().text = "" + No;
		noSoalInt = No;

		this.pemuatsoal = this.gameObject;
	}

	public void setTextJawaban(string[] TextJawabans){
		int index = 0;
		foreach(GameObject textjawaban in Jawaban){
			Text txt=  textjawaban.GetComponentInChildren<Text>();
			txt.text = TextJawabans[index];

			SimpanJawaban.Add(false);

			ScriptJawaban jawaban = textjawaban.GetComponent<ScriptJawaban>();
			jawaban.setJawaban(noSoalInt, index);
			jawaban.PemuatSoal = this.pemuatsoal;
			index++;
		}
	}
}
