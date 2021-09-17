using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OBJ_TYPE
{
    BULLET = 0,
    MONSTER,
    BOSS_MONSTER,
    END
}
public class ObjPool : MonoBehaviour
{
    static ObjPool instance = null;

    List<Queue<GameObject>> objList;
    [SerializeField] List<GameObject> prefabList = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (null != Instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        objList = new List<Queue<GameObject>>();
        for (int i = 0; i < (int)OBJ_TYPE.END; i++)
        {
            objList.Add(new Queue<GameObject>());
            GameObject temp;
            for (int j = 0; j <= 100; j++)
            {
                temp = Instantiate(prefabList[i], transform);
                temp.SetActive(false);

                objList[i].Enqueue(temp);
            }
        }
    }

    // Update is called once per frame

    public static ObjPool Instance
    {
        get
        {
            if (null == instance)
                return null;
            else
                return instance;
        }
    }
    public GameObject PullObject(OBJ_TYPE type)
    {
        if (objList[(int)type].Count == 0)
        {
            GameObject temp;
            for (int i = 0; i <= 20; i++)
            {
                temp = Instantiate(prefabList[(int)type], transform);
                temp.SetActive(false);

                objList[(int)type].Enqueue(temp);
            }
        }

        GameObject obj = objList[(int)type].Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void PushObject(GameObject obj, OBJ_TYPE type)
    {
        obj.SetActive(false);
        objList[(int)type].Enqueue(obj);
    }
}
