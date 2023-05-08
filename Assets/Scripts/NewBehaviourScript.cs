using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public enum ResourceType
{
	Gold,
	Lumber,
	Ore,
	Axes
}

public class Resource
{
	public ResourceType type;
	public int count;
}

public class ResourceFullInfo
{
	public Resource resource;
	public string name;
	public int count;
	//public Sprite icon;
}

public class ResourceTransform
{
	public List<Resource> from;
	public List<Resource> to;
}

public class ResourceBank
{
	public List<Resource> resources;
	private Dictionary<ResourceType, Resource> resourcesDictionary;

	public event Action<ResourceType, Resource> ResourceUpdated;

	public void Awake()
	{
		resourcesDictionary = new Dictionary<ResourceType, Resource>();
		foreach (var r in resources)
			resourcesDictionary[r.type] = r;
	}

	public bool TransformResource(ResourceTransform transform)
	{
		foreach (var r in transform.from)
		{
			var own = resourcesDictionary[r.type];
			if (own == null || own.count < r.count)
				return false;
		}

		foreach (var r in transform.from)
		{
			var own = resourcesDictionary[r.type];
			own.count -= r.count;

			ResourceUpdated?.Invoke(r.type, own);
		}

		foreach (var r in transform.to)
		{
			if (!resourcesDictionary.ContainsKey(r.type))
				resourcesDictionary[r.type] = new Resource { type = r.type };

			var own = resourcesDictionary[r.type];
			own.count += r.count;

			ResourceUpdated?.Invoke(r.type, own);
		}
		return true;
	}
}