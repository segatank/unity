using UnityEngine;
using System.Collections;

public class PlaceRndDecorations : MonoBehaviour {
	/*Призначення:
	 - розміщює випадково на рівні декорації із заготовленого масиву
	 - робить перевірку, щоб не помістити декорацію на гравця чи іншу декорацію*/
	public GameObject[] decorationsArr;
	public Vector2 placePoint;
	public int decorNumber;
	public int minX = 0;
	public int maxX = 40;
	public int minY = 0;
	public int maxY = 40;
	public float placeRadius = 4;
	int chosenDecoration;

	void Awake(){
		placeDecorations ();
	}

	void Start () {
		//placeDecorations ();
	}

	void placeDecorations(){
		for (int counter=0; counter<=decorNumber; counter++) {
			chosenDecoration = Random.Range (0, decorationsArr.Length);
			placePoint.x = Random.Range (minX, maxX);
			placePoint.y = Random.Range (minY, maxY);
			Collider2D[] targets = Physics2D.OverlapCircleAll(placePoint, placeRadius, 1 << LayerMask.NameToLayer("Spawners"));
			Collider2D[] players = Physics2D.OverlapCircleAll(placePoint, placeRadius, 1 << LayerMask.NameToLayer("Players"));
			if (targets.Length == 0 && players.Length == 0)
				Instantiate(decorationsArr[chosenDecoration], placePoint, Quaternion.identity);
		}
	}
}
