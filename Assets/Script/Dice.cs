using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Realtime;
public enum DiceType
{
    NON = -1,
    FIRE = 0,
    ELECTRO,
    WIND,
    POSION,
    IRON,
    END
}

public class Dice : Photon.MonoBehaviour
{
    public DiceType diceType;
    public int number;
    public bool returnFlag;
    public bool mixFlag;
    public bool moving;
    public int index;
    float speed;
    Vector3 startPosition;
    Dice mixTarget;
    float time = 0;
    Color diceColor;
    
    PhotonView pv;

    void Awake()
    {
        diceType = DiceType.NON;
        number = -1;
        speed = 4f;
        returnFlag = false;
        moving = false;
        //startPosition = transform.position;
        diceColor = new Color(0, 0, 0);
        pv = GetComponent<PhotonView>();
    }
    public bool Mine
    {
        get { return pv.isMine; }
    }
    public Vector3 StartPosition
    {
        set { startPosition = value; }
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (diceType != DiceType.NON && !moving)
        {
            time += Time.deltaTime;
            if (time >= 0.3f)
            {
                time = 0f;
                bool enemyFlag = false;
                if (InGameManager.Instance.IsMine != pv.isMine)
                    enemyFlag = true;
                GameObject Target;
                if (!enemyFlag)
                    Target = MonsterManager.Instance.GetPlayerTarget(transform.position);
                else
                    Target = MonsterManager.Instance.GetEnemyTarget(transform.position);

                if (Target != null)
                {
                    Bullet bullet = ObjPool.Instance.PullObject(OBJ_TYPE.BULLET).GetComponent<Bullet>();
                    bullet.SetBullet(3f, InGameManager.Instance.TypePower[diceType] + number, transform.position ,Target, diceColor, enemyFlag);
                }
            }
        }


        if (returnFlag)
        {
            transform.position = Vector2.Lerp(transform.position, startPosition, Time.deltaTime * speed);
            if (transform.position == startPosition)
            {
                returnFlag = false;
                mixFlag = false;
                moving = false;
            }
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dice")
        {
            mixTarget = collision.gameObject.GetComponent<Dice>();
            if ((mixTarget.number == number) && (mixTarget.diceType == diceType))
            {
                mixFlag = true;
            }
        }

    }

    public void Enable()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Disable()
    {
        number = -1;
        diceType = DiceType.NON;
        mixFlag = false;
        returnFlag = false;
        transform.position = startPosition;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Mix()
    {
        if (number < 6)
        {
            if (mixTarget != null)
            {
                if (mixFlag && (mixTarget.number == number) && (mixTarget.diceType == diceType))
                {
                    int type = Random.Range(0, 5);
                    mixTarget.number++;
                    mixTarget.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = mixTarget.number.ToString();
                    mixTarget.diceType = (DiceType)type;
                    mixTarget.DiceColor();
                    mixTarget.mixFlag = false;
                    InGameManager.Instance.DiceDataManager.diceCount--;                    
                    Disable();
                    pv.RPC("MixChangeDice", PhotonTargets.Others, index, mixTarget.index, type);
                    mixTarget = null;
                }
            }
        }
    }
    [PunRPC]
    void MixChangeDice(int index,int mixTargetIndex,int type)
    {
        Dice dice = InGameManager.Instance.DiceDataManager.enemyDices[index];
        Dice targetDice = InGameManager.Instance.DiceDataManager.enemyDices[mixTargetIndex];
        targetDice.number++;
        targetDice.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = targetDice.number.ToString();
        targetDice.diceType = (DiceType)type;
        targetDice.DiceColor();
        dice.Disable();
    }
    public void DiceColor()
    {
        diceColor = new Color(0, 0, 0);
        switch (diceType)
        {
            case DiceType.FIRE:
                diceColor = new Color(1, 0, 0);
                break;
            case DiceType.ELECTRO:
                diceColor = new Color(1, 1, 0);
                break;
            case DiceType.WIND:
                diceColor = new Color(0, 1, 0);
                break;
            case DiceType.POSION:
                diceColor = new Color(0.5f, 0, 1);
                break;
            case DiceType.IRON:
                diceColor = new Color(1, 1, 1);
                break;
        }
        gameObject.GetComponent<SpriteRenderer>().color = diceColor;
    }
    [PunRPC]
    void SetDice(int type)
    {
        diceType = (DiceType)type;
        number = 1;
        Enable();
        gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = number.ToString();
        DiceColor();
    }
    public void Create(int type)
    {
        pv.RPC("SetDice", PhotonTargets.Others,type);
    }
}
