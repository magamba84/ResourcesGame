using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTransformer : MonoBehaviour, IWorkable
{
	private ResourceTransform resourceTransform;

	public ResourceTransform ResourceTransform
	{
		get { return resourceTransform; }
	}

	public void SetResourceTransform(ResourceTransform resourceTransform)
	{
		this.resourceTransform = resourceTransform;
	}

	public void DoWork()
	{
		if (resourceTransform == null)
			return;

		if (ResourceBank.Instance.TransformResource(resourceTransform))
			ShowResult(resourceTransform);
	}

	private void ShowResult(ResourceTransform result)
	{

	}


}
