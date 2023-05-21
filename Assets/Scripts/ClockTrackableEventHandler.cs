using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ClockTrackableEventHandler : DefaultTrackableEventHandler
{
    public GameObject Clock;
    public GameObject Canvas;
    protected override void OnTrackingFound()
    {
        Clock.SetActive(true);
        Canvas.SetActive(true);
    }
    protected override void OnTrackingLost()
    {
        Clock.SetActive(false);
        Canvas.SetActive(false);
    }
}
