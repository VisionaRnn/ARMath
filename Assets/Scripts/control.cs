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
      GameObject obj = Instantiate(prefab, new Vector3(0,0,0), Quaternion.Euler(90f,0f,0f), parent) as GameObject;
      obj.tag = "org";
    }

    public void overturn()
    {
      GameObject org = GameObject.FindGameObjectWithTag(tag);
      GameObject obj = Instantiate(org, org.transform.position, Quaternion.Euler(90f,0f,180f), parent) as GameObject;
      obj.tag = "sym";
    }
}
