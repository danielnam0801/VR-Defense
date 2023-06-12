using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null && CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition()))
            {
                Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            }  
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 pos)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(pos + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAeaClear = collider2DArray.Length == 0;
        if(!isAeaClear) return false;


        collider2DArray = Physics2D.OverlapCircleAll(pos, 7f);

        foreach (Collider2D col in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = col.GetComponent<BuildingTypeHolder>(); 
            if(buildingTypeHolder != null)
            {
                if (buildingType == buildingTypeHolder.buildingType) return false;
            }
        }

        float maxConstructionRadius = 25f;

        collider2DArray = Physics2D.OverlapCircleAll(pos, maxConstructionRadius);
        
        foreach (Collider2D col in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = col.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                return true;
            }
        }

        return false;

    }
}
