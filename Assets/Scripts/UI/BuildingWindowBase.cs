using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingWindowBase : UIWindowBase
{
	[SerializeField]
	private Button buttonClose;

	
	public virtual void Init(BuildingController building)
	{ 
	
	}

}
