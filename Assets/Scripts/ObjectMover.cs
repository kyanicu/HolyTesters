using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /**
     * Moves an object a certain distance
     * takes in a vector two in the form of (xMoveBy, zMoveBy)
     * Should be used with fixed update
     * Does not require rigidbody
     * Can be used with rigidbody to prevent physics,
     *      while still using discrete collision detection
     **/
    public void Move(Vector2 moveBy)
    {
        Vector3 moveByXZ = new Vector3(moveBy.x, 0, moveBy.y);

        transform.position += moveByXZ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
