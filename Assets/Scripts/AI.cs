using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	public float originalX;	
	public float speed = .01f;
	public float useSpeed;
	public float gravity = 20f;
	public float distanceRight = 10f;
	public float distanceLeft = 10f;

	public bool isFacingLeft = false;
	
	public bool isMoving = false;
	public bool isAttacking = false;
	public double previousX;
	public double currentX;

	public GameObject sword;
	public GameObject[] loot;
	public int health = 30;
	public int currentHealth;
	public int strength = 10;
	public int armor = 1;
	public int experience = 10;
	public bool stunned = false;

	public Stats st;
	public GameObject ehb;
	public EnemyHealthBar ehbScript;
	public CharacterMovement cm;
	public GameManager gm;

	public AudioSource source;
	public AudioClip enemyHit;

	// Use this for initialization
	void Start () 
	{
		currentHealth = health;
		originalX = transform.position.x;
		transform.Translate (Vector3.left * speed);
		cm = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMovement>();
		st = GameObject.FindGameObjectWithTag ("Hitbox").GetComponent<Stats> ();
		source = GameObject.Find("Capsule").GetComponent<AudioSource>();
		ehb = GameObject.Find ("EnemyHealthBar");
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		ehbScript = ehb.GetComponent<EnemyHealthBar> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		previousX = System.Math.Round (this.gameObject.transform.position.x, 2);
		if (!stunned&&!isAttacking) 
		{
			if(isMoving)
			{
				if(originalX - transform.position.x > distanceLeft)
				{
					useSpeed = speed; //flip direction
					isFacingLeft = false;
					sword.transform.rotation = Quaternion.Euler (0, 180, 0);
				}
				else if(originalX - transform.position.x < -distanceRight)
				{
					useSpeed = -speed; //flip direction
					isFacingLeft = true;
					sword.transform.rotation = Quaternion.Euler (0, 0, 0);

				}
				//Debug.Log (origX + " - " + transform.position.x + " = " + (origX - transform.position.x));
				transform.Translate (Vector3.right * useSpeed);
			}
			else 
			{
				transform.Translate (Vector3.left * speed);	
			}
			
			currentX = System.Math.Round(this.gameObject.transform.position.x, 2);
			//Check to see if we are currently moving... this is needed for animation purposes...
			if(previousX != currentX)
			{
				isMoving = true;
			}
			else if(previousX == currentX)
			{
				isMoving = false;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag=="Player")
		{
			StartCoroutine(Attack(1f));
		}
		else if (col.tag=="Fireball") 
		{
			source.PlayOneShot(enemyHit);
			ehbScript.ai = GetComponent<AI>();
			ehb.SetActive(true);
			LoseHealthMagic(st.magic);
			Destroy(col.gameObject);
		}
		else if (col.tag=="Lightning") 
		{
			source.PlayOneShot(enemyHit);
			ehbScript.ai = GetComponent<AI>();
			ehb.SetActive(true);
			LoseHealthMagic(st.magic*2);
			col.GetComponent<Lightning>().chainLightning();
		}
		else if (col.tag=="Sword") 
		{
			source.PlayOneShot(enemyHit);
			ehbScript.ai = GetComponent<AI>();
			ehb.SetActive(true);
			if (st.stun) 
			{
				int chance = Random.Range(0,100);
				if (st.critical>=chance) 
				{
					StartCoroutine(Stunned(st.stunDuration));
				}
			}
			LoseHealthStrength(st.strength);
			if (st.knockBack) 
			{
				if (cm.isFacingLeft) 
				{
					transform.Translate (Vector3.left * 1f);
				}
				else 
				{
					transform.Translate (Vector3.right * 1f);
				}
			}
		}
	}

	public void LoseHealthStrength(int attack)
	{
		int damage = Random.Range (attack / 2, attack);
		damage -= Mathf.RoundToInt(damage * armor * .01f);
		if (stunned) 
		{
			damage = Mathf.RoundToInt(damage*st.stunDamage);
		}
		if (st.doubleStrike) 
		{
			int chance = Random.Range(0,100);
			if (st.critical>=chance) 
			{
				damage += Mathf.RoundToInt(damage*st.doubleStrikeDamage);
			}
		}
		currentHealth -= damage;
		if (currentHealth <=0) 
		{
			Instantiate (loot[Random.Range(0,loot.Length)], this.transform.position, transform.rotation);
			st.checkExp(experience);
			ehb.SetActive(false);
			ehbScript.ai=null;
			if (this.gameObject.tag=="Boss") 
			{
				gm.WinLevel();
			}
			Destroy(this.gameObject);
		}
	}

	public void LoseHealthMagic(int magic)
	{
		currentHealth -= Random.Range (magic / 2, magic);
		if (currentHealth <=0) 
		{
			Instantiate (loot[Random.Range(0,loot.Length)], this.transform.position, transform.rotation);
			st.checkExp(experience);
			ehb.SetActive(false);
			ehbScript.ai=null;
			if (this.gameObject.tag=="Boss") 
			{
				gm.WinLevel();
			}
			Destroy(this.gameObject);
		}
	}

	IEnumerator Stunned(float stunTime)
	{
		stunned = true;
		yield return new WaitForSeconds(stunTime);
		stunned = false;
	}

	IEnumerator Attack(float seconds)
	{
		sword.SetActive (true);
		isAttacking = true;
		yield return new WaitForSeconds(seconds);
		sword.SetActive (false);
		isAttacking = false;
	}
}
