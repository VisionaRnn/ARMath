using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceCenter : MonoBehaviour {

    public static InstanceCenter Instance {
        get
        {
            if (instance==null)
            {
                instance= GameObject.FindObjectOfType<InstanceCenter>();
            }
            return instance;
        }
    }
    private static InstanceCenter instance;


    public Cube CubePrefab;
    public Canvas Canvas;
    public Material PutColorMat;
    public Material OrgColorMat;

    public GameObject Mode2;
    public GameObject Mode3;
    public GameObject Mode4;
    public GameObject Moder2Variant;
    public GameObject Moder3Variant;
    public GameObject Moder4Variant;
}
