using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillTree : MonoBehaviour {

	public List<Button> purchasableBtns;
	public Button[] blueButtons;
	public Button[] redButtons;
	public Button[] greenButtons;
	public Button[] purpleButtons;
	public Button activeButton;
	public Button purchaseSkill;

	public Text skillTitle;
	public Text skillText;
	public Text skillPointText;

	public Sprite yellow;

	public int skillPoints = 0;

	public int newStrength;
	public int newMagic;
	public int newSpeed;
	public int newCritical;
	public int newHealth;

	public Text tStrength;
	public Text tMagic;
	public Text tSpeed;
	public Text tCritical;

	public Stats st;
	public CharacterMovement cm;

	public AudioSource source;
	public AudioClip upgrade;

	// Use this for initialization
	void Start () 
	{
		st = GameObject.FindGameObjectWithTag ("Hitbox").GetComponent<Stats> ();
		cm = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMovement> ();
		source = GameObject.Find("Capsule").GetComponent<AudioSource>();

		skillTitle.text = "";
		skillText.text = "";
		//updateSkillPoints();

		newStrength = st.strength;
		newMagic = st.magic;
		newSpeed = st.speed;
		newCritical = st.critical;
		newHealth = st.health;

		purchaseSkill.interactable = false;

		for (int i = 0; i < 2; i++) 
		{
			purchasableBtns.Add(blueButtons[i]);
			purchasableBtns.Add(redButtons[i]);
			purchasableBtns.Add(greenButtons[i]);
			purchasableBtns.Add(purpleButtons[i]);
		}
		updateInteractibles ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void addSkillPoint()
	{
		skillPoints++;
		updateInteractibles ();
	}

	public void purchase()
	{
		if (skillPoints > 0) 
		{
			source.PlayOneShot(upgrade);
			//Red Nodes
			if (activeButton.name=="RedNode 0") 
			{
				purchasableBtns.Add(redButtons[4]);
				purchasableBtns.Add(redButtons[5]);
				st.strength += 5;
			}
			else if (activeButton.name=="RedNode 1") 
			{
				purchasableBtns.Add(redButtons[2]);
				purchasableBtns.Add(redButtons[3]);
				st.health += 20;
				st.currentHealth+=20;
			}
			else if (activeButton.name=="RedNode 2") 
			{
				st.health += 30;
				st.currentHealth+=30;
			}
			else if (activeButton.name=="RedNode 3") 
			{
				st.regenerateH = true;
			}
			else if (activeButton.name=="RedNode 4") 
			{
				st.knockBack=true;
			}
			else if (activeButton.name=="RedNode 5") 
			{
				st.strength+=5;
			}
			//Blues Nodes
			else if (activeButton.name=="BlueNode 0") 
			{
				purchasableBtns.Add(blueButtons[2]);
				purchasableBtns.Add(blueButtons[3]);
				cm.lightningAbility = true;
			}
			else if (activeButton.name=="BlueNode 1") 
			{
				purchasableBtns.Add(blueButtons[4]);
				purchasableBtns.Add(blueButtons[5]);
				cm.bubbleAbility = true;
			}
			else if (activeButton.name=="BlueNode 2") 
			{
				st.magic+=5;
			}
			else if (activeButton.name=="BlueNode 3") 
			{
				cm.arc++;
			}
			else if (activeButton.name=="BlueNode 4") 
			{
				cm.bubbleLifeSpan= cm.bubbleLifeSpan*2;
			}
			else if (activeButton.name=="BlueNode 5") 
			{
				cm.bubbleHits++;
			}
			//Green Nodes
			else if (activeButton.name=="GreenNode 0") 
			{
				purchasableBtns.Add(greenButtons[2]);
				purchasableBtns.Add(greenButtons[3]);
				cm.speed++;
				cm.sprintSpeed++;
			}
			else if (activeButton.name=="GreenNode 1") 
			{
				purchasableBtns.Add(greenButtons[4]);
				purchasableBtns.Add(greenButtons[5]);
				st.attackSpeed +=5;
			}
			else if (activeButton.name=="GreenNode 2") 
			{
				cm.jumpSpeed+=2;
			}
			else if (activeButton.name=="GreenNode 3") 
			{
				cm.sprintSpeed++;
			}
			else if (activeButton.name=="GreenNode 4") 
			{
				
			}
			else if (activeButton.name=="GreenNode 5") 
			{
				
			}
			//Purple Nodes
			else if (activeButton.name=="PurpleNode 0") 
			{
				purchasableBtns.Add(purpleButtons[2]);
				purchasableBtns.Add(purpleButtons[3]);
				st.stun = true;
				st.critical +=5;
			}
			else if (activeButton.name=="PurpleNode 1") 
			{
				purchasableBtns.Add(purpleButtons[4]);
				purchasableBtns.Add(purpleButtons[5]);
				st.doubleStrike = true;
				st.critical +=5;
			}
			else if (activeButton.name=="PurpleNode 2") 
			{
				st.stunDuration = 2f;
				st.critical +=5;
			}
			else if (activeButton.name=="PurpleNode 3") 
			{
				st.stunDamage = 1.5f;
				st.critical +=5;
			}
			else if (activeButton.name=="PurpleNode 4") 
			{
				st.critical +=10;
			}
			else if (activeButton.name=="PurpleNode 5") 
			{
				st.doubleStrikeDamage = 1.5f;
				st.critical +=5;
			}

			purchasableBtns.Remove(activeButton);

			SpriteState sState = new SpriteState();
			sState.disabledSprite = yellow;
			activeButton.spriteState = sState;
			activeButton.interactable = false;

			skillTitle.text = "";
			skillText.text = "";
			purchaseSkill.interactable = false;

			//updateSkillPoints();
			--skillPoints;

			updateInteractibles();
		}

	}

	// Blue/Magic Nodes
	public void blueNode0()
	{
		// Give skill boost/ability
		skillTitle.text = "Lightning";
		skillText.text = "Gives new attack spell, lightning. Lightning ignores armor";
		purchaseSkill.interactable = true;
		activeButton = blueButtons [0];
	}

	public void blueNode1()
	{
		// Give skill boost/ability
		skillTitle.text = "Bubble Shield";
		skillText.text = "Gives a new defensive spell, bubble shield. It blocks one attack";
		purchaseSkill.interactable = true;
		activeButton = blueButtons [1];
	}

	public void blueNode2()
	{
		skillTitle.text = "Lightning Damage";
		skillText.text = "Adds damage to lightning spell";
		purchaseSkill.interactable = true;
		activeButton = blueButtons [2];
	}

	public void blueNode3()
	{
		skillTitle.text = "Lightning Chain";
		skillText.text = "Allows lightning to chain";
		purchaseSkill.interactable = true;
		activeButton = blueButtons [3];
	}

	public void blueNode4()
	{
		skillTitle.text = "Incease Bubble Duration";
		skillText.text = "Increases how long the bubble shield lasts";
		purchaseSkill.interactable = true;
		activeButton = blueButtons [4];
	}

	public void blueNode5()
	{
		skillTitle.text = "Increase Bubble Durability";
		skillText.text = "Increases the damage the bubble shield can take to 2";
		purchaseSkill.interactable = true;
		activeButton = blueButtons [5];
	}

	//Red/Strength Nodes
	public void redNode0()
	{
		skillTitle.text = "Improved Strength";
		skillText.text = "Gives 5 points to strength";
		purchaseSkill.interactable = true;
		activeButton = redButtons [0];
	}

	public void redNode1()
	{
		skillTitle.text = "Improved Health";
		skillText.text = "Gives 20 points to health";
		purchaseSkill.interactable = true;
		activeButton = redButtons [1];
	}

	public void redNode2()
	{
		skillTitle.text = "Improved Health";
		skillText.text = "Gives 30 points to health";
		purchaseSkill.interactable = true;
		activeButton = redButtons [2];
	}

	public void redNode3()
	{
		skillTitle.text = "Regenerating Health";
		skillText.text = "Regenerate 5 health points every 5 seconds";
		purchaseSkill.interactable = true;
		activeButton = redButtons [3];
	}

	public void redNode4()
	{
		skillTitle.text = "Knockback";
		skillText.text = "Knocks your enemies back";
		purchaseSkill.interactable = true;
		activeButton = redButtons [4];
	}

	public void redNode5()
	{
		skillTitle.text = "Improved Strength";
		skillText.text = "Give 5 points more to strength";
		purchaseSkill.interactable = true;
		activeButton = redButtons [5];
	}

	//Green/Speed Nodes
	public void greenNode0()
	{
		skillTitle.text = "Improved Movement Speed";
		skillText.text = "Gives 1 point to movement speed and sprint";
		purchaseSkill.interactable = true;
		activeButton = greenButtons [0];
	}
	
	public void greenNode1()
	{
		skillTitle.text = "Improved Attack Speed";
		skillText.text = "Gives 5 points to atack speed";
		purchaseSkill.interactable = true;
		activeButton = greenButtons [1];
	}
	
	public void greenNode2()
	{
		skillTitle.text = "Improved Jump Height";
		skillText.text = "Can jump higher";
		purchaseSkill.interactable = true;
		activeButton = greenButtons [2];
	}
	
	public void greenNode3()
	{
		skillTitle.text = "Improved Sprint Speed";
		skillText.text = "Can sprint faster";
		purchaseSkill.interactable = true;
		activeButton = greenButtons [3];
	}
	
	public void greenNode4()
	{
		skillTitle.text = "Improved Quick Attack Speed";
		skillText.text = "Speeds up quick attacks";
		purchaseSkill.interactable = true;
		activeButton = greenButtons [4];
	}
	
	public void greenNode5()
	{	
		skillTitle.text = "Improved Strong Attack Speed";
		skillText.text = "Speeds up strong attacks";
		purchaseSkill.interactable = true;
		activeButton = greenButtons [5];
	}

	//Purple/Critical Nodes
	public void purpleNode0()
	{
		skillTitle.text = "Chance to Stun";
		skillText.text = "Gives a chance to stun enemies when hit";
		purchaseSkill.interactable = true;
		activeButton = purpleButtons [0];
	}
	
	public void purpleNode1()
	{
		skillTitle.text = "Chance to Double Strike";
		skillText.text = "Gives a chance to deal a second attack when hitting an enemy";
		purchaseSkill.interactable = true;
		activeButton = purpleButtons [1];
	}
	
	public void purpleNode2()
	{
		skillTitle.text = "Increase Stun Duration";
		skillText.text = "Enemies stay stunned longer";
		purchaseSkill.interactable = true;
		activeButton = purpleButtons [2];
	}
	
	public void purpleNode3()
	{
		skillTitle.text = "Increase Stun Damage";
		skillText.text = "Increase the damage to stunned enemies to 1.5x";
		purchaseSkill.interactable = true;
		activeButton = purpleButtons [3];
	}
	
	public void purpleNode4()
	{
		skillTitle.text = "Increase Double Strike Chance";
		skillText.text = "Greatly increases double strike chance";
		purchaseSkill.interactable = true;
		activeButton = purpleButtons [4];
	}
	
	public void purpleNode5()
	{
		skillTitle.text = "Increase Double Strike Damage";
		skillText.text = "Increases the double strike damage multiplier to 1.5x";
		purchaseSkill.interactable = true;
		activeButton = purpleButtons [5];
	}

	public void updateInteractibles()
	{
		if (skillPoints>0) 
		{
			foreach (Button btn in purchasableBtns) 
			{
				btn.interactable = true;
			}
		}
		else 
		{
			foreach (Button btn in purchasableBtns) 
			{
				btn.interactable = false;
			}
		}
		skillPointText.text = skillPoints.ToString ();
	}

//	public void updateSkillPoints()
//	{
//		tStrength.text = "Strength: " + st.strength.ToString ();
//		tSpeed.text = "Speed: " + st.speed.ToString ();
//		tMagic.text = "Magic: " + st.magic.ToString ();
//		tCritical.text = "Critical: " + st.critical.ToString ();
//	}
}
