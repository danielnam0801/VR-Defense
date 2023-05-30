using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    private Dictionary<BuildingTypeSO, Transform> buildingTransformDisc;
    private BuildingTypeListSO buildingTypeList;
    private Transform btnTemplate;
    private Transform arrowBtn;

    private void Awake()
    {
        buildingTransformDisc = new Dictionary<BuildingTypeSO, Transform>();
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        int index = 0;

        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(true);

        float offsetAmount = 130f;
        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
        arrowBtn.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);
        arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });
            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            buildingTransformDisc.Add(buildingType, btnTransform);
            index++;
        }
    }

    private void Update()
    {
        UpdateActiveBuildingTypeButton();    
    }

    private void UpdateActiveBuildingTypeButton()
    {
        arrowBtn.Find("selected").gameObject.SetActive(false);
        foreach (var building in buildingTransformDisc.Keys)
        {
            Transform btnTrm = buildingTransformDisc[building];
            btnTrm.Find("selected").gameObject.SetActive(false);
        }
        
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        
        if(activeBuildingType == null)
        {
            arrowBtn.Find("selected").gameObject.SetActive(true);
        }
        else
            buildingTransformDisc[activeBuildingType].Find("selected").gameObject.SetActive(true);
    }
    
}
