using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public int creatureType = 0;
    public int score;
    public float movePower = 1f;
    GameObject traceTarget;
    Animator animator;
    Vector3 movement;
    int movementFlag = 0;
    bool isTracing = false;
    bool isDie = false;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine("ChangeMovement");
    }

    private void FixedUpdate()
    {
        Move ();
    }

    void Update()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(5, 5, 1);
        }
        else if(movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-5, 5, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);

        yield return new WaitForSeconds(1f);

        StartCoroutine("ChangeMovement");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (creatureType == 0)
            return;

        if (collision.gameObject.tag == "Player")
        {
            traceTarget = collision.gameObject;

            StopCoroutine("ChangeMovement");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (creatureType == 0)
            return;

        if (collision.gameObject.tag == "Player")
        {
            isTracing = true;
            animator.SetBool("isMoving", true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (creatureType == 0)
            return;

        if (collision.gameObject.tag == "Player")
        {
            isTracing = false;

            StartCoroutine("ChangeMovement");
        }
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";

        if (isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Left";
            else if (playerPos.x > transform.position.x)
                dist = "Right";
        }
        else
        {
            if (movementFlag == 1)
                dist = "Left";
            else if (movementFlag == 2)
                dist = "Right";
        }

        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(5, 5, 1);
        }
        else if (dist == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-5, 5, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    public void Die()
    {
        StopCoroutine("ChangeMovement");
        isDie = true;

        SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        renderer.flipY = true;

        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = false;

        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        Vector2 dieVelocity = new Vector2(0, 40f);

        Destroy(gameObject, 5f);
    }
}
