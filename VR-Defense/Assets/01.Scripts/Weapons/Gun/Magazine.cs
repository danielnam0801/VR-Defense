using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour
{
    public UnityEvent<int> OnBulletChanged;
    public UnityEvent<float> OnChargeChanged;

    public UnityEvent OnReloadStart;
    public UnityEvent OnReloadEnd;

    public int maxBullets = 20;
    public float chargingTime = 2f;
    private int currentBullets;

    public int CurrentBullet
    {
        get => currentBullets;
        set
        {
            if (value < 0)
                currentBullets = 0;
            else if (value > maxBullets)
                currentBullets = maxBullets;
            else
                currentBullets = value;

            OnBulletChanged?.Invoke(currentBullets);
            OnChargeChanged?.Invoke((float)currentBullets / maxBullets);
        }
    }

    private void Start()
    {
        CurrentBullet = maxBullets;
    }

    public bool Use(int amount = 1)
    {
        if (currentBullets >= amount)
        {
            currentBullets -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }


    [ContextMenu("Reload")]
    public void StartReload()
    {
        if (CurrentBullet == maxBullets) return;

        StopAllCoroutines();
        StartCoroutine(ReloadProcess());
    }

    public void StopReload()
    {
        StopAllCoroutines();
    }

    private IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke();

        var beginTime = Time.time;
        var beginBullets = CurrentBullet;
        var enoughPercent = 1f - ((float)CurrentBullet/maxBullets);
        var enoughChargeingTime = chargingTime * enoughPercent;

        while (true)
        {
            var t = (Time.time - beginTime) / enoughChargeingTime;
            if (t >= 1f) break;

            CurrentBullet = (int)Mathf.Lerp(beginBullets, maxBullets, t);
            yield return null;
        }
        CurrentBullet = maxBullets;

        OnReloadEnd?.Invoke();
    }

}
