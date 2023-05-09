using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[SerializeField]
public class GameData
{
	public int resourcesBuildingsCount;
	public List<Resource> resources;
}

public class ResourceGameManager : MonoBehaviour
{
	[SerializeField]
	private List<BuildingController> buildings;

	[SerializeField]
	private UIManager uiManager;

	private bool gameComplete = false;

	private GameData gameData = new GameData { resourcesBuildingsCount = 3 };

	private void Awake()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		
	}

	private void Start()
	{
		ResourceBank.Instance.ResourceUpdated -= OnResourceUpdated;
		ResourceBank.Instance.ResourceUpdated += OnResourceUpdated;

		foreach (var b in buildings)
		{
			b.gameObject.SetActive(false);
		}

		if (!Load())
		{
			uiManager.ShowStartGameWindow(this);
		}
		else
		{
			StartGame();
		}
		
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
		gameData.resourcesBuildingsCount = count;
	}

	public void StartGame()
	{
		int minesInited = 0;
		foreach (var b in buildings)
		{
			if (b.GetType() == BuildingType.Mining)
			{
				if (minesInited < gameData.resourcesBuildingsCount)
					minesInited++;
				else
					continue;
			}
			b.gameObject.SetActive(true);
		}

		StartCoroutine(SaveCoroutine());
	}

	private IEnumerator SaveCoroutine() 
	{
		do
		{
			yield return new WaitForSeconds(2f);
			Save();
		}
		while (true);
		
	}

	private void Save() 
	{
		gameData.resources = ResourceBank.Instance.Resources;
		var path = Path.Combine(Application.persistentDataPath,"save.txt");
		var data = JsonConvert.SerializeObject(gameData);//ResourceBank.Instance.GetSaveData();

		using (StreamWriter writer = new StreamWriter(path, false))
		{
			writer.WriteLine(data);
			writer.Flush();
		}
	}

	private bool Load()
	{
		UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get("file://"+ Application.persistentDataPath + "/save.txt");
		www.SendWebRequest();
		while (!www.isDone)
		{
		}
		string s = www.downloadHandler.text;
		if (string.IsNullOrEmpty(s))
		{
			return false;
		}
		else
		{
			gameData = JsonConvert.DeserializeObject<GameData>(s);
			ResourceBank.Instance.Resources = gameData.resources;
			return true;
		}
	}
}
