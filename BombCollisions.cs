using UnityEngine;
using System.Collections;

public class BombCollisions : MonoBehaviour {
	
	public bool BombBlast =false;
	public BlastParticles blastParticles;
	public float timerSeconds ;	// Seconds after which the bullets should be fired.
	int seconds;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(BombBlast==true)
		{
			timerSeconds = timerSeconds - Time.deltaTime;
			seconds = (int)timerSeconds%60;
			if(seconds<=0)
			{
				GetComponent<Animator>().SetBool("A_BombBlast",true);
				Destroy(gameObject,1.0f);
			}
	
		}
		else
		{
			BombHolderContact();
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		tag = col.transform.tag;
		CheckCollisionTagName(col);					// Sent the collision object to check the tag
	}

	void CheckCollisionTagName(Collision2D col)
	{
		switch(col.transform.tag)
		{
		
		case "BombHolder_tag" : BombHolderContact();BombBlast=false; 	 break;
		default : BombBlast=true; break;
		}
	}

	void OnCollisionStay2D(Collision2D col)
	{
		CheckCollisionTagName(col);
	}

	void OnCollisionExit2D(Collision2D col)
	{	
		switch(col.transform.tag)
		{
			case "BombHolder_tag" : BombAndHolderNotInContact();BombBlast=true;	 break;
			default : BombAndHolderNotInContact(); break;
		}

	}

	void BombAndHolderNotInContact()
	{
		BombBlast = true;
	}

	void BombHolderContact()
	{
		BombBlast =false;
		timerSeconds = 5.0f;			//Stable state
	}

	void OnDestroy()
	{
		if(this.enabled)
		{
		
			Debug.Log("BIG AND SMALL ");
			if(blastParticles!=null)
			blastParticles.GenerateParticles();
		}
	}
}
