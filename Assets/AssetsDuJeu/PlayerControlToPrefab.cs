using System;
using System.Collections;
using System.Collections.Generic;
using HappyFunTimes;
using UnityEngine;

public class PlayerControlToPrefab : MonoBehaviour {

	private GameObject VaisseauScene; // un prefab ne peut pas avoir un element de scene en public
	public GameObject MissilePrefab;

	public Vector3 c0;	// rouge
	public Vector3 c1;	// rose
//	public Vector3 c2;	// bleu
	public Vector3 c3;	// vert
	public Vector3 c4;	// jaune
	private string colorString;

	private scrPlayerMan scrPlayerMan;
	private GameObject objectControlled;
	private VaisseauScript scrv;
	private MissileScript scrm;
	private HFTGamepad gp;
	private HFTInput input;
	private string oldGpName;
	private PlayerSpawner scrPlayerSpawner;
	private bool actif;

	private delegate void Move();
	private Move move;


	public Vector3 selectRandomColor() {
		int i = GameObject.Find ("PlayerMan").GetComponent<scrPlayerMan> ().nbTotalCree % 4;
		Vector3 c;
		switch (i) {
		case 0:
			c = c0;
			colorString = "red";
			break;
		case 1:
			c = c1;
			colorString = "pink";
			break;
//		case 2:
//			c=c2;
//			break;
		case 2:
			c = c3;
			colorString = "green";
			break;
		case 3:
			c = c4;
			colorString = "yellow";
			break;
		default:
			c = Vector3.zero;
			break;
		}
		return c;
	}

	public void selfDestroy(object sender, EventArgs e) {
		NetPlayer np = (NetPlayer)sender;
		if (np.Equals (this.GetComponent<HFTGamepad> ().NetPlayer)) {
			Destroy (this.gameObject);
		}
	}

	private void ShipMove () {
		// Déplacement horizontal
		if (input.GetAxis ("Horizontal") < 0) {
			scrv.MoveLeft ();
		} else if (input.GetAxis ("Horizontal") > 0) {
			scrv.MoveRight ();
		}

		float acc = input.gyro.userAcceleration.magnitude;
		if (acc > 20) {
			scrv.Shake (acc);
		} else {
			scrv.StopShake ();
		}
	}

	private void RocketMove(){
		if (input.GetAxis ("Horizontal") < 0) {
			scrm.RotateTrigo ();
		} else if (input.GetAxis ("Horizontal") > 0) {
			scrm.RotateAntiTrigo ();
		}
	}

	public void activer() {
		actif = true;

		if (string.Equals (gp.Name, "v")) {
			objectControlled = VaisseauScene;
			scrv = VaisseauScene.GetComponent<VaisseauScript> ();
			move = ShipMove;
		} else {
			if (!string.Equals (gp.Name, "nope")) {
				objectControlled = Instantiate (MissilePrefab);
				objectControlled.GetComponent<MissileScript> ().controlPrefab = this.gameObject;

				int i = UnityEngine.Random.Range (0, 5);
				Vector3 c = selectRandomColor ();
				c = c / 255;
				gp.Color = new Color(c.x,c.y,c.z);
				objectControlled.GetComponentInChildren<SpriteRenderer> ().color = new Color(c.x, c.y, c.z);
				scrm = objectControlled.GetComponent<MissileScript> ();
				scrm.colorString = colorString;
				move = RocketMove;
			} else {
				objectControlled = null;
				scrm = null; scrv = null;
				move = null;
			}
		}
	}

	public void desactiver() {
		gp.Color = new Color (0.4f, 0.4f, 0.4f);
		actif = false;
		scrPlayerMan.inscrire (this.gameObject);
	}



	void Start () {
		actif = false;
		gp = GetComponent<HFTGamepad> ();
		input = GetComponent<HFTInput> ();
		VaisseauScene = GameObject.Find ("Vaisseau");
		scrPlayerSpawner = GameObject.Find ("PlayerSpawner").GetComponent<PlayerSpawner> ();
		scrPlayerMan = GameObject.Find ("PlayerMan").GetComponent<scrPlayerMan> ();
		oldGpName = gp.Name;

		scrPlayerMan.inscrire (this.gameObject);
		gp.NetPlayer.OnDisconnect += selfDestroy;
	}
	

	void Update () {
		/*if (!string.Equals (oldGpName, gp.Name)) {	// le gp a change de nom
			activer ();
			oldGpName = gp.Name;
		}

		Debug.Log ("gp name : " + gp.Name);
		Debug.Log ("obj name : " + objectControlled.name);*/

		if (objectControlled != null && actif) {	// on controle soit un vaisseau, soit un missile
			move ();

			if (!string.Equals (objectControlled.name, "Vaisseau")) {	// on controle un missile
				if (objectControlled.GetComponent<Transform> ().position.magnitude > 10) {	// le missile est hors de l'écran
					Destroy (objectControlled);
					desactiver ();
				}
			}
		}
	}
}
