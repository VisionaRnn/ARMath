using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class control : MonoBehaviour {
  string tag = "org";
	public Transform parent;
    // Start is called before the first frame update
    public void instantiate(GameObject prefab)
    {
      Destroy(GameObject.FindGameObjectWithTag("org"));
      Destroy(GameObject.FindGameObjectWithTag("sym"));
      //参数1：要创建的预制体。参数2：预制体的位置。参数3：预制体的方向
      GameObject obj = Instantiate(prefab, new Vector3(0,0.1f,0), Quaternion.Euler(0f,0f,0f), parent) as GameObject;
      obj.tag = "org";
    }

    public void overturn() {
      //获取当前三角形
      GameObject org = GameObject.FindGameObjectWithTag(tag);
      //生成一个与原三角形中心对称的三角形
      GameObject obj = Instantiate(org, org.transform.position, 
                                   Quaternion.Euler(0f,0f,180f),
                                   parent) as GameObject;
      obj.tag = "sym";
    }
}
