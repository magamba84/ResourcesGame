using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBank
{
	private List<Resource> resources = new List<Resource>();
	public List<Resource> Resources
	{
		get { return resources; }
		set
		{
			resources = value; 
			resourcesDictionary = new Dictionary<ResourceType, Resource>();

			if (resources == null)
				resources = new List<Resource>();

			foreach (var r in resources)
				resourcesDictionary[r.type] = r;

			foreach (var r in resources)
			{
				ResourceUpdated(r.type, r);
			}
		}
	}

	private Dictionary<ResourceType, Resource> resourcesDictionary;

	private static ResourceBank instance;
	public static ResourceBank Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new ResourceBank();
			}
			return instance;
		}
	}

	public event Action<ResourceType, Resource> ResourceUpdated;

	public ResourceBank()
	{
		resourcesDictionary = new Dictionary<ResourceType, Resource>();
		foreach (var r in resources)
			resourcesDictionary[r.type] = r;
	}

	public bool TransformResource(ResourceTransform transform)
	{
		foreach (var r in transform.from)
		{
			if (!resourcesDictionary.ContainsKey(r.type))
				return false;

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
			{
				resourcesDictionary[r.type] = new Resource { type = r.type };
				resources.Add(resourcesDictionary[r.type]);
			}

			var own = resourcesDictionary[r.type];
			own.count += r.count;

			ResourceUpdated?.Invoke(r.type, own);
		}
		return true;
	}
}