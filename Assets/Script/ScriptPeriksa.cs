using UnityEngine;
using System.Collections;

public class ScriptPeriksa : MonoBehaviour {

	public GameObject PemuatSoal;
	public GameObject publicScript;

	public void setGameObject(GameObject a, GameObject b){
		this.PemuatSoal = a;
		this.publicScript = b;
	}

	public void Periksa(){
		PemuatSoal.GetComponent<KuisScript>().PeriksaKuis();
	}

	public void Ulangi(){
		PemuatSoal.GetComponent<KuisScript>().UlangiKuis();
	}

	public void Keluar(){
		publicScript.GetComponent<ButtonScript>().ButtonCloseKuis();
	}
}
