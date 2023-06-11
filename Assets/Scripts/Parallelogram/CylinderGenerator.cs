using UnityEngine;

public class CylinderGenerator : MonoBehaviour
{
    public GameObject sphere1;
    public GameObject sphere2;
    [SerializeField]
    private float scale=0.03f;
    static public bool isFound;

    static public GameObject cylinder;

    private void Start()
    {
        CreateCylinder();
    }

    private void Update()
    {
        if (cylinder != null)
        {
            Destroy(cylinder);
        }
        CreateCylinder();
       
    }

    private void CreateCylinder()
    {
        // Get the position and radius of the spheres
        Vector3 position1 = sphere1.transform.position;
        Vector3 position2 = sphere2.transform.position;
        float radius1 = sphere1.transform.localScale.x / 2f;
        float radius2 = sphere2.transform.localScale.x / 2f;

        // Calculate the position and size of the cylinder
        Vector3 position = (position1 + position2) / 2f;
        float height = Vector3.Distance(position1, position2);
        float radius = Mathf.Min(radius1* scale, radius2* scale);

        // Create the cylinder
        cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = position;
        cylinder.transform.localScale = new Vector3(radius * 2f, height / 2f, radius * 2f);
        cylinder.transform.rotation = Quaternion.FromToRotation(Vector3.up, position2 - position1);
    }
}
