using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryWindow : UIWindowBase
{
	private ResourceGameManager gameManager;
	public void Init(ResourceGameManager manager)
	{
		gameManager = manager;
	}

	public void ResetGame()
	{
		gameManager.ResetGame();
	}
}
