using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace basics_01_game_objects_and_scripts 
{
public class Clock : MonoBehaviour
{
    [SerializeField] private Transform hoursPivot, minutesPivot, secondsPivot;

    private const float _hoursToDegrees = -30f;
    private const float _minutesToDegrees = -6f;
    private const float _secondsToDegrees = -6f;

    private void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        
        hoursPivot.localRotation = Quaternion.Euler(0f,0f, _hoursToDegrees * (float)time.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, _minutesToDegrees * (float)time.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, _secondsToDegrees * (float)time.TotalSeconds);
    }
}
    
}
