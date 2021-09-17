using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    bool movingDice = false;
    GameObject dice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!movingDice)
        {
            if (Input.GetMouseButtonDown(0))
            {           
                if (Input.GetMouseButton(0))
                {
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
                    Ray2D ray = new Ray2D(touchPos, Vector2.zero);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    if (hit.collider != null && hit.collider.tag == "Dice")
                    {
                        dice = hit.collider.gameObject;
                        movingDice = true;
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
                dice.transform.position = touchPos;
                Dice tempDice = dice.GetComponent<Dice>();
                tempDice.returnFlag = false;
                tempDice.moving = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                movingDice = false;
             
                Dice tempDice = dice.GetComponent<Dice>();
                tempDice.returnFlag = true;
                if (tempDice.mixFlag)
                    tempDice.Mix();
                dice = null;
            }
        }
    }
}
