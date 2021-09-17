using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    static MonsterManager instance = null;

    float time = 0f;
    List<Monster> playerMonsterList;
    List<Monster> enemyMonsterList;
    [SerializeField] List<Transform> playerPoints;
    [SerializeField] List<Transform> enemyPoints;

    private void Awake()
    {
        if (null != Instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        playerMonsterList = new List<Monster>();
        enemyMonsterList = new List<Monster>();
    }
    void Start()
    {
        StartCoroutine(SpawnNormalPlayerMonster());
        StartCoroutine(SpawnNormalEnemyMonster());
        //StartCoroutine(SpawnBossMonster());
    }

    // Update is called once per frame
    void Update()
    {

    }


    public static MonsterManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            else
                return instance;
        }
    }
    public GameObject GetPlayerTarget(Vector3 Pos)
    {
        if (playerMonsterList.Count == 0)
            return null;

        GameObject Target = null;
        float min = 10000;

        List<Monster> tmepList = new List<Monster>();

        foreach (Monster monster in playerMonsterList)
        {
            if (!monster.gameObject.activeSelf)
            {
                tmepList.Add(monster);
                continue;
            }
            Vector3 vLine = monster.transform.position - Pos;
            if (min > vLine.magnitude)
            {
                min = vLine.magnitude;
                Target = monster.gameObject;
            }
        }
        playerMonsterList.RemoveAll(tmepList.Contains);
        if (min > 3f)
            return null;
        else
            return Target;
    }
    public GameObject GetEnemyTarget(Vector3 Pos)
    {
        if (enemyMonsterList.Count == 0)
            return null;

        GameObject Target = null;
        float min = 10000;

        List<Monster> tmepList = new List<Monster>();

        foreach (Monster monster in enemyMonsterList)
        {
            if (!monster.gameObject.activeSelf)
            {
                tmepList.Add(monster);
                continue;
            }
            Vector3 vLine = monster.transform.position - Pos;
            if (min > vLine.magnitude)
            {
                min = vLine.magnitude;
                Target = monster.gameObject;
            }
        }
        enemyMonsterList.RemoveAll(tmepList.Contains);
        if (min > 3f)
            return null;
        else
            return Target;
    }
    IEnumerator SpawnNormalPlayerMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            Monster temp = ObjPool.Instance.PullObject(OBJ_TYPE.MONSTER).GetComponent<Monster>();
            temp.SetMonster(playerPoints,false);
            playerMonsterList.Add(temp);
        }
    }
    IEnumerator SpawnNormalEnemyMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            Monster temp = ObjPool.Instance.PullObject(OBJ_TYPE.MONSTER).GetComponent<Monster>();
            temp.SetMonster(enemyPoints,true);
            enemyMonsterList.Add(temp);
        }
    }
    IEnumerator SpawnBossMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);

            Monster temp = ObjPool.Instance.PullObject(OBJ_TYPE.BOSS_MONSTER).GetComponent<Monster>();
            temp.SetMonster(playerPoints,false);
            playerMonsterList.Add(temp);
        }
    }
}
