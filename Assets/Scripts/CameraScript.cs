using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector3 center;

    [SerializeField]
    private float
        distance,
        height,
        angle,
        speed;
    [SerializeField]
    GameObject initRoomTransitioner;

    // Start is called before the first frame update
    void Start()
    {
        center = initRoomTransitioner.transform.position;
        transform.rotation = Quaternion.Euler(angle, 0, 0);
        transform.position = new Vector3(center.x, height, center.z - distance);
    }

    public void ChangeCenter(Vector3 _center)
    {
        center = _center;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(center.x, height, center.z - distance), speed * Time.deltaTime);
    }
}
