using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{

    public int currentState = 0; // 当前状态
    public int targetState = 0; // 目标状态
    public float rotationSpeed = 5f; // 旋转速度

    private int numStates = 4; // 总状态数量
    private bool isRotating = false; // 是否正在旋转
    private float targetAngle = 0f; // 目标角度
    private Quaternion startRotation; // 初始旋转
    private Quaternion endRotation; // 结束旋转

    // Start is called before the first frame update
    void Start()
    {
        // 初始化拼图状态
        currentState = Random.Range(0, numStates);
        targetState = 0;
        transform.rotation = Quaternion.Euler(0f, 0f, currentState * 90f);
    }

    // Update is called once per frame
    void Update()
    {
        // 如果正在旋转，则进行插值旋转
        if (isRotating)
        {
            float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            if (Mathf.Abs(transform.eulerAngles.z - targetAngle) < 0.01f)
            {
                isRotating = false;
                transform.rotation = endRotation;
                currentState = (currentState + 1) % numStates;
                CheckCompletion();
            }
        }
    }

    // 点击拼图时调用
    void OnMouseDown()
    {
        if (!isRotating)
        {
            isRotating = true;
            startRotation = transform.rotation;
            targetAngle = startRotation.eulerAngles.z + 90f;
            if (targetAngle >= 360f)
            {
                targetAngle -= 360f;
            }
            endRotation = Quaternion.Euler(0f, 0f, targetAngle);
        }
    }

    // 检查是否完成拼图
    private void CheckCompletion()
    {
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.CheckPuzzleCompletion();
        }
        else
        {
            Debug.Log("PuzzleManager is null!");
        }

    }

    // 设置目标状态
    public void SetTargetState(int target)
    {
        targetState = target;
    }

    // 获取当前状态
    public int GetCurrentState()
    {
        return currentState;
    }

    // 获取目标状态
    public int GetTargetState()
    {
        return targetState;
    }
}



//public class PuzzleManager : MonoBehaviour
//{

//    public static PuzzleManager Instance;

//    public PuzzlePiece[] puzzlePieces;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Instance = this;
//        puzzlePieces = FindObjectsOfType<PuzzlePiece>();
//    }

//    // 检查是否完成拼图
//    public void CheckPuzzleCompletion()
//    {
//        bool isCompleted = true;
//        foreach (PuzzlePiece piece in puzzlePieces)
//        {
//            if (piece.GetCurrentState() != piece.GetTargetState())
//            {
//                isCompleted = false;
//                break;
//            }
//        }
//        if (isCompleted)
//        {
//            Debug.Log("Puzzle Completed!");
//            // 完成拼图后可以添加游戏结束的逻辑
//        }
//    }
//}