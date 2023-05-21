using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    public static Clock Instance;
    // 定义时针、分针、秒针步伐角度
    const float degreesPerHour = 30f;
    const float degreesPerMinute = 6f;
    const float degreesPerSecond = 6f;

    // 时针、分针、秒针的旋转体 Transform
    public Transform hoursTransform;
    public Transform minutesTransform;
    public Transform secondsTransform;

    // 指针行走模式
    public bool continuous = false;

    //秒数改变量
    public float SecondChanged;


    private void Awake()
    {
        Instance = this;
        UpdateDiscrete();
    }

    private void Update()
    {
        if (continuous)
        {
            UpdateContinuous();
        }
        else
        {
            UpdateDiscrete();
        }
    }

    // 逐步方式更新当前时间
    private void UpdateDiscrete()
    {
        DateTime time = DateTime.Now;
        
        hoursTransform.localRotation =
            Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);

        minutesTransform.localRotation =
            Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);

        secondsTransform.localRotation =
            Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
    }

    // 持续方式更新当前时间
    private void UpdateContinuous()
    {
        DateTime time1 = DateTime.Now;
        time1= time1.AddSeconds(SecondChanged);
        TimeSpan time2 = time1.TimeOfDay;
        hoursTransform.localRotation =
            Quaternion.Euler(0f, (float)time2.TotalHours * degreesPerHour, 0f);

        minutesTransform.localRotation =
            Quaternion.Euler(0f, (float)time2.TotalMinutes * degreesPerMinute, 0f);

        secondsTransform.localRotation =
            Quaternion.Euler(0f, (float)time2.TotalSeconds * degreesPerSecond, 0f);
    }

    public float AngleOfHoursAndMinutes
    {
        get
        {
            float temp = minutesTransform.localEulerAngles.y - hoursTransform.localEulerAngles.y;
            if(temp<0)
            {
                temp = -temp;
            }
            if (temp>180)
            {
                temp = 360 - temp;
            }
            return temp;
        }
    }

}