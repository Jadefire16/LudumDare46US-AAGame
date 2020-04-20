using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour, IDamageable
{
    [Header("DefaultData")]
    public EntityData data;
    EntityDataStorage dataStorage;
    [Space]
    [Space]
    public LayerMask groundLayer, waterLayer;
    [Space]
    [Space]
    private int health;
    protected int maxHealth;
    protected float speed;
    protected Rigidbody rb;

    private bool isAlive = true;
    public bool IsAlive { get => isAlive; }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        dataStorage = new EntityDataStorage();
        LoadEntityData();
        EventManager.instance.OnSaveGameEvent += SaveEntityData;
    }

    protected virtual void SaveEntityData()
    {
        SyncDataToEntity();
        SaveManager.instance.SaveEntity(dataStorage);
    }

    protected virtual void DeleteEntityData()
    {
        SaveManager.instance.DeleteEntity(dataStorage);
    }

    protected virtual void LoadEntityData()
    {
        bool x = SaveManager.instance.HasData(dataStorage.GetKey());
        if (x)
        {
            dataStorage.SetToEntityData(data);
        }
        else if(!x)
        {
            SaveManager.instance.LoadEntity(ref dataStorage);
        }
        SyncEntityToData();
        SaveManager.instance.SaveEntity(dataStorage);
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
        Destroy(this.gameObject);
    } // To use base class implementations in an overidden class you must use (in this functions case) base.KillEntity();

    protected virtual void Attack() { } // Used for enemies or players when they should attack
    protected virtual void Move() { } // Use this function to move enemies
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

    private void OnDestroy()
    {
        EventManager.instance.OnSaveGameEvent -= SaveEntityData;
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

        public EntityDataStorage()
        {
            ID = 999;
            this.eType = EntityType.Flicker;
            this.name = "x";
            this.health = 1;
            this.maxHealth = 2;
            this.speed = 0.7f;
            this.position = Vector3.zero;
            this.rotation = Quaternion.identity;
        }

        public EntityDataStorage(int iD, EntityType eType, string name, int health, int maxHealth, float speed, Vector3 pos, Quaternion rot)
        {
            SetDataToValue(ID, eType, name, health, maxHealth, speed, pos, rot);
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
            key = name + eType + ID;
            return key;
        }
    }
}


