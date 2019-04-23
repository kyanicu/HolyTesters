using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Fields
    [SerializeField]
    public float
        speed;

    //Internal references
    ObjectMover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<ObjectMover>();
    }

    //Only called once during Fixed Update
    public void MovePlayer(Vector2 direction)
    {

        if (direction == Vector2.zero)
            return;

        direction.Normalize();

        mover.Move(speed * direction * Time.fixedDeltaTime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
