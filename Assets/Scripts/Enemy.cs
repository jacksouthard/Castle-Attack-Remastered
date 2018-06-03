﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	enum State {
		moving,
		attacking,
		dead
	};
	State state = State.moving;

	public float health;
	public float movementSpeed;

	float slopeModifier = 0.05f; // ratio multiplied to speed based on how close the enemies rotation is to 90
	float xPos;
	float steepness = 0f;

	public float damage;
	public float attackSpeed;
	float attackTimer = 0f;

	Animator anim;

	void Start () {
		anim = GetComponentInChildren<Animator> ();
		xPos = transform.position.x;
	}
	
	void Update () {
		if (state == State.moving) {
			Move ();
		} else if (state == State.attacking) {
			attackTimer -= Time.deltaTime;
			if (attackTimer <= 0f) {
				attackTimer = attackSpeed;
				Attack ();
			}
		}
	}

	void Move () {
		float speedMultiplier = Mathf.Lerp (1f, slopeModifier, steepness / 90f);
		xPos -= (movementSpeed * speedMultiplier * Time.deltaTime);
		TerrainGenerator.PointData pointData = TerrainGenerator.instance.GetPointDataAtPos (xPos);
		transform.position = new Vector3 (xPos, pointData.yPos, 0);

		float newSteepness = Mathf.Abs (pointData.angle);
		if (newSteepness != steepness) {
			transform.rotation = Quaternion.Euler (0f, 0f, pointData.angle);
			steepness = newSteepness;
		}
	}

	public void TakeDamage (float damage) {
		health -= damage;
		if (health <= 0f) {
			Die ();
		}
	}

	void Die () {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag == "Base") {
			StartAttack ();
		}
	}

	void StartAttack () {
		state = State.attacking;
	}

	void Attack () {
		// assign damage
		anim.SetTrigger("Attack");
	}

	void EndAttack () {
		state = State.moving;
		anim.SetTrigger("Walk");
	}
}
