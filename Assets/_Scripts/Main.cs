using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;               //объект одиночка

    [Header("Через инспектор")]
    public GameObject[] prefabEnemies;              //массив шаблонов Enemy
    public float enemySpawnperSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;        // отступ для позиционирования

    private BoundsCheck bndCheck;


    private void Awake()
    {
        S = this;

        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnperSecond);
    }

    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length); //выбираем случайным обзраом enemy  для создания
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        float enemyPadding = enemyDefaultPadding; // размщаем врага над экраном в случайной позиции x
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnperSecond);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart() {
        SceneManager.LoadScene(0); 
    }
}
