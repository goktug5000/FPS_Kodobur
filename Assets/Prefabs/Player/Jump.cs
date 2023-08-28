using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Jump : MonoBehaviour
{

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpingPowerBase=5;
    [SerializeField] private float jumpingPowerNow;
    [SerializeField] private float jumpingPowerMulti=1;
    [SerializeField] public bool isGrounded;
    [SerializeField] private bool isJumping;

    private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    [SerializeField] private float jumpBufferCounter;


    [Header("Keyboard")]
    [SerializeField] private KeyCode KeyCode_Jump;

    [Header("Adýrs")]
    private Rigidbody rb;

    [Header("SFX")]
    [SerializeField] private GameObject SFX_Jump;
    private GameObject _sfx;
    // Start is called before the first frame update


    public void setLvlJumpPow(int lvl)
    {
        jumpingPowerNow = jumpingPowerBase + (lvl * jumpingPowerMulti);
    }
    void Start()
    {
        if (KeyCode_Jump == KeyCode.None)
        {
            KeyCode_Jump = KeyCode.Space;
        }

        jumpingPowerNow = jumpingPowerBase;


        if (rb == null)
        {

            try
            {
                rb = this.gameObject.GetComponent<Rigidbody>();
            }
            catch
            {
                Debug.Log("Rigidbody koymamýþsýn");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if ((Input.GetKeyDown(KeyCode_Jump)))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            StartCoroutine(JumpCooldown());
            rb.velocity = new Vector2(rb.velocity.x, jumpingPowerNow);
            StartCoroutine(playSFX(SFX_Jump));
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0;
            isGrounded = false;

        }
        //for short jump (when keyUp)
        if ((Input.GetKeyUp(KeyCode_Jump) && rb.velocity.y > 0f))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "CamLocation")
        {
            isGrounded = true;
        }

    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag != "CamLocation")
        {
            isGrounded = true;
        }

    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag != "CamLocation")
        {
            isGrounded = false;
        }
    }
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
    // Update is called once per frame
    IEnumerator playSFX(GameObject SFXX)
    {
        try
        {
            _sfx = Instantiate(SFXX);
        }
        catch
        {
            yield break;
        }
        yield return new WaitForSeconds(10);

        Destroy(_sfx);
        yield break;
    }




    public float gravityScale = 3.0f;
    public static float globalGravity = -9.81f;


    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
