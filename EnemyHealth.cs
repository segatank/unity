using UnityEngine;
using System.Collections;
using System;

public class EnemyHealth : MonoBehaviour {
	/*Призначення:
	 - все про все про моба: його дамаг, хп, пойнти, дроп айтемів*/
	public int startingHp = 1;	//hp
	public int currentHp;
	public bool isEnemy = true;
	public int collDmg = 5;
	public float dmgRate;	
	private float nextDmg = 2f;

	private Animator animator;
	private GameObject player;
	//for score points
	public int mobPopulation = 1;
	GameObject mobCounter;
	public int scorePoints = 10;
	GameObject scoreDisplay;
	//for items
	public GameObject[] item;
	public int dropChance = 10;
	private int result;
	private int chosenItem;
	//audio
	public AudioClip enemyAttack1;
	public AudioClip enemyAttack2;
	public AudioClip enemyDies;
	public AudioClip enemyHit1;
	public AudioClip enemyHit2;
	//after death
	public GameObject[] deathPrefabs;
	private int chosenDeathEffect;

	void Start ()
	{
		try{
			currentHp = startingHp;
			animator = GetComponent<Animator> ();
			player = GameObject.FindGameObjectWithTag ("Player");	//знаходить гравця по тегу
			scoreDisplay = GameObject.Find("Score_text");
			mobCounter = GameObject.Find("Wave_info_text");
			result = UnityEngine.Random.Range (0, 100);
			chosenDeathEffect = UnityEngine.Random.Range (0, deathPrefabs.Length);
		}
		catch (Exception e){}
	}

	void Update(){
		try{
			if (player==null)
				animator.SetTrigger ("PlayerIsDead");
		}
		catch (Exception e){}
	}

	public void Damage(int damageCount)
	{
		currentHp -= damageCount;		
		if (currentHp <= 0) {
			ScoreManager scoreMob = scoreDisplay.GetComponent<ScoreManager>();
			scoreMob.AddPoints(scorePoints);

			UpdateMobCounter mbCounter = mobCounter.GetComponent<UpdateMobCounter>();
			mbCounter.AddMonsters(mobPopulation);

			Destroy (gameObject);
			Instantiate (deathPrefabs [chosenDeathEffect], transform.position, Quaternion.identity);
			DropItem();
		}
	}
	
	public void OnTriggerEnter2D(Collider2D otherCollider)
	{
		ClearThings shot = otherCollider.gameObject.GetComponent<ClearThings>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);
				Destroy(shot.gameObject);
			}
		}
	}
	//Наносить гравцеві шкоду, доки той стоїть біля монcтра
	void OnCollisionStay2D(Collision2D otherObject)
	{
		try{
			if (otherObject.gameObject.tag == "Player"){
				PlayerGeneral player = otherObject.gameObject.GetComponent<PlayerGeneral>();
				if (player.currentHealth > 0 && Time.time > nextDmg) {
					nextDmg = Time.time + dmgRate;
					SoundManager.instance.RandomizeSfx(enemyAttack1,enemyAttack2);
					animator.SetTrigger ("Attacks");
					player.TakeMeleeDamage (collDmg);
				}
			}
		}
		catch (Exception e){}
	}

	void DropItem(){
		chosenItem = (int)UnityEngine.Random.Range(0, item.Length );
		if(result>0 && result<=dropChance)
			Instantiate (item[chosenItem], transform.position, Quaternion.identity);
	}

	public void IntoOblivion(){
		Instantiate (deathPrefabs [chosenDeathEffect], transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
