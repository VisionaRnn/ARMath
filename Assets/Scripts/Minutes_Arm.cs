using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minutes_Arm : Base_Arm {

    void Start()
    {
        SecondPerDegrees = 60f/360*60;
    }
}
