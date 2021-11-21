using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardNotification : AwardDisplay
{
    public Park park;
    public float displayTime = 5f;
    public Transform positionTransform;
    public AudioSource soundOnOpen;

    public void Awake()
    {
        park.OnNewAwardReceived += OnNewAwardReceived;
        transform.position = positionTransform.position;
        gameObject.SetActive(false);
    }

    private void OnNewAwardReceived(object sender, System.EventArgs e)
    {
        award = park.LatestAward;
        gameObject.SetActive(true);
        StartCoroutine(DisplayAward());
    }

    private IEnumerator DisplayAward()
    {
        yield return new WaitForSeconds(displayTime);
        gameObject.SetActive(false);
        soundOnOpen.Play();
    }
}
