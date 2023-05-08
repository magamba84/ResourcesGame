using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingWindowMine : BuildingWindowBase
{
	[SerializeField]
	private  ResourceIndicator resource;

	[SerializeField]
	private Button buttonLaunch;
	[SerializeField]
	private Button buttonStop;

	private ResourceTransformer resourceTransformer;
	private TimerActivator timerActivator;
	private List<ResourceTransform> options;
	private ResourceTransform currentOption;

	public override void Init(BuildingController building)
	{
		resourceTransformer = building.gameObject.GetComponent<ResourceTransformer>();
		timerActivator = building.gameObject.GetComponent<TimerActivator>();
		options = ResourceSettings.Instance.MiningTransforms;

		if (timerActivator != null)
		{
			buttonLaunch.gameObject.SetActive(!timerActivator.IsWorking);
			buttonStop.gameObject.SetActive(timerActivator.IsWorking);
		}
		else
		{
			buttonLaunch.gameObject.SetActive(false);
			buttonStop.gameObject.SetActive(false);
		}
		

		resource.gameObject.SetActive(resourceTransformer != null);

		if (resourceTransformer != null)
		{
			var currentOption = resourceTransformer.ResourceTransform;
			if (options.Count == 0)
			{
				Debug.LogError("empty resource options!");
				return;
			}
			if(currentOption==null)
				SetOption(options[0]);
			else
				resource.SetResource(currentOption.to[0].type);

		}
	}

	private void SetOption(ResourceTransform option) 
	{
		currentOption = option;
		if (currentOption.to.Count == 0)
		{
			Debug.LogError("no output resources producing!");
			resource.gameObject.SetActive(false);
			return;
		}

		resourceTransformer.SetResourceTransform(currentOption);
		resource.SetResource(currentOption.to[0].type);
	}

	public void NextOption() 
	{
		var index = options.IndexOf(currentOption);
		index++;
		if (index >= options.Count)
			index = 0;

		SetOption(options[index]);
	}

	public void Launch() 
	{
		timerActivator.StartWork();
		buttonLaunch.gameObject.SetActive(false);
		buttonStop.gameObject.SetActive(true);
	}

	public void Stop()
	{
		timerActivator.StopWork();
		buttonLaunch.gameObject.SetActive(true);
		buttonStop.gameObject.SetActive(false);
	}
}
