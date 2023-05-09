using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGameManager : MonoBehaviour
{
	[SerializeField]
	private List<BuildingController> buildings;

	[SerializeField]
	private UIManager uiManager;

	private int resourcesBuildingsCount = 3;

	private bool gameComplete = false;

	private void Awake()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		foreach (var b in buildings)
		{
			b.gameObject.SetActive(false);
		}
	}

	private void Start()
	{
		uiManager.ShowStartGameWindow(this);

		ResourceBank.Instance.ResourceUpdated -= OnResourceUpdated;
		ResourceBank.Instance.ResourceUpdated += OnResourceUpdated;
	}

	private void OnDestroy()
	{
		ResourceBank.Instance.ResourceUpdated -= OnResourceUpdated;
	}

	private void OnResourceUpdated(ResourceType type, Resource resource)
	{
		if (!gameComplete && type == ResourceType.gold && resource.count >= ResourceSettings.Instance.GoldToWin)
		{
			FinishGame();
		}
	}

	private void FinishGame() 
	{
		gameComplete = true;
		uiManager.ShowVictoryWindow();
	}

	public void SetResourcesBuildingsCount(int count)
	{
		resourcesBuildingsCount = count;
	}

	public void StartGame()
	{
		int minesInited = 0;
		foreach (var b in buildings)
		{
			if (b.GetType() == BuildingType.Mining)
			{
				if (minesInited < resourcesBuildingsCount)
					minesInited++;
				else
					continue;
			}
			b.gameObject.SetActive(true);
		}
	}
}
