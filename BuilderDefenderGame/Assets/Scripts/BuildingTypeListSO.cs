using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BuildingTypeListSO")]
public class BuildingTypeListSO : ScriptableObject
{
    public List<BuildingTypeSO> buildingTypeSO;
}
