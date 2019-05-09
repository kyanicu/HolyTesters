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
        isRunning,
        isStunned;

    private ObjectMover mover;
    private Animator animator;
    private CameraScript cameraScript;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<ObjectMover>();
        animator = GetComponentInChildren<Animator>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
    }
    public void MovePlayer(Vector2 direction)
    { 
        if (isRunning && direction == Vector2.zero)
        {
            animator.SetBool("Running", false);
            isRunning = false;
        }

        direction.Normalize();

        if(isStunned /*|| isDashing*/ || direction == Vector2.zero)
            return;
        
        if (!isRunning)
            animator.SetBool("Running", true);

        mover.Move(direction * runSpeed * Time.fixedDeltaTime);
        isRunning = true;
        transform.forward = new Vector3(direction.x, 0, direction.y);
    
    }

    public void OnCollisionEnter(Collision col) 
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RoomTransitioner")
        {
            cameraScript.ChangeCenter(other.transform.parent.transform.position);
        }
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
        transform.forward = new Vector3(direction.x, 0, direction.y);

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
        Debug.Log(isRunning);
    }
}
