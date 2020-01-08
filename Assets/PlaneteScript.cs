using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneteScript : MonoBehaviour {

	public AudioClip sndPoints;

	public GameObject textScoreObject;
	private Text textScore;
	private int score;

	public GameObject textNameObject;

	public GameObject explosion;
	public string colorString;

	void Start () {
		textScore = textScoreObject.GetComponent<Text> ();
	}
	
	void Update () {
		textScore.text = score.ToString();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (string.Equals (other.gameObject.tag, "jpublic")) {
			MissileScript scr = other.gameObject.GetComponent<MissileScript> ();
			if (string.Equals (this.colorString, scr.colorString)) {
				score += 100;

				textNameObject.GetComponent<TextNameScript> ().phrase (other.gameObject.GetComponent<MissileScript> ().controlPrefab.GetComponent<HFTGamepad> ().Name);

				GameObject part = Instantiate (explosion);
				part.GetComponent<Transform> ().localScale = 0.5f * new Vector3 (1, 1, 1);
				part.GetComponent<Transform> ().position = GetComponent<Transform> ().position;
				part.GetComponent<PartStartColor> ().c0 = GetComponent<SpriteRenderer> ().color;
				part.GetComponent<PartStartColor> ().c1 = GetComponent<SpriteRenderer> ().color;
				part.GetComponent<PartStartColor> ().InitiateColor ();

				AudioSource asrc = GameObject.Find ("Camera").GetComponent<AudioSource> ();
				if (!asrc.isPlaying) {
					asrc.clip = sndPoints;
					asrc.volume = 1f;
					asrc.Play ();
				}

				scr.controlPrefab.GetComponent<PlayerControlToPrefab> ().desactiver ();
				Destroy (other.gameObject);
			}
		}
	}
}
