using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.transform.localPosition = new Vector3(0, 0f, 0);
        gameObject.transform.localScale = new Vector2((10f / GameManager.Instance.gameWidth), (10f / GameManager.Instance.gameWidth));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
