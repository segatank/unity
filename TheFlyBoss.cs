using UnityEngine;
using System.Collections;

public class TheFlyBoss : MonoBehaviour {
	/*Призначення:
	 - босс вибирає випадкову точку на карті та рухається у її напрямку;
	 - якщо йому трапиться сторонній об*єкт або він дійде до точки - обирається новий напрямок;
	 - коли у боса менше половини хп - він пришвидшується.
	 */

	public float initialMoveSpeed = 5f;
	public float enragedSpeed = 10f;
	Vector2 destinationPoint;

	EnemyHealth theBody;
	public float xMax = 50;
	public float yMax = 50;
	
	void Start () {
		ChoosePoint ();
	}
	
	void Update () {
		Enrage ();
		transform.position = Vector2.MoveTowards (transform.position, destinationPoint, initialMoveSpeed * Time.deltaTime);
		if (Vector2.Distance(transform.position, destinationPoint) <2f)
			ChoosePoint ();
	}

	public void Enrage(){
		theBody = gameObject.GetComponent<EnemyHealth>();
		if (theBody.currentHp < theBody.startingHp / 2)
			initialMoveSpeed = enragedSpeed;
	}
	
	void ChoosePoint(){
		destinationPoint.x = Random.Range (0, xMax);
		destinationPoint.y = Random.Range (0, yMax);
	}
	
	void OnCollisionStay2D(Collision2D otherObject)
	{
		ChoosePoint ();
	}
}
