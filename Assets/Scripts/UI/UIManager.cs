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
	private Transform windowsParent;

	private GameObject currentWindow;

	private static UIManager instance;
	public static UIManager Instance
	{
		get { return instance; }
	}

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
					currentWindow = Instantiate(mineWindow, windowsParent);
					break;
				}
			case BuildingType.Producing:
				{
					currentWindow = Instantiate(factoryWindow, windowsParent);
					break;
				}
			case BuildingType.Trading:
				{
					currentWindow = Instantiate(marketWindow, windowsParent);
					break;
				}
		}
		Debug.Log(currentWindow.GetComponent<BuildingWindowBase>() + " !!!");
		currentWindow.GetComponent<BuildingWindowBase>().Init(building);
	}

}