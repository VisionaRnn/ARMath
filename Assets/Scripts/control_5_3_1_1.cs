using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_5_3_1_1 : MonoBehaviour {
	public void appear(GameObject obj){
		obj.SetActive(true);
	}

	public void disappear(GameObject obj){
		obj.SetActive(false);
	}
}

