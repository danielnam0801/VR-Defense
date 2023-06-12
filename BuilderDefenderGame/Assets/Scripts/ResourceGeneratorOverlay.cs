using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private ResourceGeneratorData resourceGeneratorData;

    private void Start()
    {
        resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
    }
}
