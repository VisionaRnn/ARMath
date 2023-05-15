using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_control : MonoBehaviour {
	public GameObject prefab;
	int state = 0;

	IEnumerator OnMouseDown()    //使用协程
    {
		state = GameObject.Find("GameObject_5_1_1_3").GetComponent<control_5_1_1_3>().state;
		while (Input.GetMouseButtonDown(0))
		{
			Debug.Log(state);
			// float angle = Mathf.Atan2(-300f, 400f) / Mathf.PI * 180;
			if(state == 1){
				GameObject.Find("GameObject_5_1_1_3").GetComponent<control_5_1_1_3>().start_pos = transform.localPosition;
				GameObject.Find("GameObject_5_1_1_3").GetComponent<control_5_1_1_3>().state = 11;
			}
			if(state == 11){
				Vector3 start_pos = GameObject.Find("GameObject_5_1_1_3").GetComponent<control_5_1_1_3>().start_pos;
				float x1 = start_pos.x;
				float y1 = start_pos.y;
				float x2 = transform.localPosition.x;
				float y2 = transform.localPosition.y;
				float x = x1 - x2;
				float y = y1 - y2;
				Vector3 pos = new Vector3((x1 + x2) / 2, (y1 + y2) / 2, transform.localPosition.z);
				float len = Mathf.Sqrt(x*x + y*y);
				Debug.Log(len);
				float angle = Mathf.Atan2(x, y) / Mathf.PI * 180;
				GameObject cur = prefab;
				GameObject new_cube = Instantiate(cur, transform.parent) as GameObject;
				new_cube.transform.localPosition = pos;
				new_cube.transform.localRotation = Quaternion.Euler(angle, 90f, 0f);
				cur.transform.localScale = new Vector3(cur.transform.localScale.x, len, cur.transform.localScale.z);
			}

			yield return new WaitForFixedUpdate();//循环执行 
		}
    }
}
