using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameWindow : UIWindowBase
{
    [SerializeField]
    private ResourceGameManager gameManager;

    [SerializeField]
    private GameObject toggleGroup;

   public void Init(ResourceGameManager manager)
	{
        gameManager = manager;

        toggleGroup.SetActive(true);
    }

	public void StartGame() 
    {
        gameManager.StartGame();
        Close();
    }

    public void SetToggle(int num)
    {
        gameManager.SetResourcesBuildingsCount(num);
    }
}
