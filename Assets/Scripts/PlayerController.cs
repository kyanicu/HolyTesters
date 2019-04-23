using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float
        runSpeed;

    private ObjectMover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<ObjectMover>();
    }

    public void MovePlayer(Vector2 direction)
    {
        mover.Move(direction * runSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
