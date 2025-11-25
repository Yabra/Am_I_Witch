using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public bool usable;

    private Camera mainCamera;
    [SerializeField] private GameObject owner;
    [SerializeField] private float ownerDistance;
    [SerializeField] private float attackDistance;

    [SerializeField] private float useTime;
    private float useTimer;

    [SerializeField] private Quaternion currentAngle;
    [SerializeField] private Vector2 currentDestination;
    [SerializeField] private float summonDistance;
    [SerializeField] private GameObject summonObject;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (owner == null)
        {
            return;
        }

        useTimer += Time.deltaTime;
        if (owner.GetComponent<Player>() != null)
        {
            RotateTo(mainCamera.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetMouseButtonDown(0))
            {
                Use();
            }
        }

        else if (owner != null)
        {
            var player = FindAnyObjectByType<Player>();
            RotateTo(player.transform.position);

            var hit = Physics2D.Raycast(
                transform.position,
                (player.transform.position - transform.position).normalized,
                attackDistance
                );
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Player>() != null)
            {
                Use();
            }
        }

    }

    protected void RotateTo(Vector3 to)
    {
        transform.position = owner.transform.position;

        var direction = to - owner.transform.position;
        currentDestination = direction.normalized;
        float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90;

        currentAngle = Quaternion.Euler(0, 0, angle);
        transform.rotation = currentAngle;
        transform.Translate(Vector3.up * ownerDistance);
    }

    public void Use()
    {
        if (useTimer < useTime)
        {
            return;
        }

        useTimer = 0;
        var newBullet = Instantiate(summonObject);
        newBullet.transform.position = this.transform.position;
        newBullet.transform.rotation = currentAngle;
        newBullet.transform.Translate(Vector2.up * summonDistance);

        Bullet b = newBullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.SetDestination(currentDestination);
        }
    }
}
