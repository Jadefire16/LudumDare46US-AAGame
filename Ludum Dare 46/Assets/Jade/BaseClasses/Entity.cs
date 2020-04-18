using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour, IDamageable
{
    private int health = 3;
    protected int maxHealth = 10;
    protected int speed = 2;
    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void LateUpdate()
    {
        WithinBoundsCheck();
    }
    protected virtual void KillEntity() // this yeets the Entity component, conviniently if you do this the player stops moving
    {
        Debug.Log("This is where the entity would die if you weren't dumb " + this.name);
        Destroy(this);
    } // To use base class implementations in an overidden class you must use (in this functions case) base.KillEntity();

    protected virtual void Attack() { } // Used for enemies or players when they should attack
    protected virtual void Move(Vector3 dir) { } // Use this function to move enemies
    public void TakeDamage(int val) => Health -= val; // Take Damage can be called by using Entity.TakeDamage(amount); even on derived classes
    protected int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                KillEntity();
            }
            else if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    } // use this and not the actual health variable
    void WithinBoundsCheck()
    {
        if (transform.position.y < GameManager.killLimitY) { Destroy(this.gameObject); }
    }
}