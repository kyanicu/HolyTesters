using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{

    Rigidbody rb;

    private bool hasRigidbody;
    private bool isContinuous;

    // Start is called before the first frame update
    void Start()
    {
        if ((rb = GetComponent<Rigidbody>()) != null)
        {
            hasRigidbody = true;
            if (rb.collisionDetectionMode == CollisionDetectionMode.Continuous)
                isContinuous = true;

            rb.useGravity = false;
            rb.freezeRotation = true;
            rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionY;
            
        }
    }

    /**
     * Moves an object a certain distance
     * takes in a vector2 in the form of (xMoveBy, zMoveBy)
     * Should be used with fixed update
     * Does not require rigidbody
     * Can be used with rigidbody for collision detection
     **/
    public void Move(Vector2 moveBy)
    {
        
        Vector3 moveByXZ = new Vector3(moveBy.x, 0, moveBy.y);
        /*
        if (hasRigidbody && isContinuous)
        {

            RaycastHit hit;
            if (rb.SweepTest(moveByXZ.normalized, out hit, moveByXZ.magnitude))
            {
                if (hit.distance > Physics.defaultContactOffset + 0.0035f)
                    moveByXZ = moveByXZ.normalized * (hit.distance - Physics.defaultContactOffset + 0.0035f);
            }

        }
        */
        //transform.position += moveByXZ;
        GetComponent<CharacterController>().Move(moveByXZ);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasRigidbody)
            rb.velocity = Vector3.zero;
    }
}
