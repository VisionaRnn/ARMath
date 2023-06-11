using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//如果下面两行没有自动补全，则要手动加上
using UnityEngine.UI;

public class ButtonChapter : MonoBehaviour
{
    [SerializeField]
    private Button Puzzle;   //拖动赋值
    [SerializeField]
    private Button Parallelogram; //拖动赋值
    [SerializeField]
    private Button clientBtn; //拖动赋

    // Start is called before the first frame update
    void Start()
    {   
        Puzzle.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/Puzzle");
        });
        Parallelogram.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/Armath");//四边形场景（面积不变）
        });

     
    }
}
