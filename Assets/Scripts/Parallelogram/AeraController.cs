using System;
using UnityEngine;
using UnityEngine.UI;

public class AeraController : MonoBehaviour
{
    private Text timeDisplay;

    private void Start()
    {
        timeDisplay = GetComponent<Text>();
    }

    private void Update()
    {
        DateTime now = DateTime.Now;
        timeDisplay.text = now.ToString("HH:mm:ss");
    }
}
