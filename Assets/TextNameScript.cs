using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNameScript : MonoBehaviour {

	private Text txt;
	private string texteAffiche;


	public void phrase(string nom) {
		string p = "";
		switch (Random.Range (0, 3)) {
		case 0:
			p = "Bravo " + nom + " !";
			break;
		case 1:
			p = "GG " + nom;
			break;
		case 2:
			p = nom + " marque des points !";
			break;
		}
		texteAffiche = p;
	}

	void Start () {
		txt = GetComponent<Text> ();
		texteAffiche = "";
	}
	

	void Update () {
		txt.text = texteAffiche;
	}
}
