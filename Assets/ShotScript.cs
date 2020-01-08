using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

	private Transform tr;
	private float angle;
	private float shotSpeed;


	void Start () {
		tr = GetComponent<Transform> ();
		tr.position = GameObject.Find ("Vaisseau").GetComponent<Transform> ().position;

		tr.rotation = Quaternion.identity;
		angle = Vector2.Angle (new Vector2 (tr.position.x, tr.position.y), Vector2.zero);
		tr.RotateAround (tr.position, new Vector3 (0, 0, 1), angle);
	}
	

	void Update () {
		tr.Translate (new Vector3 (Mathf.Cos (angle * 180 / Mathf.PI), Mathf.Sin (angle * 180 / Mathf.PI), 0));

	}
}
