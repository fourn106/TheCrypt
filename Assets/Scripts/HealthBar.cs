using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Image healthBar;
	public Image healthBG;

	//public Health h;

	public float maxHealth;
	public float currentHealth;

	public float percentHealth=0f;

	public float width;
	public float newX;

	public Text tHealth;
	public Stats st;

	// Use this for initialization
	void Start () 
	{
		percentHealth = currentHealth / maxHealth;
		width = healthBG.rectTransform.rect.width;
		tHealth.text = currentHealth.ToString () + "/" + maxHealth.ToString ();
		st = GameObject.FindGameObjectWithTag ("Hitbox").GetComponent<Stats> ();
		maxHealth = st.health;
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateHealthBar ();
	}

	public void UpdateHealthBar()
	{
		maxHealth = st.health;
		currentHealth = st.currentHealth;
		percentHealth = currentHealth / maxHealth;
		percentHealth = 1f - percentHealth;
		newX = width * percentHealth;
		healthBar.rectTransform.anchoredPosition = new Vector2 (-newX, 0);
		tHealth.text = currentHealth.ToString () + "/" + maxHealth.ToString ();
	}
}
