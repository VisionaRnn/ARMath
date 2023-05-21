using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CubeBuilder : MonoBehaviour {
	private Cube cubePrefab;

    public int X;
	public int Y;
	public int Z;

	public float Interval;

    [SerializeField]
    public ModerType Mode;

    [Serializable]
    public enum ModerType
    {
        BigCube, HalfBigCube
    }
    void Awake()
    {
		cubePrefab = InstanceCenter.Instance.CubePrefab;


    }
    void Start () {
        CreateMode();

    }

    void CreateMode()
    {
        if (Mode== ModerType.BigCube)
        {
            CreateBigCube();
        }
        else
        {
            CreateHalfBigCube();
        }
    }
	void CreateBigCube()
	{
        int count = 0;
        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                for (int z = 0; z < Z; z++)
                {
                    count++;
                    Cube cube = GameObject.Instantiate(cubePrefab, this.transform);
                    cube.transform.position = new Vector3(cubePrefab.transform.position.x + x * Interval, cubePrefab.transform.position.y + y * Interval, cubePrefab.transform.position.z + z * Interval);
                    cube.name = "Cube:" + x + "," + y + "," + z+","+count;
                }
            }
        }
    }
    void CreateHalfBigCube()
    {

    }
}
