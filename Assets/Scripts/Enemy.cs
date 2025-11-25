using UnityEngine;

public enum EnemyState { Patrol, Chase, Attack, Return }

public class Enemy : MonoBehaviour
{
    private Animator _anim;

    [Header("State Settings")]
    public EnemyState currentState = EnemyState.Patrol;

    [Header("Movement Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackRange = 1.5f;
    public float chaseRange = 6f;
    public float returnRange = 10f;

    [Header("Patrol Points")]
    public Transform[] patrolPoints;

    [Header("Components")]
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 startPosition;
    private int currentPatrolIndex = 0;
    private bool hasPatrolPoints = false;

    void Start()
    {
        _anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindAnyObjectByType<Player>().transform;
        startPosition = transform.position;
        hasPatrolPoints = patrolPoints != null && patrolPoints.Length > 0;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Patrol:
                PatrolBehavior();
                if (distanceToPlayer <= chaseRange)
                    currentState = EnemyState.Chase;
                break;

            case EnemyState.Chase:
                ChaseBehavior();
                if (distanceToPlayer <= attackRange)
                    currentState = EnemyState.Attack;
                else if (distanceToPlayer > returnRange)
                    currentState = EnemyState.Return;
                break;

            case EnemyState.Attack:
                AttackBehavior();
                if (distanceToPlayer > attackRange && distanceToPlayer <= chaseRange)
                    currentState = EnemyState.Chase;
                else if (distanceToPlayer > chaseRange)
                    currentState = EnemyState.Patrol;
                break;

            case EnemyState.Return:
                ReturnToStart();
                if (Vector2.Distance(transform.position, startPosition) < 0.5f)
                    currentState = EnemyState.Patrol;
                else if (distanceToPlayer <= chaseRange)
                    currentState = EnemyState.Chase;
                break;
        }
    }

    void PatrolBehavior()
    {
        _anim.SetBool("isMoving", true);
        if (!hasPatrolPoints)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 targetPosition = patrolPoints[currentPatrolIndex].position;
        MoveToPosition(targetPosition, patrolSpeed);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChaseBehavior()
    {
        _anim.SetBool("isMoving", true);
        MoveToPosition(player.position, chaseSpeed);
    }

    void AttackBehavior()
    {
        _anim.SetBool("isMoving", false);
        rb.velocity = Vector2.zero;
    }

    void ReturnToStart()
    {
        _anim.SetBool("isMoving", true);
        MoveToPosition(startPosition, patrolSpeed);
    }

    void MoveToPosition(Vector2 targetPosition, float speed)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed;

        spriteRenderer.flipX = direction.x < 0;
    }
}