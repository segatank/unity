using UnityEngine;
using System.Collections;
using System;

public class FlipEnemySide : MonoBehaviour {
	/*Призначення:
	 - міняє сторону, в яку дивиться монстр
	 - тобто обличчям завжди повернутий до гравця*/
	private Transform target;
	public bool facingRight = true;

	void Awake () {
		try {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		catch (Exception e){
			enabled = false;
		}
	}
	
	void Update () {
		if (target) {
			if (transform.position.x > target.position.x && facingRight)
				Flip ();
			else if (transform.position.x < target.position.x && !facingRight)
				Flip ();
		}
		else
			enabled = false;
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
