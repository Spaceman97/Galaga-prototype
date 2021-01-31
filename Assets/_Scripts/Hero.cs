using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S; // одиночка

    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 1f;

    public GameObject bulletHeroPrefabs;
    public float bulletSpeed = 30f;
    private float _shieldLevel = 1;
    


    public float shieldLevel {
        get {
            return (_shieldLevel);          //читает свойство и возвращает значение
        }
        set {
            _shieldLevel = Mathf.Min(value, 4);  // выше 4 не будет
            if (value < 0)
            {
                Destroy(this.gameObject);
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }

    private GameObject lastTriggerGo = null; //    ссылка на последний столкнувшийся игровой объект

    void Awake()
    {
        if (S == null)
        {
            S = this; // сохранить ссылку на одиночку 
        }
        else
            Debug.LogError("Hero.Awake  failed");
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        //Извлекаем информацию из инпута
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Изменить transform.position опираясь на инфу по осям
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
    }

    private void Fire()
    {
        GameObject bulletGO = Instantiate<GameObject>(bulletHeroPrefabs);
        bulletGO.transform.position = transform.position;
        Rigidbody rigidBullet = bulletGO.GetComponent<Rigidbody>();
        rigidBullet.velocity = Vector3.up * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print(go.name);

        if (go == lastTriggerGo) { return; }


        if (go.tag == "Enemy")   //если защ поле столкнулось с врагом

        {
            shieldLevel--;
            Destroy(go);
        }
        else {
            print(go.name);
        }
    }
}
