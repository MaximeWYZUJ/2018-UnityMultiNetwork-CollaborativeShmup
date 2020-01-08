using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaisseauScript : MonoBehaviour {

	private Transform tr;

	//public GameObject laserShot;
	//private float shotCooldown;
	//private float instantShot;

	public float vitesseAng;
	private float angle;		// en radian
	public float rayon;

	private float fuel;

	void Start () {
		angle = -Mathf.PI / 2;
		rayon = 4f;
		tr = GetComponent<Transform> ();
	}
	
	public void MoveLeft () {
		angle -= vitesseAng;
		Vector3 oldPosition = tr.position;
		tr.position = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0) * rayon;

		float angleDiff = Vector3.Angle (tr.position, oldPosition);
		tr.RotateAround (tr.position, new Vector3 (0, 0, 1), -angleDiff);
	}

	public void MoveRight () {
		angle += vitesseAng;
		Vector3 oldPosition = tr.position;
		tr.position = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0) * rayon;

		float angleDiff = Vector3.Angle (tr.position, oldPosition);
		tr.RotateAround (tr.position, new Vector3 (0, 0, 1), angleDiff);
		//tr.LookAt (Vector3.zero);
	}

	public void Shake (float intensite) {
		
	}

	public void StopShake() {
		//GetComponentInChildren<SpriteRenderer> ().color = Color.white;
	}
}
