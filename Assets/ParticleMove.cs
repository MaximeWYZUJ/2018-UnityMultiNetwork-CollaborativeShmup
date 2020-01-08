using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour {

	private ParticleSystem ps;
	public Vector3 destination;
	private ParticleSystem.Particle[] particules;
	private int nbPart;
	private bool[] hasStopped;
	public float seuil;


	void Start () {
		ps = GetComponent<ParticleSystem> ();
		particules = new ParticleSystem.Particle[ps.main.maxParticles];
		nbPart = ps.GetParticles (particules);
		hasStopped = new bool[ps.main.maxParticles];
	}
	
	// Update is called once per frame
	void Update () {
		/*
		for (int i = 0; i < ps.main.maxParticles; i++) {
			Debug.Log (particules [i].velocity.magnitude);
			if (particules [i].velocity.magnitude < seuil) {
				hasStopped [i] = true;
			}

			if (hasStopped [i]) {
				particules [i].position = 0.7f * particules [i].position + 0.3f * destination;
			}
		}*/
	}
}
