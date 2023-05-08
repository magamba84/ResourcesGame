using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingWindowFactory : BuildingWindowBase
{
	[SerializeField]
	private ResourceIndicator resource;

	[SerializeField]
	private ResourceIndicator inputResource1;

	[SerializeField]
	private ResourceIndicator inputResource2;

	[SerializeField]
	private Button buttonLaunch;
	[SerializeField]
	private Button buttonStop;

	private ResourceTransformer resourceTransformer;
	private TimerActivator timerActivator;
	private List<ResourceTransform> options;

	private List<ResourceType> possibleInputs;

	private List<ResourceIndicator> indicators;
	private List<ResourceType> selectedInputs = new List<ResourceType>();

	public override void Init(BuildingController building)
	{
		indicators = new List<ResourceIndicator> { inputResource1, inputResource2 };

		resourceTransformer = building.gameObject.GetComponent<ResourceTransformer>();
		timerActivator = building.gameObject.GetComponent<TimerActivator>();
		options = ResourceSettings.Instance.CraftTransforms;

		possibleInputs = new List<ResourceType>();
		foreach (var n in options)
		{
			foreach (var r in n.from)
			{
				if (!possibleInputs.Contains(r.type))
					possibleInputs.Add(r.type);
			}
		}

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
			if (currentOption == null)
			{
				SetRecipe(ResourceType.lumber, ResourceType.lumber);
			}
			else
			{
				SetRecipeFull(currentOption);
			}
		}
	}

	private void SetRecipe(ResourceType r1, ResourceType r2)
	{
		selectedInputs = new List<ResourceType> { r1, r2 };

		inputResource1.SetResource(r1);
		inputResource2.SetResource(r2);
		var recipe = FindRecipe(r1, r2);
		SetRecipeOutput(recipe);
	}

	private ResourceTransform FindRecipe(ResourceType r1, ResourceType r2)
	{
		var list = new List<ResourceType> { r1, r2 };
		foreach (var recipe in options)
		{
			var first = recipe.from.Find(x => x.type == r1);
			if (first != null)
			{
				var second = recipe.from.Find(x => x != first && x.type == r2);
				if (second != null)
					return recipe;
			}

		}
		return null;
	}


	private void SetRecipeFull(ResourceTransform recipe)
	{
		selectedInputs = new List<ResourceType> { recipe.from[0].type, recipe.from[1].type };

		inputResource1.SetResource(selectedInputs[0]);
		inputResource2.SetResource(selectedInputs[1]);
		SetRecipeOutput(recipe);
	}

	private void SetRecipeOutput(ResourceTransform recipe)
	{
		resource.gameObject.SetActive(recipe != null);
		if (recipe != null)
			resource.SetResource(recipe.to[0].type);

		resourceTransformer.SetResourceTransform(recipe);
	}



	public void NextOption(int ind)
	{
		var index = possibleInputs.IndexOf(selectedInputs[ind]);
		index++;
		if (index >= possibleInputs.Count)
			index = 0;

		selectedInputs[ind] = possibleInputs[index];
		indicators[ind].SetResource(possibleInputs[index]);

		SetRecipe(selectedInputs[0], selectedInputs[1]);
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
