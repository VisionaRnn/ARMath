using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	public void appear(GameObject obj){
		obj.SetActive(true);
	}

	public void disappear(GameObject obj){
		obj.SetActive(false);
	}
}
