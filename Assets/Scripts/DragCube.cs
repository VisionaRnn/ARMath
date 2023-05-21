
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCube : MonoBehaviour {
	private Vector3 orgPos;
	void Start()
	{
		orgPos = transform.position;

    }
	private float depth;
    private void OnMouseDrag()
	{
		//计算出Cube的中点到屏幕的距离
		depth = Camera.main.WorldToScreenPoint(transform.position).z;
		//获取鼠标位置
		Vector3 mousePosition = Input.mousePosition;
		//同样是鼠标的位置，但是具有深度
		mousePosition = new Vector3(mousePosition.x,mousePosition.y, depth);
		transform.position= Camera.main.ScreenToWorldPoint(mousePosition);
    }


    private void OnMouseUp()
	{
		print("OnMouseUp");

        Ray ray = new Ray(transform.position, Camera.main.transform.forward);
        ////声明一个Ray结构体，用于存储该射线的发射点，方向
        //RaycastHit hitInfo;
        ////声明一个RaycastHit结构体，存储碰撞信息
        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    Debug.Log(hitInfo.collider.gameObject.name);
        //    if (hitInfo.collider.GetComponent<Cube>())
        //    {
        //        transform.position = orgPos;
        //    }
        //}
        //ray = new Ray(transform.position, -Camera.main.transform.forward);
        ////声明一个RaycastHit结构体，存储碰撞信息
        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    Debug.Log(hitInfo.collider.gameObject.name);
        //    if (hitInfo.collider.GetComponent<Cube>())
        //    {
        //        transform.position = orgPos;
        //    }
        //}
        RaycastHit[] hitInfos = Physics.RaycastAll(ray);
        foreach (var item in hitInfos)
        {
            print(item.transform.name);
           if(item.transform.tag == "ModeImage")
            {
                transform.position = orgPos;
            }
        }
        ray = new Ray(transform.position, -Camera.main.transform.forward);
        hitInfos = Physics.RaycastAll(ray);
        foreach (var item in hitInfos)
        {
            print(item.transform.name);
            if (item.transform.tag == "ModeImage")
            {
                transform.position = orgPos;
            }
        }
    }

}
