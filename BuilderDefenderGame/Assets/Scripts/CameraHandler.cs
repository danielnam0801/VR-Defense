using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCam;
    private float orthographicSize;
    private float targetOrthographicSize;

    public float moveSpeed = 50f;
    public float zoomAmount = 2f;
    public float zoomSpeed = 5f;

    public float minOrthographicSize = 10f;
    public float maxOrthographicSize = 30f;

    private void Start()
    {
        orthographicSize = vCam.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleZoom()
    {
        targetOrthographicSize += -Input.mouseScrollDelta.y * Time.deltaTime * zoomAmount;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        vCam.m_Lens.OrthographicSize = orthographicSize;
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(x, y).normalized;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
