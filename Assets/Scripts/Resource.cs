using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
	gold,
	lumber,
	ore,
	stone,
	hammers,
	forks,
	drills
}

[Serializable]
public class Resource
{
	public ResourceType type;
	public int count;
}

[Serializable]
public class ResourceFullInfo
{
	public ResourceType type;
	public string name;
	public Sprite icon;
}

[Serializable]
public class ResourceTransform
{
	public List<Resource> from;
	public List<Resource> to;
}