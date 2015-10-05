using UnityEngine;
using System.Collections;

public class EnRndWanderer : MonoBehaviour {
	/*Призначення:
	 - ворог вибирає випадкову точку на карті та рухається у її напрямку;
	 - якщо йому трапиться сторонній об*єкт або він дійде до точки - обирається новий напрямок
	 */
	public float moveSpeed = 5;
	Vector2 destinationPoint;
	float xPoint;
	float yPoint;

	public int xMax = 50;
	public int yMax = 50;

	void Start () {
		ChoosePoint ();
	}

	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, destinationPoint, moveSpeed * Time.deltaTime);
		if (Vector2.Distance(transform.position, destinationPoint) <2f)
			ChoosePoint ();
	}

	void ChoosePoint(){
		xPoint = Random.Range (0, xMax);
		yPoint = Random.Range (0, yMax);
		destinationPoint.x = xPoint;
		destinationPoint.y = yPoint;
	}

	void OnCollisionStay2D(Collision2D otherObject)
	{
		ChoosePoint ();
	}
}
