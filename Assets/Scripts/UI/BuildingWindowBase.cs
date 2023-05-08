using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingWindowBase : MonoBehaviour
{
	[SerializeField]
	private Button buttonClose;
	public virtual void Init(BuildingController building)
	{ 
	
	}

	public void Close() 
	{
		Destroy(gameObject);
	}

}
