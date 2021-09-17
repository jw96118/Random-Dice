using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    List<Transform> points;
    [SerializeField] MonsterFontPool fontPool;
    int pointIndex = 0;
    float speed;
    int hp;
    bool enemyFlag;
    // Start is called before the first frame update
    private void Awake()
    {
        pointIndex = 0;
        speed = 1f;
        hp = 50;
     
    }
    private void OnEnable()
    {
        pointIndex = 0;
        speed = 1f;
        hp = 50;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            ObjPool.Instance.PushObject(this.gameObject, OBJ_TYPE.MONSTER);
            InGameManager.Instance.sp += 30;
        }
        else
        {
           gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = hp.ToString();
        }
        Vector3 vDir = points[pointIndex + 1].position - transform.position;
        if (vDir.magnitude < 0.01f)
        {
            pointIndex++;
            if (pointIndex == 3)
            {
                ObjPool.Instance.PushObject(this.gameObject,OBJ_TYPE.MONSTER);
            }
        }
        transform.position += vDir.normalized * Time.deltaTime * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.EnemyFlag == enemyFlag)
            {
                hp -= bullet.Damage;
                ObjPool.Instance.PushObject(bullet.gameObject, OBJ_TYPE.BULLET);
                fontPool.PullObject(bullet.Damage);
            }
        }
    }
    public void SetMonster(List<Transform> points, bool enemyFlag)
    {
        this.points = points;
        this.enemyFlag = enemyFlag;
        transform.position = points[pointIndex].position;
    }
}
