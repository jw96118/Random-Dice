using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeButton : MonoBehaviour
{
    public DiceType diceType;
    SpriteRenderer spriteRenderer;
    public int upgradeCost;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        upgradeCost = 50;
    }

    // Update is called once per frame
    void Update()
    {
        Color diceColor = new Color(0, 0, 0);
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


        gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = upgradeCost.ToString();

    }
}
