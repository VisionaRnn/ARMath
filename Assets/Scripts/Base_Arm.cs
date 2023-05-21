using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Arm : MonoBehaviour {

    private Vector3 lastMouseDrag_Vector;
    protected float SecondPerDegrees;
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, LayerMask.GetMask("Face")))
        {
            Vector3 center = hitInfo.transform.position;
            Vector3 mouseDown_Pos = hitInfo.point;
            Vector3 mouseDrag_Vector = mouseDown_Pos - center;

            lastMouseDrag_Vector = mouseDrag_Vector;
        }
    }


    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, LayerMask.GetMask("Face")))
        {
            Vector3 center = hitInfo.transform.position;
            Vector3 mouseDrag_Pos = hitInfo.point;
            Vector3 mouseDrag_Vector = mouseDrag_Pos - center;
            float addAngle = VectorAngle(lastMouseDrag_Vector, mouseDrag_Vector);
            Clock.Instance.SecondChanged += addAngle* SecondPerDegrees;


            lastMouseDrag_Vector = mouseDrag_Vector;
        }

    }


    float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;

        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }
}
