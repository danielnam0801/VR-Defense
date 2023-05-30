using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObj;

    private void Awake()
    {
        spriteGameObj = transform.Find("sprite").gameObject;
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, EventArgs e)
    {
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if(activeBuildingType == null)
        {
            Hide();
        }
        else
        {
            Show(activeBuildingType.sprite);
        }
    }

    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }

    public void Show(Sprite ghostSprite)
    {
        spriteGameObj.SetActive(true);
        spriteGameObj.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    public void Hide()
    {
        spriteGameObj.SetActive(false);
    }
}
