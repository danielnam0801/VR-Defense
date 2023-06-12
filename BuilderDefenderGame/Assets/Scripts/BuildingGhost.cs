using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;

    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (e.activeBuildingType == null)
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
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
    public void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
