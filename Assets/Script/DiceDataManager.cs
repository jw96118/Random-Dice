using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DiceDataManager : MonoBehaviour
{
    public List<Dice> playerDices;
    public List<Dice> enemyDices;
    public int diceCount = 0;

    void Awake()
    {
        playerDices = new List<Dice>();
        enemyDices = new List<Dice>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Create()
    {
        if (diceCount < 15)
            while (true)
            {
                int index = Random.Range(0, 15);
                if (playerDices[index].diceType == DiceType.NON)
                {
                    int type = Random.Range(0, 5);
                    //playerDices[index].Create(type);
                    playerDices[index].diceType = (DiceType)type;
                    playerDices[index].number = 1;
                    playerDices[index].Enable();
                    playerDices[index].gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = playerDices[index].number.ToString();
                    playerDices[index].DiceColor();

                    enemyDices[index].Create(type);
                    //enemyDices[index].diceType = (DiceType)Random.Range(0, 5);
                    //enemyDices[index].number = 1;
                    //enemyDices[index].Enable();
                    //enemyDices[index].gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = playerDices[index].number.ToString();
                    //enemyDices[index].DiceColor();
                    diceCount++;
                    break;
                }
            }
    }
}
