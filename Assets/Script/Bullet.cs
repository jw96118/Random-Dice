using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed;
    int damage;
    GameObject Target;
    SpriteRenderer spriteRenderer;
    Vector3 vDIr;
    float time;
    bool enemyFlag;
    public int Damage
    {
        get { return damage; }
    }
    public bool EnemyFlag
    {
        get { return enemyFlag; }
    }
    void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 15f)
            ObjPool.Instance.PushObject(gameObject, OBJ_TYPE.BULLET);
        if (Target.activeSelf)
            vDIr = Target.transform.position - transform.position;

        transform.position += vDIr.normalized * Time.deltaTime * speed;
    }

    public void SetBullet(float speed, int damage, Vector3 pos , GameObject Target, Color color, bool enemyFlag)
    {
        this.speed = speed;
        this.damage = damage;
        this.Target = Target;
        this.enemyFlag = enemyFlag;
        transform.position = pos;
        spriteRenderer.color = color;
        vDIr = Target.transform.position - transform.position;
        time = 0f;
    }
}
