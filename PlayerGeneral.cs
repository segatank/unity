using UnityEngine;
using UnityEngine.UI;	//for text fields on canvas
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}
	/*Призначення:
	 * - переміщення гравця по карті
	 * - блокування його виходу за межі карти,
	 * - задає здоров*я та шкоду від колізії з ворогом,
	 * - виклик скриптів, що відповідають за стрільбу (лкм та пкм)
	 */
public class PlayerGeneral : MonoBehaviour {
	public Boundary boundary;
	//player stats
	public Vector2 speed = new Vector2(10, 10);
	private Vector2 movement;
	public float startingHealth = 10.0f;
	public float currentHealth;

	[HideInInspector]
	public bool facingRight = true;
	//collision dmg
	public float dmgRate;	
	private float nextDmg;
	//animation and sound
	private Animator animator;
	public AudioClip getHit1;
	public AudioClip getHit2;
	public AudioClip getPickUp1;
	public AudioClip getPickUp2;
	public AudioClip death;
	//players status 
	private bool isPoisoned = false;
	private int poisonTime = 0;
	private float poisonDuration = 0;
	private int poisonDmg = 0;

	void Awake ()
	{
		currentHealth = startingHealth;
	}

	void Start()
	{
		animator = GetComponent<Animator> ();
	}
	
	void Update()
	{
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		
		//переміщення по карті
		movement = new Vector2 (speed.x * inputX, speed.y * inputY);

		if(inputX > 0 && !facingRight)
			Flip();
		else if(inputX < 0 && facingRight)
			Flip();
		
		if (currentHealth < startingHealth)//health regen
			currentHealth += 1.0f * Time.deltaTime;
	}
	//гравець ходить по карті + не може покинути її межі
	void FixedUpdate()
	{
		rigidbody2D.velocity = movement;
		//animator.SetTrigger ("heroWalk");
		rigidbody2D.position = new Vector2
		(
			Mathf.Clamp (rigidbody2D.position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (rigidbody2D.position.y, boundary.yMin, boundary.yMax)
		);
	}

	void OnTriggerEnter2D (Collider2D enemyShot)
	{
		ClearThings shot = enemyShot.gameObject.GetComponent<ClearThings>();
		if (shot != null)
		{
			if (shot.isEnemyShot == true)
			{
				TakeRangerDamage (shot.damage);
				Destroy(shot.gameObject);
			}
		}
	}
	
	public void TakeMeleeDamage (int dmgNum)
	{
		currentHealth -= dmgNum;
		if (currentHealth <= 0) {
			SoundManager.instance.PlaySingle(death);
			SoundManager.instance.musicSource.Stop();
			Destroy (gameObject, 0.1f);
		}
	}

	public void TakeRangerDamage(int dmgNum){
		currentHealth -= dmgNum;
		if (currentHealth <= 0) {
			SoundManager.instance.PlaySingle(death);
			SoundManager.instance.musicSource.Stop();
			Destroy (gameObject, 0.1f);
		}
	}
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	//ITEMS
	//1 - аптечка, яка відновлює хп
	public void PickUpAidKit (int restoreAmount)
	{
		currentHealth += restoreAmount;
		if (currentHealth > startingHealth) {	//щоб хп не було більше за стартове
			currentHealth = startingHealth;
			SoundManager.instance.RandomizeSfx(getPickUp1,getPickUp2);
		}
	}

	public void PickUpPoison (int dot, int dotTime)
	{
		isPoisoned = true;
		poisonDmg = dot;
		poisonTime = dotTime;
	}
}
