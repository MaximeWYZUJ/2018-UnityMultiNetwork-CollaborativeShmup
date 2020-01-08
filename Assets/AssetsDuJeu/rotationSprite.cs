using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationSprite : MonoBehaviour {

	private Transform tr;
	private SpriteRenderer sr;

	void Start () {
		tr = GetComponent<Transform> ();
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (string.Equals (sr.sprite.name, "basicEnnemi1")) {	// carre
			tr.RotateAround (tr.position, new Vector3 (0, 0, 1), 4);
		}
		if (string.Equals (sr.sprite.name, "basicEnnemi2")) {	// shuriken
			tr.RotateAround (tr.position, new Vector3 (0, 0, 1), 6);
		}
	}
}
