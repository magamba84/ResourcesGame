using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
	Gold,
	Lumber,
	Ore,
	Axes
}

[Serializable]
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

[Serializable]
public class ResourceTransform
{
	public List<Resource> from;
	public List<Resource> to;
}