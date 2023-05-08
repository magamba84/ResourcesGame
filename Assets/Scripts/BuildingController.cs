using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{ 
    Mining,
    Producing,
    Trading
}
public class BuildingController : MonoBehaviour,IClickable
{
    [SerializeField]
    private BuildingType buildingType;

    public BuildingType GetType() 
    {
        return buildingType;
    }

    public void Click() 
    {
        UIManager.Instance.ShowBuildingWindow(this);
    }

}
