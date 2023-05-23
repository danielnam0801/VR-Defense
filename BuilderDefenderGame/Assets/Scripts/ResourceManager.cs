using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, int> resourceAmountDic;
    private ResourceTypeListSO _resourceTypelist;

    private void Awake()
    {
        _resourceTypelist = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceAmountDic = new Dictionary<ResourceTypeSO, int>();

        foreach(ResourceTypeSO resourceType in _resourceTypelist.resourceType)
        {
            resourceAmountDic[resourceType] = 0;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddResource(_resourceTypelist.resourceType[0], 1);
        }   
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddResource(_resourceTypelist.resourceType[1], 1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddResource(_resourceTypelist.resourceType[2], 1);
        }    
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDic[resourceType] += amount;
        LogResource();
    }

    private void LogResource()
    {
        foreach(ResourceTypeSO resourceType in resourceAmountDic.Keys)
        {
            Debug.Log(resourceType.name + " : " + resourceAmountDic[resourceType]);
        }
    }
}
