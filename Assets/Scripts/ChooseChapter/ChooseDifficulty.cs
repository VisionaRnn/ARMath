using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//如果下面两行没有自动补全，则要手动加上
using UnityEngine.UI;
public class ChooseDifficulty : MonoBehaviour {

    private enum chapterString
    {
        多边形面积,
        认识图形,
        图形的运动
    }

    [SerializeField]
    private chapterString chapter;

    // Use this for initialization
    [SerializeField]
    private Button understand;   //拖动赋值
    [SerializeField]
    private Button practice; //拖动赋值
    [SerializeField]
    private Button enhancement; //拖动赋

    private string path;

    // Start is called before the first frame update
    void Start()
    {
        Choosepath();

        understand.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/5/"+path+"/"+path+".1");
        });
        practice.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/5/" + path + "/" + path + ".2");
        });
        enhancement.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/5/"+path + "/" + path + ".3");
        });

    }
    private void Choosepath()
    {
        if (chapter == chapterString.多边形面积)
        {
            path = "5.1";
        }
        else if (chapter == chapterString.认识图形)
        {
            path = "5.2";
        }
        else if (chapter == chapterString.图形的运动)
        {
            path = "5.3";
        }
    }
}
