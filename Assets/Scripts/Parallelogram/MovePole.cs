using System;
using UnityEngine;
using UnityEngine.UI;

public class MovePole : MonoBehaviour
{
    //两个球同时移动
    public Transform Sphere1;
    public Transform Sphere2;

    public Transform topPole;
    public Transform bottomPole;

    //文本
    public Text text; //在unity里的定义

    public float speed = 1f; // 移动速度
    public float zOffset = 0f; // 物体移动的z轴偏移量

    private bool isDragging = false; // 是否正在拖拽
    private Vector3 lastMousePosition; // 上一帧鼠标的位置

    public char direction;

    private float safeDistance = 0.1f;
    private float adjustDistance = 0.05f;
    private float littleDistance = 0.01f;

    private float aera = 5;
    

    private float deltaDirection()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        if (direction == 'x')
        {
            return delta.x * Time.deltaTime * speed;
        }
        else if (direction == 'y')
        {
            return delta.y * Time.deltaTime * speed; ;
        }
        else if (direction == 'z')
        {
            return delta.z * Time.deltaTime * speed; ;
        }
        else
            return 0;
    }

    private void Start()
    {
        text.text = "面积为:"+aera.ToString();
    }
        

    // 鼠标按下事件
    private void OnMouseDown()
    {
        isDragging = true; // 开始拖拽
        lastMousePosition = Input.mousePosition; // 记录上一帧鼠标的位置
    }

    // 鼠标抬起事件
    private void OnMouseUp()
    {
        isDragging = false; // 停止拖拽
    }

    // 鼠标拖拽事件
    private void OnMouseDrag()
    {

        if (isDragging)
        {
            // 计算鼠标移动的距离
            // Vector3 delta = Input.mousePosition - lastMousePosition;

            // 计算在轴上移动的距离
            float Delta = deltaDirection();

            Vector3 newPosition;
            // 计算新的物体位置
            if (direction == 'x')
            {
                newPosition = transform.position + new Vector3(0f, 0f, Delta);
            }

            else if (direction == 'y')
            {
                newPosition = transform.position + new Vector3(0f, Delta, 0f);
            }

            else
            {
                newPosition = transform.position;
            }

            if (topPole.position.y>=bottomPole.position.y+safeDistance) 
            {
                Debug.Log("KEYIDONG");
                Vector3 newSpherePos1 = Sphere1.position + new Vector3(0f, 0f, Delta);
                Vector3 newSpherePos2 = Sphere2.position + new Vector3(0f, 0f, Delta);

                Sphere1.position = newSpherePos1;
                Sphere2.position = newSpherePos2;

                transform.position = newPosition;

                aera += Delta*6;
      
               // aera = aera.ToString("#0.00");//点后面几个0就保留几位 
                                             // aera = (int)Math.Floor(aera);

                text.text = "面积："+aera.ToString("#0.00");
            }

            if (topPole.position.y<bottomPole.position.y+safeDistance)
            {
                topPole.position=topPole.position+ new Vector3(0f, littleDistance, 0f);
                Vector3 newSpherePos1 = Sphere1.position + new Vector3(0f, littleDistance, 0f);
                Vector3 newSpherePos2 = Sphere2.position + new Vector3(0f, littleDistance, 0f);
                Sphere1.position = newSpherePos1;
                Sphere2.position = newSpherePos2;
                Debug.Log("noo");
            }

            
            // 记录当前鼠标位置
            lastMousePosition = Input.mousePosition;
        }
    }
}
