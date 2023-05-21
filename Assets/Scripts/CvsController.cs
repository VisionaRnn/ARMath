using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CvsController : MonoBehaviour {

    public Text showAngleText;
    public void ShowAngleBtnClick()
    {
        if (showAngleText.gameObject.activeInHierarchy)
        {
            showAngleText.gameObject.SetActive(false);
        }
        else
        {
            showAngleText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (showAngleText.gameObject.activeInHierarchy)
        {
            showAngleText.text = "时针和分针的夹角为：" + Clock.Instance.AngleOfHoursAndMinutes.Round(0);
        }
    }
}
