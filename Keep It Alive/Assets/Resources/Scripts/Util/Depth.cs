using UnityEngine;

[ExecuteInEditMode]
public class Depth : MonoBehaviour
{

    public Transform target;
    public float depthSortOffset;

    void Start()
    {
        if (target == null)
        {
            target = transform;
        }
    }

    void Update()
    {
        target.GetComponent<Renderer>().sortingOrder = (int)((transform.position.y + depthSortOffset) * -1000);
    }


}