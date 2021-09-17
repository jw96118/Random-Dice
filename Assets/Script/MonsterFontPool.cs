using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterFontPool : MonoBehaviour
{
    Queue<Text> damageFontPool;
    [SerializeField] Text damageFont;
    void Awake()
    {
        damageFontPool = new Queue<Text>();
        Text temp;
        for (int j = 0; j <= 20; j++)
        {
            temp = Instantiate(damageFont, transform);
            temp.gameObject.SetActive(false);

            damageFontPool.Enqueue(temp);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public Text PullObject(int damage)
    {
        if (damageFontPool.Count == 0)
        {
            Text temp;
            for (int i = 0; i <= 10; i++)
            {
                temp = Instantiate(damageFont, transform);
                temp.gameObject.SetActive(false);

                damageFontPool.Enqueue(temp);
            }
        }

        Text obj = damageFontPool.Dequeue();
        obj.gameObject.SetActive(true);
        obj.text = damage.ToString();
        return obj;
    }
    public void PushObject(Text obj)
    {
        obj.gameObject.SetActive(false);
        damageFontPool.Enqueue(obj);
    }
}
