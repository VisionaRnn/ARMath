using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_control : MonoBehaviour {
	public GameObject prefab;
	int state = 0;

	IEnumerator OnMouseDown()    //使用协程
    {
		state = GameObject.Find("GameObject_5_1_1_3").GetComponent<control_5_1_1_3>().state;

		while (Input.GetMouseButtonDown(0)) {
			if(state == 2) {
				Instantiate(prefab, transform.position, Quaternion.identity, transform.parent);
				GameObject.Find("GameObject_5_1_1_3").GetComponent<control_5_1_1_3>().state = 0;
			}
			yield return new WaitForFixedUpdate();//循环执行 
		}
    }


}
