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

	private void Start()
	{
		//ResourceBank.Instance.ResourceUpdated -= OnResourceUpdated;
		//ResourceBank.Instance.ResourceUpdated += OnResourceUpdated;

		
	}

	/*private void OnDestroy()
	{
		ResourceBank.Instance.ResourceUpdated -= OnResourceUpdated;
	}*/

	public void SetResource(Resource resource)
	{
		if (info == null || info.type != resource.type)
		{
			info = ResourceSettings.Instance.GetResourceInfo(resource.type);
			image.sprite = info.icon;
		}

		text.text = resource.count.ToString();
	}
}
