using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int health=100;
	public int currentHealth;
	public int armor=0;
	public double expierence = 0;
	public double nextLevel = 10;

	public GameManager gm;

	//current stats
	public int strength=10;
	public int magic=5;
	public int critical=1;
	public int speed;
	public int attackSpeed;
	
	public HealthBar hb;
	public SkillTree st;

	// Strength/Health skills
	public bool regenerateH = false;
	public bool knockBack=false;


	//Crit skills
	public bool stun = false;
	public float stunDuration = 1f;
	public float stunDamage = 1.2f;
	public bool doubleStrike = false;
	public float doubleStrikeDamage = 1f;

	//Speed skills
	public float jumpHeight;
	public float sprintSpeed;

	// Audio
	private AudioSource source;
	public AudioClip levelUp;

	// Use this for initialization
	void Start () 
	{
		source = GetComponent<AudioSource>();
		currentHealth = health;
		hb = GameObject.Find ("HealthBG").GetComponent<HealthBar> ();
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (regenerateH && currentHealth<=health-1) 
		{
			currentHealth = currentHealth + 2;
			if (currentHealth>=health) 
			{
				currentHealth= health;
			}
			hb.UpdateHealthBar();
			StartCoroutine("Delay", 3);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag=="Weapon") 
		{
			AI enemy = col.GetComponentInParent<AI>();
			LoseHealth(enemy.strength);
		}
	}

	public void checkExp(int exp)
	{
		expierence += exp;
		if (expierence>=nextLevel) 
		{
			source.PlayOneShot(levelUp);
			expierence-= nextLevel;
			nextLevel = nextLevel * 1.5;
			st.skillPoints++;
		}
	}

	IEnumerator Delay(float delay) 
	{
		regenerateH = false;
		yield return new WaitForSeconds(delay);
		regenerateH = true;
	}

	public void LoseHealth(int attack)
	{
		int damage = Random.Range (attack / 2, attack);
		damage -= Mathf.RoundToInt(damage * armor * .01f);
		currentHealth -= damage;
		if (currentHealth <=0) 
		{
			gm.TriggerGameOver();
		}
	}
}
