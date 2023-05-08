using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTransformer : MonoBehaviour, IWorkable
{
    [SerializeField]
    private ResourceTransform resourceTransform;

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
