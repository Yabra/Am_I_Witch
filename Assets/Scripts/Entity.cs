using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int MaxHealth { get => maxHealth; }
    [SerializeField] int health;
    public int Health { get => health; }
    public bool isDead { get => health <= 0; }
    [SerializeField] bool destroyIfdead;


    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (isDead && destroyIfdead)
        {
            Destroy(this.gameObject);
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
    }
}
