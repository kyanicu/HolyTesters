using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float
        runSpeed,
        runAccel,
        runDecel,
        dashSpeed,
        dashTime,
        dashCoolDownTime;

    private bool
        isDashing,
        isStunned;

    private ObjectMover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<ObjectMover>();
    }
    public void MovePlayer(Vector2 direction)
    {

        direction.Normalize();

        if(isStunned /*|| isDashing*/ || direction == Vector2.zero)
            return;
        else
            mover.Move(direction * runSpeed * Time.fixedDeltaTime);
    
        if (!isDashing)
            transform.forward = new Vector3(direction.x, 0, direction.y);
    
    }

    public void OnCollisionEnter(Collision col) 
    {
        
    }

    public void InitiateDash(Vector2 direction) 
    {
        if(isStunned || isDashing || direction == Vector2.zero)
            return;
        else
            StartCoroutine("Dash", direction);
    }

    public IEnumerator Dash(Vector2 direction) 
    {
        isDashing = true;
        
        for (float timer = 0; timer < dashTime; timer += Time.fixedDeltaTime)
        {
            if (isStunned)
                break;
            mover.Move(direction * dashSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        
        for (float timer = 0; timer < dashCoolDownTime; timer += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }


        isDashing = false;

    }

    public void Stun()
    {
        isStunned = true;
    }

    public void Unstun()
    {
        isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
