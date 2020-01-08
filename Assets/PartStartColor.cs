using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartStartColor : MonoBehaviour {

	public Color c0;
	public Color c1;

	private ParticleSystem[] ps;

	public void InitiateColor () {
		ps = GetComponentsInChildren<ParticleSystem> ();
		var mainParent = ps[0].main;
		var mainChild  = ps[1].main;

		mainParent.startColor = new Color(c0.r, c0.g, c0.b, 1);
		mainChild.startColor  = new Color(c1.r, c1.g, c1.b, 1);
	}
	
}
