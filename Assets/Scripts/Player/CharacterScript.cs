using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

    [Header("Character")]
    private Transform child;
    private Rigidbody2D myRigidbody;
    private int direction;
    public float velocity = 8f;
    private float currentVelocity;
    public float jumpPower = 10f;
    private bool DoubleJump = true;

    [Header("Ground detection")]
    private bool _IsGrounded;
    LayerMask Ground;
    public float width;
    public float heigth;
    private float RayLength = 0.1f;
    Vector3 rightOrigin;
    Vector3 leftOrigin;

    [Header("Animation")]
    private Animator myAnim;


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        child = transform.GetChild(0);
        Ground = (1 << LayerMask.NameToLayer("Ground"));
        Ground |= (1 << LayerMask.NameToLayer("DynamicObject"));
    }
	
	// Update is called once per frame
	void Update () {

        direction = 0;
        currentVelocity = Input.GetAxisRaw("Horizontal");

        if (currentVelocity < 0)
        {
            direction = -1;
            Flip();
            Move();
        }
        else if (currentVelocity > 0)
        {
            direction = 1;
            Flip();
            Move();
        }

        IsGrounded();
        if (_IsGrounded && Input.GetButtonDown("Jump"))
            Jump();
        else if ((!_IsGrounded && DoubleJump) && Input.GetButtonDown("Jump"))
        {
            DoubleJump = false;
            Jump();
        }
	}

    void LateUpdate()
    {
        myAnim.SetBool("IsGrounded", _IsGrounded);
        myAnim.SetBool("IsMoving", currentVelocity != 0);
    }

    private void Move()
    {
        myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * velocity, myRigidbody.velocity.y);
    }

    private void Jump()
    {
        myRigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
    }

    private void IsGrounded()
    {
        rightOrigin = transform.position + new Vector3(width, -heigth/2,0);
        leftOrigin = transform.position + new Vector3(-width, -heigth/2,0);

        RaycastHit2D rightRay = Physics2D.Raycast(rightOrigin, -Vector3.up, RayLength,Ground);
        RaycastHit2D leftRay = Physics2D.Raycast(leftOrigin, -Vector3.up, RayLength,Ground);

        Debug.DrawLine(rightOrigin, rightOrigin + -Vector3.up * RayLength, Color.red);
        Debug.DrawLine(leftOrigin, leftOrigin + -Vector3.up * RayLength, Color.red);

        _IsGrounded = rightRay.collider != null || leftRay.collider != null;

        if (_IsGrounded)
            DoubleJump = true;
    }

    private void Flip()
    {
        if (direction < 0)
        {
            child.localScale = new Vector3(-1 * Mathf.Abs(child.localScale.x), child.localScale.y, 1);

        }
        else if (direction > 0)
        {
            child.localScale = new Vector3(Mathf.Abs(child.localScale.x), child.localScale.y, 1);
        }
    }

    public void Die()
    {
        Respawn();
    }

    private void Respawn()
    {
        transform.position = GameManager.GetGemeManager().GetSpawnerPosition();
    }
}
