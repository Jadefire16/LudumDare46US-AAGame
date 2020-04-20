using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour, IDamageable
{
    [Header("DefaultData")]
    public EntityData data;
    [Space]
    [Header("Entity Data Storage")]
    public EntityDataStorage dataStorage;
    [Space]
    [Space]
    public LayerMask groundLayer;
    [Space]
    [Space]
    private int health = 3;
    protected int maxHealth = 10;
    protected float speed = 2;
    protected Rigidbody rb;
    private bool isAlive = true;

    public bool IsAlive { get => isAlive; }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        LoadEntityData();
        //EventManager.instance.OnSaveGameEvent += SaveEntity;
    }

    protected virtual void SaveEntityData()
    {
        SyncDataToEntity();
        SaveManager.instance.SaveEntity(dataStorage);
        Debug.Log("Saved Entity " + dataStorage.GetKey());
    }

    protected virtual void DeleteEntityData()
    {
        SaveManager.instance.DeleteEntity(dataStorage);
        Debug.Log("Deleted Entity " + dataStorage.GetKey());
    }

    protected virtual void LoadEntityData()
    {
        if (SaveManager.instance.FindData(dataStorage.GetKey()) == false)
        {
            dataStorage.SetToEntityData(data);
            SaveManager.instance.SaveEntity(dataStorage);
        }
        else
        {
            SaveManager.instance.LoadEntity(ref dataStorage);
            SyncEntityToData();
        }
        //SaveManager.instance.SaveEntity(dataStorage);
    }

    protected virtual void SyncDataToEntity()
    {
        dataStorage.SetDataToBaseValue(health, maxHealth, speed, transform.position, transform.rotation);
    }

    protected virtual void SyncEntityToData()
    {
        health = dataStorage.health;
        maxHealth = dataStorage.maxHealth;
        speed = dataStorage.speed;
        transform.position = dataStorage.position;
        transform.rotation = dataStorage.rotation;
    }

    private void LateUpdate()
    {
        WithinBoundsCheck();
    }
    protected virtual void KillEntity() // this yeets the Entity component, conviniently if you do this the entity stops moving
    {
        DeleteEntityData();
        Debug.Log("Deleted " + data.name);
        //Destroy(this);
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
                isAlive = false;
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

    [System.Serializable]
    public class EntityDataStorage
    {

        public int ID = 0;
        public EntityType eType;
        public string name;

        public int health;
        public int maxHealth;
        public float speed;
        public Vector3 position;
        public Quaternion rotation;

        public EntityDataStorage(int iD, EntityType eType, string name, int health, int maxHealth, float speed, Vector3 pos, Quaternion rot)
        {
            ID = iD;
            this.eType = eType;
            this.name = name;
            this.health = health;
            this.maxHealth = maxHealth;
            this.speed = speed;
            this.position = pos;
            this.rotation = rot;
        }

        public void SetDataToValue(int iD, EntityType eType, string name, int health, int maxHealth, float speed, Vector3 pos, Quaternion rot)
        {
            this.ID = iD;
            this.eType = eType;
            this.name = name;
            this.health = health;
            this.maxHealth = maxHealth;
            this.speed = speed;
            this.position = pos;
            this.rotation = rot;
        }

        public void SetDataToBaseValue(int health, int maxHealth, float speed, Vector3 pos, Quaternion rot)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.speed = speed;
            this.position = pos;
            this.rotation = rot;
        }

        public void SetToEntityData(EntityData data)
        {
            this.ID = data.ID;
            this.eType = data.eType;
            this.name = data.name;
            this.health = data.health;
            this.maxHealth = data.maxHealth;
            this.speed = data.speed;
            this.position = data.position;
            this.rotation = data.rotation;
        }

        public string GetKey()
        {
            string key;
            key = this.name + this.eType + this.ID;
            return key;
        }
    }
}


