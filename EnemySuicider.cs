using UnityEngine;
using System.Collections;
using System;

public class EnemySuicider : MonoBehaviour {
	public float moveSpeed = 5;
	public float detonateDistance = 1.5f;
	public float fuseTimer = 2.5f;
	public float explosionRadius = 3f;
	public float bombForce = 10f;
	public int bombDmg = 20;
	public GameObject explosion;			// Prefab of explosion effect.
	private Transform player;
	private Animator animator;

	void Awake () {
		try{
			player = GameObject.FindGameObjectWithTag ("Player").transform;	//знаходить гравця по тегу
			animator = GetComponent<Animator> ();
		}
		catch (Exception e){
			enabled = false;
		}
	}
	
	void Update () {
		if (player){ 
			if (Vector2.Distance(transform.position, player.position) > detonateDistance)
				transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
			else if (Vector2.Distance(transform.position, player.position) < detonateDistance){
				moveSpeed = 0;
			    StartCoroutine (BeginDetonation ());
			}
		}
		else if (player==null)
			enabled = false;
	}

	IEnumerator BeginDetonation ()
	{
		yield return new WaitForSeconds (fuseTimer);
		Explode ();
	}

	public void Explode()
	{						
		// Find all the colliders on the Enemies layer within the bombRadius.
		Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius, 1 << LayerMask.NameToLayer("Players"));
		animator.SetTrigger ("Attacks");
		foreach(Collider2D tar in targets)
		{
			Rigidbody2D rb = tar.GetComponent<Rigidbody2D>();
			if(rb != null && rb.tag == "Player")
			{
				rb.gameObject.GetComponent<PlayerGeneral>().TakeMeleeDamage(bombDmg);

				// Find a vector from the bomb to the enemy.
				Vector3 deltaPos = rb.transform.position - transform.position;
				
				// Apply a force in this direction with a magnitude of bombForce.
				Vector3 force = deltaPos.normalized * bombForce;
				rb.AddForce(force);
			}
		}
		// Instantiate the explosion prefab.
		Instantiate(explosion,transform.position, Quaternion.identity);
		
		Destroy (gameObject);
	}
}
