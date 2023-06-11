using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    private List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();

    [SerializeField]
    private ObjectShakeAndRise shakeAndRiseScript;
    [SerializeField]
    private CameraShake camShake;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
     

        // 获取所有的 PuzzlePiece 组件
        PuzzlePiece[] pieces = FindObjectsOfType<PuzzlePiece>();
        foreach (PuzzlePiece piece in pieces)
        {
            // 将 PuzzlePiece 添加到列表中
            puzzlePieces.Add(piece);
        }
    }

    // 检查是否完成拼图
    public void CheckPuzzleCompletion()
    {
        bool isCompleted = true;

        // 检查每个 PuzzlePiece 的当前状态是否与目标状态匹配
        foreach (PuzzlePiece piece in puzzlePieces)
        {
         //   Debug.Log("Current:" + piece.GetCurrentState() + " Target:" + piece.GetTargetState());

            if (piece.GetCurrentState() != piece.GetTargetState())
            {

                isCompleted = false;
                break;
            }
        }

        if (isCompleted)
        {
            Debug.Log("puzzle Completed！");
            // 在拼图完成时调用 ObjectShakeAndRise 脚本
            // 在拼图完成时调用 ObjectShakeAndRise 脚本
            if (shakeAndRiseScript != null)
            {
                shakeAndRiseScript.StartShakeAndRise();
                camShake.ShakeCamera();

            }
        }
    }
}
