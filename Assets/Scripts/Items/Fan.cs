﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;

public class Fan : _Equipable {

	[Header("Fan fields")]

	[Tooltip("Top speed the lilypad will reach.")]
	public float speed = 1;
	[Tooltip("Similar to acceleration.")]
	public float power = 1;

	public ParticleSystem particles;

	// Reset to lastY when dropped. Is set when picked up
	private float lastY;

	protected override void Start() {
		base.Start();

		SetParticleEmission(false);
	}

	void OnElectrify() {
		// Error checking
		if (inventory == null)
			return;

		Lilypad lilypad = inventory.movement.platform as Lilypad;
		if (lilypad != null) {
			// Move objects
			Vector3 move = -transform.forward * speed;
			lilypad.Move(move, power);
		}
	}

	void OnElectrifyStart() {
		SetParticleEmission(true);
	}

	void OnElectrifyEnd() {
		SetParticleEmission(false);
	}

	void SetParticleEmission(bool state) {
		if (particles == null) return;

		foreach(var ps in particles.GetComponentsInChildren<ParticleSystem>()) {
			var em = ps.emission;
			em.enabled = state;
			if (state && !ps.isPlaying) ps.Play();
		}
	}

}