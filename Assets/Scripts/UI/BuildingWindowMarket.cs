using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingWindowMarket : BuildingWindowBase
{
	[SerializeField]
	private ResourceIndicator resource;

	[SerializeField]
	private ResourceIndicator price;

	[SerializeField]
	private Button buttonSell;

	private ResourceTransformer resourceTransformer;

	private List<ResourceTransform> options;
	private ResourceTransform currentOption;

	public override void Init(BuildingController building)
	{
		resourceTransformer = building.gameObject.GetComponent<ResourceTransformer>();

		options = ResourceSettings.Instance.MarketTransforms;

		resource.gameObject.SetActive(resourceTransformer != null);

		if (resourceTransformer != null)
		{
			var currentOption = resourceTransformer.ResourceTransform;
			if (options.Count == 0)
			{
				Debug.LogError("empty resource options!");
				return;
			}

			if (currentOption == null)
				SetOption(options[0]);
			else
				SetOption(currentOption);

		}
	}

	private void SetOption(ResourceTransform option)
	{
		currentOption = option;
		if (currentOption.from.Count == 0)
		{
			Debug.LogError("no output resources producing!");
			resource.gameObject.SetActive(false);
			return;
		}

		resourceTransformer.SetResourceTransform(currentOption);
		resource.SetResource(currentOption.from[0].type);
		price.SetResource(currentOption.to[0]);
	}

	public void NextOption()
	{
		var index = options.IndexOf(currentOption);
		index++;
		if (index >= options.Count)
			index = 0;

		SetOption(options[index]);
	}

	public void Sell()
	{
		resourceTransformer.DoWork();
	}

	
}
