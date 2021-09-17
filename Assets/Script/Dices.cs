using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dices : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            Dice dice = gameObject.transform.GetChild(i).GetComponent<Dice>();
            dice.index = i;
            if (dice.Mine == InGameManager.Instance.IsMine)
            {
                dice.transform.position = InGameManager.Instance.PlayerDicePos.transform.GetChild(i).position;
                dice.StartPosition = dice.transform.position;                
                InGameManager.Instance.DiceDataManager.playerDices.Add(dice);
            }
            else
            {
                dice.transform.position = InGameManager.Instance.EnemyDicePos.transform.GetChild(i).position;
                dice.StartPosition = dice.transform.position;
                InGameManager.Instance.DiceDataManager.enemyDices.Add(dice);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
