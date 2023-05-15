using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
	void Update()
    {
        Vector3 angles = this.transform.localEulerAngles;
        angles.z += 0.2f;    
        this.transform.localEulerAngles=angles;
    }
}