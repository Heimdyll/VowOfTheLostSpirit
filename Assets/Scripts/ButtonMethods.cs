using UnityEngine;
using System.Collections;

public class ButtonMethods : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Jump")) 
		{
			//If I press Spacebar this will run
			LoadInGameLevel();
		}
	}

	public void LoadInGameLevel()
	{
		Application.LoadLevel ("InGame");
	}

	public void LoadCreditsLevel()
	{ 
		Application.LoadLevel ("Credits");
	}

	public void LoadMainMenuLevel()
	{ 
		Application.LoadLevel ("MainMenu");
	}

}
