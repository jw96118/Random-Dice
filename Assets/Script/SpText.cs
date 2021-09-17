using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpText : MonoBehaviour
{
    public Text spText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spText.text = InGameManager.Instance.sp.ToString();
    }
}
