using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{   
    private BuildingTypeListSO buildingList;
    public BuildingTypeSO currentBuilding;
    private Camera _mainCam;
    public Camera MainCam => _mainCam;

    private void Start()
    {
        buildingList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        currentBuilding = buildingList.buildingTypeSO[0];
        _mainCam = Camera.main;
    }

    private void Update()
    {
        KeyInput();
    }

    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            currentBuilding = buildingList.buildingTypeSO[0];
        if (Input.GetKeyDown(KeyCode.W))
            currentBuilding = buildingList.buildingTypeSO[1];
        if (Input.GetKeyDown(KeyCode.E))
            currentBuilding = buildingList.buildingTypeSO[2];

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(currentBuilding.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = MainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }
}
