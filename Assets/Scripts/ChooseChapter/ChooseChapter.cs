using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//如果下面两行没有自动补全，则要手动加上
using UnityEngine.UI;
public class ChooseChapter : MonoBehaviour
{
    [SerializeField]
    private Button polyAera;   //拖动赋值
    [SerializeField]
    private Button expolreGraph; //拖动赋值
    [SerializeField]
    private Button movementOfGraph; //拖动赋

    // Start is called before the first frame update
    void Start()
    {
        polyAera.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/5/5.1/5.1");
        });
        expolreGraph.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/5/5.2/5.2");
        });
        movementOfGraph.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/5/5.3/5.3");
        });


    }
}
