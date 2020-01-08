using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour {

	public float vitesse;
	public float vitesseAng;

	public AudioClip sndExplosion;

	public Sprite spr1;
	public Sprite spr2;
	public Sprite spr3;

	public Material mat1;
	public Material mat2;
	public string colorString;	// red, pink, yellow, green

	public GameObject partExplosion;
	public GameObject controlPrefab;

	private Transform tr;
	private Transform spriteTR;
	private SpriteRenderer sr;


	private float getAngle () { // renvoie l'angle en radian que fait le missile dans le plan XY
		return ((tr.position.z) % 360) * Mathf.PI / 180f;
	}

	private Vector3 getDirection () {
		Vector3 projection = new Vector3 (Mathf.Cos (getAngle ()), Mathf.Sin (getAngle ()), 0);
		return projection.normalized;
	}

	public void RotateTrigo () {
		tr.RotateAround (tr.position /*- 0.5f * getDirection()*/, new Vector3 (0,0,1), vitesseAng);
	}

	public void RotateAntiTrigo () {
		tr.RotateAround (tr.position /*- 0.5f * getDirection()*/, new Vector3 (0,0,1), -vitesseAng);
	}


	public void Start () {
		tr = GetComponent<Transform>();
		spriteTR = GetComponentInChildren<Transform> ();
		tr.RotateAround (tr.position, new Vector3 (0, 0, 1), Random.Range (0, 360));

		ParticleSystemRenderer pr = GetComponent<ParticleSystemRenderer> ();
		sr = GetComponentInChildren<SpriteRenderer> ();
		int n = Random.Range (1, 4); 
		switch (n) {
		case 1:
			sr.sprite = spr1;
			pr.material = mat1;
			break;
		case 2:
			sr.sprite = spr2;
			pr.material = mat2;
			break;
		case 3:
			sr.sprite = spr3;
			pr.material = mat1;
			break;
		}


	}

	public void Update() {
		tr.Translate (getDirection() * vitesse);

		// Sortie de l'ecran. Deja present dans PlayerControlToPrefab mais peut etre cancel si disconnect
		if (tr.position.magnitude > 20) {
			if (controlPrefab != null) {
				controlPrefab.GetComponent<PlayerControlToPrefab> ().desactiver ();
			}
			Destroy (this.gameObject);
		}
	}


	public void OnTriggerEnter2D (Collider2D other) {
		if (string.Equals (other.gameObject.tag, "jpublic")) {
			// Création et paramétrage des particules d'explosion
			GameObject explosion = Instantiate (partExplosion);
			Color c0 = GetComponentInChildren<SpriteRenderer> ().color;
			Color c1 = other.gameObject.GetComponentInChildren<SpriteRenderer> ().color;
			PartStartColor explosionColor = explosion.GetComponent<PartStartColor> ();
			explosionColor.c0 = c0;
			explosionColor.c1 = c1;
			explosionColor.InitiateColor ();
			explosion.GetComponent<Transform> ().position = tr.position;

			other.gameObject.GetComponent<MissileScript> ().controlPrefab.GetComponent<PlayerControlToPrefab> ().desactiver ();
			controlPrefab.GetComponent<PlayerControlToPrefab> ().desactiver ();
			Destroy (other.gameObject);
			Destroy (this.gameObject);

		} else if (string.Equals (other.gameObject.tag, "jscene")) {
			GameObject explosion = Instantiate (partExplosion);
			PartStartColor explosionColor = explosion.GetComponent<PartStartColor> ();
			explosionColor.c0 = GetComponentInChildren<SpriteRenderer> ().color;
			explosionColor.c1 = new Color(1,1,1);
			explosionColor.InitiateColor ();
			explosion.GetComponent<Transform> ().position = tr.position;

			AudioSource asrc = GameObject.Find ("Camera").GetComponent<AudioSource> ();
			if (!asrc.isPlaying) {
				asrc.clip = sndExplosion;
				asrc.volume = 0.6f;
				asrc.Play ();
			}

			controlPrefab.GetComponent<PlayerControlToPrefab> ().desactiver ();
			Destroy (this.gameObject);
		}
	}
}
