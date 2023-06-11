using UnityEngine;

public class ObjectShakeAndRise : MonoBehaviour
{
    public float shakeIntensity = 0.1f; // 抖动强度
    public float shakeSpeed = 10f; // 抖动速度
    public float riseSpeed = 1f; // 上升速度
    public float stopHeight = 5f; // 停止上升的高度

    private Vector3 initialPosition; // 初始位置
    private float timer = 0f; // 计时器

    private bool startRise = false;

    private void Start()
    {
        // 保存初始位置
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (startRise == true)
        {
            // 更新计时器
            timer += Time.deltaTime;

            // 计算抖动的偏移量
            float shakeOffset = Mathf.PerlinNoise(timer * shakeSpeed, 0f) * 2f - 1f;
            Vector3 shakeVector = new Vector3(0f, shakeOffset, 0f) * shakeIntensity;

            // 计算上升的位移
            float riseOffset = timer * riseSpeed;
            Vector3 riseVector = new Vector3(0f, riseOffset, 0f);

            // 应用抖动和上升的位移
            transform.position = initialPosition + shakeVector + riseVector;

            // 检查是否达到停止高度
            if (transform.position.y >= stopHeight)
            {
                transform.position = new Vector3(transform.position.x, stopHeight, transform.position.z);
                enabled = false; // 停用脚本
            }
        }
    }

    public void StartShakeAndRise()
    {
        startRise = true;
    }
}
