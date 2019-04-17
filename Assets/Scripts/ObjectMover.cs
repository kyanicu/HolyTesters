using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{

    private enum CollisionType { NONE, DISCRETE, CONTINUOUS }

    private struct MoveData
    {

        public Vector2 moveInitial;
        public Vector2 moveDone;
        public Vector2 moveLeft { get { return moveInitial - moveDone; } }
        public Vector2 normal;
        public RaycastHit2D[] hits;
        public int hitSize;

        public MoveData(Vector2 init, Vector2 done, Vector2 _normal, RaycastHit2D[] _hits, int _hitSize)
        {
            moveInitial = init;
            moveDone = done;
            normal = _normal;
            hits = _hits;
            hitSize = _hitSize;
        }

    }

    private bool moveCompleted(MoveData moveData)
    {
        return moveData.moveLeft.magnitude <= totalCollisionOffset;
    }

    private float extraCollisionOffset;
    private float totalCollisionOffset { get { return Physics2D.defaultContactOffset + extraCollisionOffset; } }

    private Collider2D col;
    private Rigidbody2D rb;

    private LayerMask layerMask { get { return Physics2D.GetLayerCollisionMask(gameObject.layer); } }

    List<Collider2D> collidersEntered = new List<Collider2D>();
    List<Collider2D> triggersEntered = new List<Collider2D>();
    List<Collider2D> collidersStayed = new List<Collider2D>();
    List<Collider2D> triggersStayed = new List<Collider2D>();
    List<Collider2D> collidersExited = new List<Collider2D>();
    List<Collider2D> triggersExited = new List<Collider2D>();

    [SerializeField]
    private bool 
        checkCollisionWhenIdle,
        stopOnCollision;

    [SerializeField]
    private CollisionType collisionType;

    private bool moveCalled;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Collider2D>() == null)
            collisionType = CollisionType.NONE;
        if (collisionType != CollisionType.NONE && GetComponent<Rigidbody2D>() == null)
            collisionType = CollisionType.DISCRETE;

        if (GetComponent<Rigidbody2D>() != null)
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        extraCollisionOffset = 0.0035f;

    }

    private void translate(Vector2 moveBy)
    {
        transform.position += (Vector3)moveBy;
    }

    private MoveData moveUntilCollision(Vector2 moveBy)
    {

        Vector2 initMoveBy = moveBy;
        Vector2 normal = Vector2.zero;
        int maxSize = 20;

        Physics2D.queriesHitTriggers = true;

        RaycastHit2D[] hits = new RaycastHit2D[maxSize];
        int numHits;
        if ((numHits = col.Cast(moveBy.normalized, hits, moveBy.magnitude)) > 0)
        {

            Physics2D.queriesHitTriggers = false;

            int collidersHit = 0;
            RaycastHit2D[] colliderHits = new RaycastHit2D[numHits];

            if (numHits > maxSize)
                numHits = maxSize;


            float distance = moveBy.magnitude;
            for (int i = 0; i < numHits; i++)
            {

                if (hits[i].collider.isTrigger)
                    continue;

                if (hits[i].distance < distance)
                    distance = hits[i].distance;
            }

            bool projTest = false;
            for (int i = 0; i < numHits; i++)
            {

                if (hits[i].distance > distance)
                    continue;
                else if (hits[i].distance < distance && hits[i].collider.tag == "One Way Surface")
                    continue;

                if (hits[i].collider.isTrigger)
                {
                    addTrigger(hits[i].collider);
                    continue;
                }
                else
                {
                    addCollider(hits[i].collider);
                    colliderHits[collidersHit] = hits[i];
                    collidersHit++;

                    normal += hits[i].normal;
                    if (Vector3.Project(moveBy.normalized * hits[i].distance, hits[i].normal).magnitude <= totalCollisionOffset)
                    {
                        projTest = true;
                        break;
                    }
                }
            }
            if (collidersHit == 0)
            {

                for (int i = 0; i < numHits; i++)
                {
                    hits[i] = new RaycastHit2D();
                }
                numHits = 0;

                Physics2D.queriesHitTriggers = false;

                if (moveBy.magnitude <= totalCollisionOffset)
                {
                    return new MoveData(initMoveBy, Vector2.zero, normal, hits, numHits);
                }

            }
            else
            {

                hits = colliderHits;
                numHits = collidersHit;

                normal.Normalize();

                if (distance < 0)
                    distance = 0;

                moveBy = (moveBy.normalized * (distance - totalCollisionOffset));

                if (Vector2.Dot(moveBy, initMoveBy) <= 0)
                    moveBy = initMoveBy.normalized * totalCollisionOffset / 2; 

                if (projTest || distance <= totalCollisionOffset)
                {
                    return new MoveData(initMoveBy, Vector2.zero, normal, hits, numHits);
                }
            }

        }
        else
        {
            Physics2D.queriesHitTriggers = false;

            if (moveBy.magnitude <= totalCollisionOffset)
            {
                return new MoveData(initMoveBy, Vector2.zero, normal, hits, numHits);
            }
        }

        translate(moveBy);

        return new MoveData(initMoveBy, moveBy, normal, hits, numHits);

    }

    private void MoveContinuous(Vector2 moveBy)
    {
        if (stopOnCollision)
        {
            Vector2 newMoveBy = moveBy;
            MoveData moveData;
            List<Vector2> directions = new List<Vector2>();
            directions.Add(newMoveBy.normalized);
            int loops = 0;
            while (!moveCompleted(moveData = moveUntilCollision(newMoveBy)))
            {

                directions.Add(newMoveBy);

                Vector2 slopeFromNormal = Quaternion.Euler(0, 0, -90) * moveData.normal;

                newMoveBy = Vector3.Project(Vector3.Project(moveData.moveLeft, moveBy), slopeFromNormal);

                if (Vector2.Dot(newMoveBy, moveBy) <= 0 || directions.Contains(newMoveBy.normalized))
                    break;

                directions.Add(newMoveBy.normalized);

                loops++;
                if (loops > 10)
                {
                    Debug.LogError("MoveContinuous(): Possible Infinite Loop. Exiting");
                    break;
                }
            }
        }

        else
        {
            int maxSize = 20;

            Physics2D.queriesHitTriggers = true;

            RaycastHit2D[] hits = new RaycastHit2D[maxSize];
            int numHits;
            if ((numHits = col.Cast(moveBy.normalized, hits, moveBy.magnitude)) > 0)
            {

                if (numHits > maxSize)
                    numHits = maxSize;

                for (int i = 0; i < numHits; i++)
                {
                    if (hits[i].collider.isTrigger)
                        addTrigger(hits[i].collider);
                    else
                        addCollider(hits[i].collider);
                }

                translate(moveBy);

            }

            Physics2D.queriesHitTriggers = false;
        }
    }

    private void checkAndHandleDiscreteCollision()
    {
        
        bool stuck = true;

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(layerMask);

        int maxSize = 20;
        Collider2D[] colliders = new Collider2D[maxSize];
        int numHits;

        int loops = 0;
        while (stuck)
        {

            stuck = false;
            if ((numHits = col.OverlapCollider(filter, colliders)) > 0)
            {

                if (numHits > maxSize)
                    numHits = maxSize;

                for (int i = 0; i < numHits; i++)
                {

                    ColliderDistance2D dist = col.Distance(colliders[i]);

                    if (dist.distance < -totalCollisionOffset)
                    {
                        
                        stuck = true;

                        translate(dist.distance * dist.normal);
                    }

                }
            }

            loops++;
            if (loops > 10)
            {
                Debug.LogError("checkAndHandleDiscreteCollision(): Possible Infinite Loop. Exiting");
                break;
            }
        }

    }

    private void MoveDiscrete(Vector2 moveBy)
    {
        translate(moveBy);
        checkAndHandleDiscreteCollision();
    }

    public void Move(Vector2 moveBy)
    {
        switch (collisionType) {
            case (CollisionType.CONTINUOUS):
                MoveContinuous(moveBy);
                sendCollisionMessages(false);
                break;
            case (CollisionType.DISCRETE) :
                MoveDiscrete(moveBy);
                sendCollisionMessages(true);
                break;
            default :
                translate(moveBy);
                break;
        }

        moveCalled = true;

    }

    private void addCollider(Collider2D collider)
    {
        if (!collidersStayed.Contains(collider))
        {
            collidersEntered.Add(collider);
        }
    }

    private void addTrigger(Collider2D collider)
    {
        if (!triggersStayed.Contains(collider))
        {
            triggersEntered.Add(collider);
        }
    }

    private void sendCollisionMessages(bool checkForAdditions)
    {
        Physics2D.queriesHitTriggers = true;

        int maxSize = 20;
        Collider2D[] hits = new Collider2D[maxSize];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(layerMask);
        filter.useTriggers = true;

        int numHits = col.OverlapCollider(filter, hits);

        Physics2D.queriesHitTriggers = false;

        if (numHits >= maxSize)
            numHits = maxSize;


        if (checkForAdditions)
        {
            for (int i = 0; i < numHits; i++)
            {
                if (hits[i].isTrigger)
                    addTrigger(hits[i]);
                else
                    addCollider(hits[i]);
            }
        }


        foreach (Collider2D collider in collidersEntered)
        {
            SendMessage("OnObjectCollisionEnter", collider, SendMessageOptions.DontRequireReceiver);
            collidersStayed.Add(collider);
        }
        collidersEntered.Clear();

        foreach (Collider2D trigger in triggersEntered)
        {
            SendMessage("OnObjectTriggerEnter", trigger, SendMessageOptions.DontRequireReceiver);
            triggersStayed.Add(trigger);
        }
        triggersEntered.Clear();


        foreach (Collider2D collider in collidersStayed)
        {
            if (!hits.Contains(collider))
            {
                collidersExited.Add(collider);
            }
            else
                SendMessage("OnObjectCollisionStay", collider, SendMessageOptions.DontRequireReceiver);
        }

        foreach (Collider2D trigger in triggersStayed)
        {
            if (!hits.Contains(trigger))
            {
                triggersExited.Add(trigger);
            }
            else
                SendMessage("OnObjectTriggerStay", trigger, SendMessageOptions.DontRequireReceiver);
        }


        foreach (Collider2D collider in collidersExited)
        {
            SendMessage("OnObjectCollisionExit", collider, SendMessageOptions.DontRequireReceiver);
            collidersStayed.Remove(collider);
        }
        collidersExited.Clear();

        foreach (Collider2D trigger in triggersExited)
        {
            SendMessage("OnObjectTriggerExit", trigger, SendMessageOptions.DontRequireReceiver);
            triggersStayed.Remove(trigger);
        }
        triggersExited.Clear();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!moveCalled && checkCollisionWhenIdle && collisionType != CollisionType.NONE)
        {
            checkAndHandleDiscreteCollision();
            sendCollisionMessages(true);
        }

        moveCalled = false;
    }
}
