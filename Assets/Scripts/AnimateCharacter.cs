using UnityEngine;
using System.Collections;

public class AnimateCharacter : MonoBehaviour 
{
	public GameObject jumpSprite;
	public GameObject[] moving;
	public GameObject[] idle;
	public GameObject[] attack;

	CharacterMovement baseMovement;

	public int curSpriteCount;
	private int totalSpriteCount;
	private bool isMovingLeft;
	private bool hasChangedDirection;
	public float animationInterval = .1f;

	private GameObject previousSprite;
	private GameObject currentSprite;
	private float originalAnimationInterval;
	private float curAnimationInterval;

	// Use this for initialization
	void Start () 
	{
		originalAnimationInterval = animationInterval;
		baseMovement = gameObject.GetComponent<CharacterMovement>();//Grab the character movement script so we can check variables to update our sprites
		currentSprite = idle[0];
		UpdateSprite();
	}
	
	void UpdateDirection()
	{
		//Reset movement to first sprite if not moving the same direction they were OR if we are out of sprites.
		if (baseMovement.isAttacking) 
		{
			totalSpriteCount = attack.Length;
		}
		else if(baseMovement.isMoving)
		{
			totalSpriteCount = moving.Length;
		}
		else if(!baseMovement.isMoving)
		{
			totalSpriteCount = idle.Length;
		}
	}

	void UpdateSprite()
	{
		UpdateAnimationCounter();//Update counter for animating

		//Turn off old sprite
		currentSprite.SetActive(false);
		//Turn on new sprite
		if(baseMovement.isMoving)//If we are facing right and moving
		{
			currentSprite = moving[curSpriteCount];
			if(baseMovement.isJumping)
			{
				currentSprite = jumpSprite;
			}
		}
		else if(baseMovement.isJumping)
		{
			currentSprite = jumpSprite;
		}
		else if (baseMovement.isAttacking) 
		{
			currentSprite = attack[curSpriteCount];
		}
		else
		{
			currentSprite = idle[curSpriteCount];
		}

		previousSprite.SetActive(false);
		currentSprite.SetActive(true);
		curSpriteCount++;
	}

	void UpdateAnimationCounter()
	{
		previousSprite = currentSprite;

		if(curSpriteCount >= totalSpriteCount)
		{
			curSpriteCount = 0;
		}

		if (baseMovement.isAttacking) 
		{
			curAnimationInterval = animationInterval/5f;
		}
		else if(baseMovement.isSprinting)
		{
			curAnimationInterval = animationInterval/1.2f;
		}
		else
		{
			curAnimationInterval = originalAnimationInterval/1.1f;
		}
		StartCoroutine("AnimateWait", curAnimationInterval);
	}

	IEnumerator AnimateWait (float waitTime)
	{
		yield return new WaitForSeconds(curAnimationInterval);
		//animateNPC();
		UpdateDirection();
		UpdateSprite();
	}
}
