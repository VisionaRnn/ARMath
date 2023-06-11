using UnityEngine;

public class FadeInObjects : MonoBehaviour
{
    public float fadeDuration = 2f; // 过渡时间
    public float fadeDelay = 1f; // 延迟开始过渡的时间

    private MeshRenderer[] objectRenderers; // 存储所有物体的MeshRenderer组件
    private float timer = 0f; // 过渡计时器

    private void Start()
    {
        // 获取空对象下的所有子物体的MeshRenderer组件
        objectRenderers = GetComponentsInChildren<MeshRenderer>();

        // 将所有物体的材质的透明度设置为0，使其变为模糊状态
        foreach (MeshRenderer renderer in objectRenderers)
        {
            Material[] materials = renderer.materials;
            foreach (Material material in materials)
            {
                Color objectColor = material.color;
                objectColor.a = 0f;
                material.color = objectColor;
            }
        }

        // 开始延迟计时
        Invoke("StartFadeIn", fadeDelay);
    }

    private void StartFadeIn()
    {
        // 启动过渡计时器
        timer = 0f;
    }

    private void Update()
    {
        // 如果计时器小于过渡时间，则逐渐增加物体的透明度
        if (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = timer / fadeDuration;

            foreach (MeshRenderer renderer in objectRenderers)
            {
                Material[] materials = renderer.materials;
                foreach (Material material in materials)
                {
                    Color objectColor = material.color;
                    objectColor.a = alpha;
                    material.color = objectColor;
                }
            }
        }
    }
}
