using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourceSettings : ScriptableObject
{
	[SerializeField] 
	private List<ResourceFullInfo> resources;

	private static ResourceSettings instance;

	public static ResourceSettings Instance
	{
		get
		{
			if (instance == null)
			{
				var resourceName = typeof(ResourceSettings).Name;
				if (!resourceName.EndsWith("Data"))
					resourceName += "Data";
				instance = Resources.Load<ResourceSettings>(resourceName);
			}

			return instance;
		}
	}

	public ResourceFullInfo GetResourceInfo(ResourceType type)
	{
		var r = resources.Find(x => x.type == type);
		return r;
	}
}
