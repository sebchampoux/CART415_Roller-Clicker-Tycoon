using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _dayDurationInSeconds = 1.0f;
    private bool _isRunning = true;

    public event EventHandler OnNewDay;
    public float DayDurationSeconds
    {
        get
        {
            return _dayDurationInSeconds;
        }
        private set
        {
            _dayDurationInSeconds = Mathf.Max(0f, value);
        }
    }
    
    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (_isRunning)
        {
            yield return new WaitForSeconds(DayDurationSeconds);
            OnNewDay?.Invoke(this, null);
        }
    }
}
