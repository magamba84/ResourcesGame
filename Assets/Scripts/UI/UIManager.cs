using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private GameObject mineWindow;

	[SerializeField]
	private GameObject factoryWindow;

	[SerializeField]
	private GameObject marketWindow;

	[SerializeField]
	private GameObject startGameWindow;

	[SerializeField]
	private GameObject victoryWindow;

	[SerializeField]
	private Transform windowsParent;

	[SerializeField]
	private ResourcePanel resourcePanel;

	private UIWindowBase currentWindow;

	private static UIManager instance;
	public static UIManager Instance => instance;
	

	private void Awake()
	{
		instance = this;
	}

	public void ShowBuildingWindow(BuildingController building)
	{
		switch (building.GetType())
		{
			case BuildingType.Mining:
				{
					ShowWindow(mineWindow);
					break;
				}
			case BuildingType.Producing:
				{
					ShowWindow(factoryWindow);
					break;
				}
			case BuildingType.Trading:
				{
					ShowWindow(marketWindow);
					break;
				}
		}

		(currentWindow as BuildingWindowBase).Init(building);
		currentWindow.OnClose -= WindowClosed;
		currentWindow.OnClose += WindowClosed;
	}

	private void WindowClosed(UIWindowBase window)
	{
		currentWindow.OnClose -= WindowClosed;
		currentWindow = null;
	}

	public void ShowStartGameWindow(ResourceGameManager manager)
	{
		ShowWindow(startGameWindow);
		(currentWindow as StartGameWindow).Init(manager);
	}

	public void ShowVictoryWindow(ResourceGameManager manager)
	{
		ShowWindow(victoryWindow);
		(currentWindow as VictoryWindow).Init(manager);
	}

	private void ShowWindow(GameObject WindowPrefab)
	{
		if (currentWindow != null)
			currentWindow.Close();

		currentWindow = Instantiate(WindowPrefab, windowsParent).GetComponent<UIWindowBase>();
		currentWindow.OnClose -= WindowClosed;
		currentWindow.OnClose += WindowClosed;
	}

	public void Reset()
	{
		resourcePanel.Reset();
	}

}