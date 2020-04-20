using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    const string hp = "Health";
    const string maxHP = "MaxHealth";
    const string speed = "Speed";
    const string pos = "Position";
    const string rot = "Rotation";
    const string x = "X";
    const string y = "Y";
    const string z = "Z";
    const string w = "W";

    private void Awake()
    {
        if (!instance)
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool FindData(string key)
    {
        bool x = PlayerPrefs.HasKey(key);
        return x;
    }

    public void YeetAllData()
    {
        Debug.Log("Just Send It Boysssss");
        PlayerPrefs.DeleteAll();
    }

    public void WriteChanges() => PlayerPrefs.Save();

    public void SaveData(string key, int val) { PlayerPrefs.SetInt(key, val); WriteChanges(); }
    public void SaveData(string key, float val) { PlayerPrefs.SetFloat(key, val); WriteChanges(); }
    public void SaveData(string key, string val)  { PlayerPrefs.SetString(key, val); WriteChanges(); }

    public int LoadInt(string key) { return PlayerPrefs.GetInt(key); }
    public float LoadFloat(string key) { return PlayerPrefs.GetFloat(key); }
    public string LoadString(string key) { return PlayerPrefs.GetString(key); }

    public void SaveEntity(Entity.EntityDataStorage entity)
    {
        string key = entity.GetKey();
        SaveData(key, entity.GetKey());
        SaveVector3(key + pos, entity.position);
        SaveQuaternion(key + rot, entity.rotation);
        SaveData(key + hp, entity.health);
        SaveData(key + speed, entity.speed);
        SaveData(key + maxHP, entity.maxHealth);
        WriteChanges();
    }
    public void DeleteEntity(Entity.EntityDataStorage entity)
    {
        string key = entity.GetKey();
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.DeleteKey(key + pos);
        PlayerPrefs.DeleteKey(key + rot);
        PlayerPrefs.DeleteKey(key + hp);
        PlayerPrefs.DeleteKey(key + speed);
        PlayerPrefs.DeleteKey(key + maxHP);
        WriteChanges();
    }
    public void LoadEntity(ref Entity.EntityDataStorage entity)
    {
        string key = entity.GetKey();
        entity.position = LoadVector3(key + pos);
        entity.rotation = LoadQuaternion(key + rot);
        entity.health = LoadInt(key + hp);
        entity.speed = LoadFloat(key + speed);
        entity.maxHealth = LoadInt(key + maxHP);
    }

    public void SaveVector3(string key, Vector3 vec)
    {
        SaveData(key + x, vec.x);
        SaveData(key + y, vec.y);
        SaveData(key + z, vec.z);
        WriteChanges();
    }

    public Vector3 LoadVector3(string key)
    {
        Vector3 vec;
        vec.x = LoadFloat(key + x);
        vec.y = LoadFloat(key + y);
        vec.z = LoadFloat(key + z);
        return vec;
    }

    public void SaveQuaternion(string key, Quaternion vec)
    {
        SaveData(key + x, vec.x);
        SaveData(key + y, vec.y);
        SaveData(key + z, vec.z);
        SaveData(key + w, vec.w);
        WriteChanges();
    }

    public Quaternion LoadQuaternion(string key)
    {
        Quaternion vec;
        vec.x = LoadFloat(key + x);
        vec.y = LoadFloat(key + y);
        vec.z = LoadFloat(key + z);
        vec.w = LoadFloat(key + w);
        return vec;
    }
}
