
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICenter : MonoBehaviour {

    [SerializeField]
    public ModeType DefaultMode;
    private ModeType currentMode;
    
    void Awake()
    {
        currentMode = DefaultMode;
        changeMode();
    }
	public void OnPutColorButonClick()
	{
        changeMode();


        foreach (var item in GameObject.FindObjectsOfType<Cube>())
		{
			item.PutColor();
		}
	}

	public void CreateMode2()
	{
        ClearMode();
        InstanceCenter.Instance.Mode2.SetActive(true);
        currentMode = ModeType.Moder2;
    }
    public void CreateMode3()
    {
        ClearMode();
        InstanceCenter.Instance.Mode3.SetActive(true);
        currentMode = ModeType.Moder3;
    }
    public void CreateMode4()
    {
        ClearMode();
        InstanceCenter.Instance.Mode4.SetActive(true);
        currentMode = ModeType.Moder4;
    }
    public void CreateMode2Variant()
    {
        ClearMode();
        InstanceCenter.Instance.Moder2Variant.SetActive(true);
        currentMode = ModeType.Moder2Variant;
    }
    public void CreateMode3Variant()
    {
        ClearMode();
        InstanceCenter.Instance.Moder3Variant.SetActive(true);
        currentMode = ModeType.Moder3Variant;
    }
    public void CreateMode4Variant()
    {
        ClearMode();
        InstanceCenter.Instance.Moder4Variant.SetActive(true);
        currentMode = ModeType.Moder4Variant;
    }
    private void ClearMode()
    {
        InstanceCenter.Instance.Mode2.SetActive(false);
        InstanceCenter.Instance.Mode3.SetActive(false);
        InstanceCenter.Instance.Mode4.SetActive(false);
        InstanceCenter.Instance.Moder2Variant.SetActive(false);
        InstanceCenter.Instance.Moder3Variant.SetActive(false);
        InstanceCenter.Instance.Moder4Variant.SetActive(false);
    }

    private void changeMode()
    {
        switch (currentMode)
        {
            case ModeType.Moder2:
                CreateMode2();
                break;
            case ModeType.Moder3:
                CreateMode3();
                break;
            case ModeType.Moder4:
                CreateMode4();
                break;
            case ModeType.Moder2Variant:
                CreateMode2Variant();
                break;
            case ModeType.Moder3Variant:
                CreateMode3Variant();
                break;
            case ModeType.Moder4Variant:
                CreateMode4Variant();
                break;
            default:
                break;
        }
    }
    
}
[Serializable]
public enum ModeType
{
    Moder2,Moder3,Moder4, Moder2Variant, Moder3Variant,Moder4Variant
}