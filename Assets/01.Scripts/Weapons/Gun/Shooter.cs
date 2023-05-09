using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    public UnityEvent<Vector3> OnShootSuccess;
    public UnityEvent OnShootFail;

    public LayerMask hittableMask;
    public GameObject hitEffectPrefab;
    public Transform shootPoint;

    public float shootDelay = 0.1f;
    public float maxDistance = 100f;

    private void Start()
    {
        Stop();    
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
    
    private IEnumerator Process()
    {
        var wfs = new WaitForSeconds(shootDelay);

        while (true)
        {
            Shoot();
            yield return wfs;
        }
    }

    private void Shoot()
    {
        if(Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance, hittableMask)){
            OnShootSuccess?.Invoke(hitInfo.point);
            Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);
        }
        else
        {
            var hitPoint = shootPoint.position + shootPoint.forward * maxDistance;
            OnShootSuccess?.Invoke(hitPoint);
        }
    }
}
