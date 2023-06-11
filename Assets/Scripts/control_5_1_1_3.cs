using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_5_1_1_3 : MonoBehaviour {
	//0取消 1塑料套 11已经选择了塑料套的初始点 2串珠
	public int state = 0;	
	public Transform parent;
	public Vector3 start_pos;
	
    public void instantiate(GameObject prefab)
    {
		Destroy(GameObject.FindGameObjectWithTag("org"));
		//参数1：要创建的预制体。参数2：预制体的位置。参数3：预制体的方向
		GameObject obj = Instantiate(prefab, parent) as GameObject;
		obj.transform.localPosition = new Vector3(0,0.2f,0);
		obj.tag = "org";
    }

	public void cube(){
		state = 1;
	}

	public void sphere(){
		state = 2;
	}

	public void cancle(){
		state = 0;
	}
}
