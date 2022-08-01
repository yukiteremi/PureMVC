using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FPSItemView : MonoBehaviour
{
    public UnityAction NoClickEvent = null;
    public UnityAction<int> Click = null;

    public bool IsExist = true;
     Button clickBtn;
    public Button ClickBtn
    {
        get
        {
            if (clickBtn == null)
            {
                clickBtn = transform.GetComponent<Button>();
            }
            return clickBtn;
        }
    }
    RectTransform trans;
    public RectTransform Trans
    {
        get
        {
            if (trans == null)
            {
                trans= transform.GetComponent<RectTransform>();
            }
            return trans;
        }
    }
    Image content;
    public RectTransform Content
    {
        get
        {
            if (content == null)
            {
                content = transform.Find("Content").GetComponent<Image>();
            }
            return content.GetComponent<RectTransform>();
        }
    }
    float timeScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(int level,Vector3 pos)
    {
        gameObject.SetActive(true);
        ClickBtn.onClick.RemoveAllListeners();
        ClickBtn.onClick.AddListener(() => {
            ClickSuccess(level);
        });
        transform.position = pos;
        Trans.sizeDelta = new Vector2(123-(level*6f), 123 - (level * 6f));
        Content.sizeDelta = new Vector2(10 , 10 );
        timeScale = 1;
        IsExist = true;
    }
    public void ClickSuccess(int level)
    {
        gameObject.SetActive(false);
        IsExist = false;
        Click(level);
    }
    public void NoClick()
    {
        IsExist = false;
        Debug.Log("没点到！");
        NoClickEvent();
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Content.rect.width>= Trans.rect.width)
        {
            if (IsExist)
            {
                NoClick();
            }
        }
        else
        {
            timeScale += Time.deltaTime;
            Content.sizeDelta = new Vector2(2 + timeScale * 8f, 2 + timeScale * 8f);
        }
    }
}
