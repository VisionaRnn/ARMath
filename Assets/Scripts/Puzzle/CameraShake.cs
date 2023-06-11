using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f; // 抖动持续时间
    public float shakeMagnitude = 0.1f; // 抖动幅度

    private Vector3 originalPosition; // 原始位置
    private float currentShakeDuration = 0f; // 当前抖动持续时间

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (currentShakeDuration > 0f)
        {
            // 在抖动持续时间范围内抖动相机
            Debug.Log("正在抖动");
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            // 抖动结束，恢复到原始位置
            currentShakeDuration = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void ShakeCamera()
    {
        currentShakeDuration = shakeDuration;
    }
}
