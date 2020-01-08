using System.Collections;
using System.Collections.Generic;
using HappyFunTimes;
using UnityEngine;

public class scrPlayerMan : MonoBehaviour {

	public int nbEnnemisMax;
	public int nbTotalCree;
	private Queue<GameObject> qPlayers; // file d'attente des joueurs connectes

	public void inscrire(GameObject player) {
		qPlayers.Enqueue (player);
	}

	public void activerPlayer () {
		nbTotalCree++;

		GameObject newPlayer = qPlayers.Dequeue ();
		NetPlayer newNetPlayer = newPlayer.GetComponent<HFTGamepad> ().NetPlayer;

		if (newPlayer != null) {	// la file n'est pas vide et l'objet est associe a un telephone connecte
			if (!string.Equals (newPlayer.GetComponent<HFTGamepad> ().Name, "nope")) {	// le joueur a choisi son nom
				newPlayer.GetComponent<PlayerControlToPrefab> ().activer ();
			} else {
				qPlayers.Enqueue (newPlayer);
			}
		}
	}



	void Start () {
		qPlayers = new Queue<GameObject> ();
		nbTotalCree = 0;
	}
	

	void Update () {
		int nbMissiles = GameObject.FindGameObjectsWithTag ("jpublic").Length;

		Debug.Log ("nb file att : " + qPlayers.Count);
		Debug.Log ("nb missiles : " + nbMissiles);
		if (qPlayers.Count > 0 && nbMissiles < nbEnnemisMax) {
			Debug.Log ("on dequeue");
			activerPlayer ();
		}


	}
}
