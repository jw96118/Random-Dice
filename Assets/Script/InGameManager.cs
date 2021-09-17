using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    static InGameManager instance = null;

    PhotonView pv;

    public int sp;
    [SerializeField] Dictionary<DiceType, int> typePower;
    [SerializeField] DiceDataManager diceDataManager;
    [SerializeField] GameObject playerDicePos;
    [SerializeField] GameObject enemyDicePos;
    public Dictionary<DiceType, int> TypePower
    {
        get { return typePower; }
    }
    public DiceDataManager DiceDataManager
    {
        get { return diceDataManager; }
    }
    public GameObject PlayerDicePos
    {
        get { return playerDicePos; }
    }
    public GameObject EnemyDicePos
    {
        get { return enemyDicePos; }
    }
    public bool IsMine
    {
        get { return pv.isMine; }
    }
    // Start is called before the first frame update
    private void Awake()
    {

        if (null != Instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        pv = GetComponent<PhotonView>();
        sp = 100;
        typePower = new Dictionary<DiceType, int>();
        for (int i = 0; i < (int)DiceType.END; i++)
        {
            typePower.Add((DiceType)i, 5);
        }
        PhotonNetwork.Instantiate("Prefab/Dices",new Vector3(0,0,0),Quaternion.identity,0);
        //PhotonNetwork.Instantiate("Prefab/TestObj",new Vector3(0,0,0),Quaternion.identity,0);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.tag == "UpGrade")
            {
                UpGradeButton temp = hit.collider.gameObject.GetComponent<UpGradeButton>();
                if(Upgrade(temp.diceType, temp.upgradeCost))
                    temp.upgradeCost += 50;
            }
            else if (hit.collider != null && hit.collider.tag == "CreateButton")
            {
                DiceDataManager.Create();
                sp -= 10;
            }
        }
    }


    public static InGameManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            else
                return instance;
        }
    }
    public bool Upgrade(DiceType type, int cost)
    {
        if (sp >= cost)
        {
            sp -= cost;
            typePower[type]++;
            return true;
        }
        return false;
    }
}
