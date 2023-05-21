using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
    Material putColorMat { get { return InstanceCenter.Instance.PutColorMat; } }
    Material orgColorMat { get { return InstanceCenter.Instance.OrgColorMat; } }
    MeshRenderer up;
    MeshRenderer down;
    MeshRenderer back;
    MeshRenderer front;
    MeshRenderer left;
    MeshRenderer right;
    Vector3 orgPosition;
    void Awake()
    {
        up = transform.Find("PlaneUP").GetComponent<MeshRenderer>();
        down = transform.Find("PlaneDown").GetComponent<MeshRenderer>();
        back = transform.Find("PlaneBack").GetComponent<MeshRenderer>();
        front = transform.Find("PlaneFront").GetComponent<MeshRenderer>();
        left = transform.Find("PlaneLeft").GetComponent<MeshRenderer>();
        right = transform.Find("PlaneRight").GetComponent<MeshRenderer>();
        orgPosition = transform.position;
    }
    public void PutColor()
    {
        up.material = putColorMat;
        down.material = putColorMat;
        back.material = putColorMat;
        front.material = putColorMat;
        left.material = putColorMat;
        right.material = putColorMat;
        Ray ray = new Ray(transform.position, transform.up);
        //声明一个Ray结构体，用于存储该射线的发射点，方向
        RaycastHit hitInfo;
        //声明一个RaycastHit结构体，存储碰撞信息
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.GetComponent<Cube>()) 
            {
                up.material = orgColorMat;
            }
        }
        ray = new Ray(transform.position, -transform.up);
        //声明一个RaycastHit结构体，存储碰撞信息
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.GetComponent<Cube>())
            {
                down.material = orgColorMat;
            }
        }
        ray = new Ray(transform.position, -transform.right);
        //声明一个RaycastHit结构体，存储碰撞信息
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.GetComponent<Cube>())
            {
                left.material = orgColorMat;
            }
        }
        ray = new Ray(transform.position, transform.right);
        //声明一个RaycastHit结构体，存储碰撞信息
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.GetComponent<Cube>())
            {
                right.material = orgColorMat;
            }
        }
        ray = new Ray(transform.position, transform.forward);
        //声明一个RaycastHit结构体，存储碰撞信息
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.GetComponent<Cube>())
            {
                front.material = orgColorMat;
            }
        }
        ray = new Ray(transform.position, -transform.forward);
        //声明一个RaycastHit结构体，存储碰撞信息
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.GetComponent<Cube>())
            {
                back.material = orgColorMat;
            }
        }
    }
    void OnEnable()
    {
        up.material = orgColorMat;
        down.material = orgColorMat;
        back.material = orgColorMat;
        front.material = orgColorMat;
        left.material = orgColorMat;
        right.material = orgColorMat;
        transform.position = orgPosition;
    }
}
