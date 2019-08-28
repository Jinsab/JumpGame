using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rigid;
    public Text stage, score;
    public Image oneStar, twoStar, threeStar;
    public float runSpeed = 40f;
    public int life = 1;
    public GameObject clear_box;

    private Collider2D bxCollider, crCollider;
    private bool over = true;
    private new Renderer renderer;
    ScoreManager Manager;

    static int[,] star_score = new int[2, 3] { { 0, 25, 50 }, { 0, 10, 20 } };
    float unbeatableTime = 0f;
    float deadTime = 0f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {
        animator.SetInteger("Life", life);
        bxCollider = gameObject.GetComponent<BoxCollider2D>();
        crCollider = gameObject.GetComponent<CircleCollider2D>();
        renderer = gameObject.GetComponent<Renderer>();
        Manager = gameObject.GetComponent<ScoreManager>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        animator.SetInteger("Life", life);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (life <= 0)
        {
            if (over)
            {
                over = false;

                //message.text = "GameOver!" + "\n" + "Press 'R' 3 Second Restart!";

                bxCollider.isTrigger = true;
                crCollider.isTrigger = true;

                rigid.AddForce(new Vector2(0f, 50f));
                rigid.gravityScale = 0;

                deadTime = Time.time;
            }

            if (Time.time - deadTime > 2)
            {
                rigid.gravityScale = 3;
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        BlockStatus block = collision.gameObject.GetComponent<BlockStatus>();

        if (collision.CompareTag("Util Block") && (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.UpArrow)))
        {
            if (block.type == "Portal Enter")
            {
                Vector3 anotherProtalPos = block.portal.transform.position;
                gameObject.transform.position = anotherProtalPos;
            }
            else if (block.type == "Clear Door")
            {
                collision.enabled = false;

                clear_box.SetActive(true);

                score.text = "Score : " + Manager.GetScore();

                if (GameManagement.stageLevel < 10)
                {
                    stage.text = "Stage 0" + GameManagement.stageLevel+1;
                }
                else
                {
                    stage.text = "Stage " + GameManagement.stageLevel+1;
                }

                if (star_score[GameManagement.stageLevel, 0] < Manager.GetScore())
                {
                    oneStar.color = new Color(255, 255, 0, 1f);

                    if (star_score[GameManagement.stageLevel, 1] < Manager.GetScore())
                    {
                        twoStar.color = new Color(255, 255, 0, 1f);

                        if (star_score[GameManagement.stageLevel, 2] < Manager.GetScore())
                            threeStar.color = new Color(255, 255, 0, 1f);
                    }
                }

            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        BlockStatus block = collision.gameObject.GetComponent<BlockStatus>();
         
        if (collision.collider.CompareTag("Util Block") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            if (block.type == "Up")
            {
                Vector2 upVelocity = new Vector2(0, block.value);
                //collision.collider.attachedRigidbody.velocity = upVelocity;
                rigid.velocity = upVelocity;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Monster") && !collision.collider.isTrigger && rigid.velocity.y < 0f) { // 몬스터를 정확하게 밟았을 때

            MonsterMovement monster = collision.collider.gameObject.GetComponent<MonsterMovement>();
            monster.Die();

            Vector2 KillVelocity = new Vector2(0, 15f);
            rigid.AddForce(KillVelocity, ForceMode2D.Impulse);

            Manager.SetScore(monster.score);
        }
        else if (collision.collider.CompareTag("Monster") && Time.time - unbeatableTime > 2) // 몬스터에게 부딪히거나 밟지 못해 피격한 경우
        {
            life--;

            Light();

            unbeatableTime = Time.time;
        }

        /*
        if (collision.collider.CompareTag("Monster") && Time.time - unbeatableTime > 2)
        {
            life--;

            Light();

            unbeatableTime = Time.time;
        }
        */
    }

    void Light ()
    {
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);

        StartCoroutine(WaitForLight());
    }

    IEnumerator WaitForLight()
    {
        yield return new WaitForSeconds(2f);

        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
    }
}
