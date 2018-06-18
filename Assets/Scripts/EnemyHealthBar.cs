using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour {
	
	public Image healthBar;
	public Image healthBG;
	
	//public Health h;
	
	public float maxHealth;
	public float currentHealth;
	
	public float percentHealth=0f;
	
	public float width;
	public float newX;
	
	public Text tHealth;
	public AI ai=null;
	
	// Use this for initialization
	void Start () 
	{
		width = healthBG.rectTransform.rect.width;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ai!=null) 
		{
			UpdateHealthBar ();
		}
		else 
		{
			this.gameObject.SetActive (false);	
		}
	}
	
	public void UpdateHealthBar()
	{
		maxHealth = ai.health;
		currentHealth = ai.currentHealth;
		percentHealth = currentHealth / maxHealth;
		percentHealth = 1f - percentHealth;
		newX = width * percentHealth;
		healthBar.rectTransform.anchoredPosition = new Vector2 (-newX, 0);
		//tHealth.text = currentHealth.ToString () + "/" + maxHealth.ToString ();
	}
}
