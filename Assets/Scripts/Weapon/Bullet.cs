using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] Vector2 destination;
    [SerializeField] float speed;
    [SerializeField] int damage;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.velocity = destination;
    }

    public void SetDestination(Vector2 destination)
    {
        this.destination = destination.normalized * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var entity = collision.gameObject.GetComponent<Entity>();

        if (entity != null)
        {
            entity.Damage(damage);
        }

        Destroy(this.gameObject);
    }
}
