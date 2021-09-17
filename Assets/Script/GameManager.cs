using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    public float gameWidth = 1440f;
    public float gameHeight = 2560f;
    private void Awake()
    {
        if (null != Instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        Screen.SetResolution(600, 900, false);
        //SetResolution();

        DontDestroyOnLoad(this);
    }
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            else
                return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetResolution()
    {
        int setWidth = 1440;
        int setHeight = 2560;

        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), false);

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight);
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        }
        else
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        }
    }

}
