using UnityEngine;

public class lineController : MonoBehaviour
{
    [SerializeField] private Vector3 pointA,pointB;
    [SerializeField] private LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if(pointA != null)
        {
            lr.SetPosition(0,pointA);
            lr.SetPosition(1,pointB);
        }
    }
    public void setPoints(Vector3 a, Vector3 b)
    {
        pointA = a;
        pointB = b;
    }
}
