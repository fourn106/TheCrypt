using UnityEngine;
using System.Collections;

public class AnimateNPC : MonoBehaviour 
{
	public GameObject[] movingLeft;
	public GameObject[] movingRight;
	public GameObject[] attackLeft;
	public GameObject[] attackRight;
	
	AI baseMovement;
	
	private int curSpriteCount;
	private int totalSpriteCount;
	private bool isMovingLeft;
	private bool hasChangedDirection;
	public float animationInterval = 1f;
	
	private GameObject previousSprite;
	private GameObject currentSprite;
	private float originalAnimationInterval;
	private float curAnimationInterval;
	
	// Use this for initialization
	void Start () 
	{
		originalAnimationInterval = animationInterval;
		baseMovement = gameObject.GetComponent<AI>();//Grab the character movement script so we can check variables to update our sprites
		currentSprite = movingLeft[0];
		UpdateSprite();
	}
	
	void UpdateDirection()
	{
		//Reset movement to first sprite if not moving the same direction they were OR if we are out of sprites.
		if(baseMovement.isFacingLeft && baseMovement.isMoving)//If I am facing left and moving
		{
			totalSpriteCount = movingLeft.Length;
		}

		else if(!baseMovement.isFacingLeft && baseMovement.isMoving)//If I am facing right and moving
		{
			totalSpriteCount = movingRight.Length;
		}
		else 
		{
			totalSpriteCount = attackRight.Length;
		}
	}
	
	void UpdateSprite()
	{
		UpdateAnimationCounter();//Update counter for animating
		
		//Turn off old sprite
		currentSprite.SetActive(false);
		//Turn on new sprite
		if(baseMovement.isFacingLeft && baseMovement.isMoving)//If we are facing left and moving
		{
			currentSprite = movingLeft[curSpriteCount];//then we are the next sprite in the "moving left" list			}
		}
		else if(!baseMovement.isFacingLeft && baseMovement.isMoving)//If we are facing right and moving
		{
			currentSprite = movingRight[curSpriteCount];
		}
		else if (!baseMovement.isFacingLeft && !baseMovement.isMoving && baseMovement.isAttacking) 
		{
			currentSprite = attackRight[curSpriteCount];
		}
		else if (baseMovement.isFacingLeft && !baseMovement.isMoving && baseMovement.isAttacking) 
		{
			currentSprite = attackLeft[curSpriteCount];
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
		
		//		if(baseMovement.isSprinting)
		//		{
		//			curAnimationInterval = animationInterval/1.2f;
		//		}
		else
		{
			curAnimationInterval = originalAnimationInterval;
		}
		StartCoroutine("AnimateWait", animationInterval);
	}
	
	IEnumerator AnimateWait (float waitTime)
	{
		yield return new WaitForSeconds(curAnimationInterval);
		//animateNPC();
		UpdateDirection();
		UpdateSprite();
	}
}