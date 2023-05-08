using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceIndicator : MonoBehaviour
{
	[SerializeField]
	private Image image;
	[SerializeField]
	private Text text;
	[SerializeField]
	private Resource resource;

	private ResourceFullInfo info;

	public void SetResource(ResourceType resource)
	{
		if (info == null || info.type != resource)
		{
			info = ResourceSettings.Instance.GetResourceInfo(resource);
			image.sprite = info.icon;
		}
	}

	public void SetResource(Resource resource)
	{
		SetResource(resource.type);
		text.text = resource.count.ToString();
	}
}
