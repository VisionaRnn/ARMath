using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//如果下面两行没有自动补全，则要手动加上
using UnityEngine.UI;

public class ChooseGrade : MonoBehaviour
{
    [SerializeField]
    private Button fifthGrade;   //拖动赋值


    // Start is called before the first frame update
    void Start()
    {
        fifthGrade.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/5/5");
        });



    }
}
