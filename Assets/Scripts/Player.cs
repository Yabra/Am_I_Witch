using NaughtyAttributes;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    private Animator _anim;


    [Header("Basic Params")]
    [SerializeField] private float speed;


    [Header("Dash Params")]
    [SerializeField] private bool canDash;

    [SerializeField][ShowIf("canDash")][ReadOnly] private bool dash = false;
    [SerializeField][ShowIf("canDash")][ReadOnly] private float dashTimer;

    [SerializeField][ShowIf("canDash")] private float dashSpeed;
    [SerializeField][ShowIf("canDash")] private float dashDistance;
    [SerializeField][ShowIf("canDash")] private float dashRegenerationTime;
    [SerializeField][ShowIf("canDash")] private GameObject dashEffectPrefab;

    Vector2 dashStart;
    Vector2 dashDirection;

    void Start()
    {
        mainCamera = Camera.main;
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (mainCamera.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
        }

        _anim.SetBool("dash", dash);
        dashTimer += Time.deltaTime;

        if (canDash
            && dashTimer > dashRegenerationTime
            && Input.GetKeyDown(KeyCode.LeftShift)
            && (Input.GetAxis("X") != 0 || Input.GetAxis("Y") != 0))
        {
            dash = true;
            dashStart = _rb.position;
            dashDirection = new Vector2(Input.GetAxis("X"), Input.GetAxis("Y")).normalized * dashSpeed;

            var dashEffect = Instantiate(dashEffectPrefab, transform);
            dashEffect.transform.position = _rb.position;
        }

        if (dash)
        {
            if ((_rb.position - dashStart).magnitude >= dashDistance)
            {
                EndDash();
            }

            _rb.velocity = dashDirection;
        }

        else
        {
            _rb.velocity = new Vector2(Input.GetAxis("X"), Input.GetAxis("Y")).normalized * speed;
            _anim.SetBool("isMoving", _rb.velocity.magnitude != 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EndDash();
    }

    private void EndDash()
    {
        if (dash)
        {
            dash = false;
            dashTimer = 0;
        }
    }
}
