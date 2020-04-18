using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour, IDamageable
{
    private int health = 3;
    protected int speed = 2;
    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void KillEntity()
    {
        Debug.Log("This is where the entity would die if you weren't dumb" + this.name);
        Destroy(this);
    }

    protected virtual void Attack() { }
    protected virtual void Move(Vector3 dir) { }
    public void TakeDamage(int val) => Health -= val;

    protected int Health { get => health; set { health = value; if (health <= 0) { KillEntity(); } } }
}
