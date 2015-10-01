using UnityEngine;
using System.Collections;
using System;

public class EnemyChasesMinDistance : MonoBehaviour {
	/*Призначення:
	 * - ворог знаходить гравця і підходить лише на певну відстань до нього, потім зупиняється;
	 * - якщо гравець віддаляється - повтор.
	 */
	public float moveSpeed = 2;
	private Transform player;
	private float range;
	public float maxDist = 10;
	public float minDist = 5;

	void Start () {
		try{
			player = GameObject.FindGameObjectWithTag ("Player").transform;	//знаходить гравця по тегу
		}
		catch (Exception e){
			enabled = false;
		}
	}

	void Update () 
	{
		if (player) {
			if (Vector2.Distance (transform.position, player.position) >= minDist) {
				transform.position = Vector2.MoveTowards (transform.position, player.position, moveSpeed * Time.deltaTime);

				if (Vector2.Distance (transform.position, player.position) <= maxDist) {
					//a place to add some functions like shoot, spawn monster etc.
				} 			
			}
		}
	}
}
