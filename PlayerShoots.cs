using UnityEngine;
using System.Collections;

public class PlayerShoots : MonoBehaviour {
	/*Призначення:
	 * - дозволяє гравцеві вести вогонь по ворогах
	 */
	[HideInInspector]
	public bool facingRight = true;

	private float angle;
	public GameObject shot;

	public float fireRate;	
	private float nextFire;

	private bool gameOver = false;	//to cancel shooting after G.O.
	private Animator animator;
	public AudioClip shoot1;
	public AudioClip shoot2;

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	void Update () {
		if (!gameOver) {
			if (Input.GetKey (KeyCode.L) && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (shot, transform.position, Quaternion.identity);
				animator.SetTrigger ("heroAttack");
				SoundManager.instance.RandomizeSfx(shoot1,shoot2);
			} else if (Input.GetKey (KeyCode.I) && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (shot, transform.position, Quaternion.identity);
				animator.SetTrigger ("heroAttack");
				SoundManager.instance.RandomizeSfx(shoot1,shoot2);
			} else if (Input.GetKey (KeyCode.J) && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (shot, transform.position, Quaternion.identity);
				animator.SetTrigger ("heroAttack");
				SoundManager.instance.RandomizeSfx(shoot1,shoot2);
			} else if (Input.GetKey (KeyCode.K) && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (shot, transform.position, Quaternion.identity);
				animator.SetTrigger ("heroAttack");
				SoundManager.instance.RandomizeSfx(shoot1,shoot2);
			}
		}
	}
}
