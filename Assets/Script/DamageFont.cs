using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageFont : MonoBehaviour
{
    Text text;
    float y;
    float alpha;
    float speed;
    MonsterFontPool fontPool;
    private void Awake()
    {
        text = GetComponent<Text>();
        fontPool = transform.parent.GetComponent<MonsterFontPool>();
        speed = 3f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        y = 0;
        alpha = 1f;
        transform.position = new Vector3(0, 0, 0);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (text.color.a <= 0)
        {

        }
        y += Time.deltaTime * speed * 10f;
        alpha -= Time.deltaTime * speed;
        transform.localPosition = new Vector3(0, y, 0);
        text.color =  new Color(1,1,1,alpha);
    }
}
