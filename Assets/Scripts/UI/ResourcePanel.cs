using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] private GameObject resourceIndicatorInstance;

    private Dictionary<ResourceType, ResourceIndicator> resourceIcons = new Dictionary<ResourceType, ResourceIndicator>();

    void Start()
    {
        ResourceBank.Instance.ResourceUpdated -= OnResourceUpdated;
        ResourceBank.Instance.ResourceUpdated += OnResourceUpdated;
    }

    private void OnDestroy()
	{
		ResourceBank.Instance.ResourceUpdated -= OnResourceUpdated;
	}

    private void OnResourceUpdated(ResourceType resourceType, Resource resource)
    {
        if (!resourceIcons.ContainsKey(resourceType))
        {
            var go = Instantiate(resourceIndicatorInstance, resourceIndicatorInstance.transform.parent);
            go.SetActive(true);
            resourceIcons[resourceType] = go.GetComponent<ResourceIndicator>();
        }

        resourceIcons[resourceType].SetResource(resource);
    }
}
