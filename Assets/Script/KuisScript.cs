using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KuisScript : MonoBehaviour {

	[System.Serializable]
	public class Jawaban{
		public string Text;
		public bool jawaban;
	}
	
	[System.Serializable]
	public class Soal{
		public string Text;
		public Jawaban[] jawabans = new Jawaban[4];
	}

	[System.Serializable]
	public class rekapPeriksa
	{
		public bool jawabanBenar;
		public bool jawabanSalah;
	}


	public GameObject SoalPrefabs;
	public GameObject scrollbar;
	public GameObject EndSoal;
	public GameObject scriptGlobal;


	public GameObject textNilai;
	public GameObject textBenar;
	public GameObject textSalah;

	public Soal[] Soals;
	RectTransform selfsize;
	GameObject EndInstance;

	public List<GameObject> soalsInstitiate = new List<GameObject>();

	public void loadKuis(){
		Debug.Log ("Load Kuis");
		int Urutan = 1;

		selfsize = GetComponent<RectTransform> ();
		selfsize.sizeDelta = new Vector2 (selfsize.sizeDelta.x, (Soals.Length) * 200);

		foreach(Soal soal in Soals){
			GameObject soalInstance = Instantiate(SoalPrefabs, Position(Urutan), Quaternion.identity) as GameObject;
			soalsInstitiate.Add(soalInstance);
			soalInstance.SetActive(true);
			soalInstance.transform.SetParent(transform);
			soalInstance.name = "Soal " + (Urutan);

			RectTransform transformPrefabs = soalInstance.GetComponent<RectTransform>();
			transformPrefabs.anchoredPosition3D = Position(Urutan);
			transformPrefabs.localScale = new Vector3(1,1,1);

			SoalScript scriptSoal = soalInstance.GetComponent<SoalScript>();
			scriptSoal.SetTextSoal(soal.Text, Urutan);

			string[] jawabanText = new string[soal.jawabans.Length];
			int index = 0;
			foreach(Jawaban jawaban in soal.jawabans){
				jawabanText[index] = jawaban.Text;
				index++;
			}

			scriptSoal.setTextJawaban(jawabanText);

			Urutan ++ ;
		}

		EndInstance = Instantiate(EndSoal, Position(Urutan), Quaternion.identity) as GameObject;
		EndInstance.SetActive(true);
		EndInstance.GetComponent<ScriptPeriksa> ().setGameObject (this.gameObject, scriptGlobal);
		EndInstance.transform.SetParent(transform);
		EndInstance.name = "Soal " + (Urutan);

		RectTransform transformPrefabsEnd = EndInstance.GetComponent<RectTransform>();
		transformPrefabsEnd.anchoredPosition3D = new Vector3(0, (-200 * (Urutan - 1) + (GetComponent<RectTransform> ().sizeDelta.y / 2)) + 100,0);
		transformPrefabsEnd.localScale = new Vector3(1,1,1);
	}

	Vector3 Position(int Urutan){
		Vector3 origin = Vector3.zero;

		origin.y = -200 * (Urutan - 1) + (GetComponent<RectTransform> ().sizeDelta.y / 2);

		return origin;
	}

	public void clearSoal(){
		foreach (GameObject soal in soalsInstitiate) {
			Destroy(soal);
		}

		soalsInstitiate = new List<GameObject>();

		Destroy (EndInstance);
	}

	public void PeriksaKuis(){
		float nilai = 0f;
		float benar = 0f;
		float salah = 0f;

		foreach (GameObject soal in soalsInstitiate) {
			int index = 0;
			bool jawabanBenar = true;


			SoalScript ScriptSoal = soal.GetComponent<SoalScript>();
			List<rekapPeriksa> rekapPeriksas = new List<rekapPeriksa>();


			foreach(Jawaban jawaban in Soals[ScriptSoal.noSoalInt - 1].jawabans){
				Texture falseButton = ScriptSoal.Jawaban[index].GetComponent<ScriptJawaban>().falseButton;
				Texture trueButton = ScriptSoal.Jawaban[index].GetComponent<ScriptJawaban>().RightButton;
				Texture offButton = ScriptSoal.Jawaban[index].GetComponent<ScriptJawaban>().offButton;
				RawImage button = ScriptSoal.Jawaban[index].GetComponent<RawImage>();
				rekapPeriksa rekap = new rekapPeriksa();

				if(jawaban.jawaban){
					if(ScriptSoal.SimpanJawaban[index]){
						rekap.jawabanBenar = true;
						rekap.jawabanSalah = false;

						button.texture = trueButton;
					}else{
						rekap.jawabanBenar = false;
						rekap.jawabanSalah = true;

						button.texture = falseButton;
					}
				}else{
					if(ScriptSoal.SimpanJawaban[index]){
						rekap.jawabanBenar = false;
						rekap.jawabanSalah = true;

						button.texture = falseButton;
					}else{
						rekap.jawabanBenar = true;
						rekap.jawabanSalah = false;

						button.texture = offButton;
					}
				}

				rekapPeriksas.Add(rekap);

				index++;

				Debug.Log(index);
			}

			foreach(rekapPeriksa rekap in rekapPeriksas){
				if(rekap.jawabanSalah){
					jawabanBenar = false;
				}
			}

			if(jawabanBenar){
				benar = benar + 1f;
			}else{
				salah = salah + 1f;
			}

		}

		nilai = benar / (benar + salah) * 100;
		textNilai.GetComponent<Text> ().text = "Nilai : " + nilai;
		textBenar.GetComponent<Text> ().text = "Benar : " + benar;
		textSalah.GetComponent<Text> ().text = "Salah : " + salah;
	}

	public void UlangiKuis(){
		clearSoal ();
		this.loadKuis ();
	}
}
