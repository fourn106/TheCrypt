using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
{
	public int coffeeCupsTotal;
	public int coffeeCupsCollected = 0;
	public Text coffeeTotalText;
	public Text coffeeCollectedText;

	// Use this for initialization
	void Start () 
	{
		CountTotalCupsAvailable();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void CountTotalCupsAvailable()
	{
		coffeeCupsTotal = GameObject.FindGameObjectsWithTag("Collectible").Length;
		coffeeTotalText.text = coffeeCupsTotal.ToString();
	}

	public void CupCollected()
	{
		coffeeCupsCollected = coffeeCupsCollected +1;
		coffeeCollectedText.text = coffeeCupsCollected.ToString();
	}
}
