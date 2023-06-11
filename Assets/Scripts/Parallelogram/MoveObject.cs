using UnityEngine;

public class MoveObject : MonoBehaviour
{
    //两个球同时移动
    public Transform AnotherSphere;

    public float speed = 0.1f; // 移动速度
    public float zOffset = 0f; // 物体移动的z轴偏移量

    private bool isDragging = false; // 是否正在拖拽
    private Vector3 lastMousePosition; // 上一帧鼠标的位置

    public char direction;

    private float deltaDirection()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
     //   Debug.Log(delta);
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
            Debug.Log(delta.z);
            return delta.z * Time.deltaTime * speed; ;
        }
        else 
        return 0;
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
                 newPosition = transform.position + new Vector3(0f, 0f, -Delta);
            }

            else if (direction == 'y')
            {
                 newPosition = transform.position + new Vector3(Delta, 0f, 0f);
            }

            else
            {
                newPosition = transform.position + new Vector3(Delta, 0f, 0f);
               // Debug.Log(Delta);
               // Debug.Log("z");
            }
            //Debug.Log(newPosition); 
          

            if (AnotherSphere != null)
            {
                Vector3 newAnotherPos = AnotherSphere.position + new Vector3(Delta, 0f, 0f);
                // 只在z轴上移动，并加上偏移量
                // newPosition.z = zOffset;
                // 更新物体位置
                AnotherSphere.position = newAnotherPos;
            }
            transform.position = newPosition;
            // 记录当前鼠标位置
            lastMousePosition = Input.mousePosition;
        }
    }
}
