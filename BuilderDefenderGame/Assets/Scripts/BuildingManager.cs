using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform pfWoodHarvester;
    [SerializeField] private Transform pfStoneHarvester;
    [SerializeField] private Transform pfGoldHarvester;

    public Transform currentTrm;

    private void Start()
    {
        currentTrm = pfWoodHarvester;
    }

    private void Update()
    {
        KeyInput();
    }

    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            currentTrm = pfWoodHarvester.transform;
        if (Input.GetKeyDown(KeyCode.W))
            currentTrm = pfStoneHarvester.transform;
        if (Input.GetKeyDown(KeyCode.E))
            currentTrm = pfGoldHarvester.transform;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(currentTrm, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }
}
