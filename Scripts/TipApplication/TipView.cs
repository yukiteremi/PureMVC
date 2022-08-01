using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class TipView : MonoBehaviour
{
    public UnityAction<BagItem> UseAction = null;
    public UnityAction CloseAction = null;
    public Button Close, Use;
    public BagItem Data;
    public Image img;
    public Button CloseBtn
    {
        get
        {
            if (Close == null)
            {
                Close = transform.Find("CloseBtn").GetComponent<Button>();
            }
            return Close;
        }
    }
    public Button UseBtn
    {
        get
        {
            if (Use == null)
            {
                Use = transform.Find("UseBtn").GetComponent<Button>();
            }
            return Use;
        }
    }
    public Image IconImage
    {
        get
        {
            if (img == null)
            {
                img = transform.Find("Image").GetComponent<Image>();
            }
            return img;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Init()
    {
        UseBtn.onClick.AddListener(()=> { UseAction(Data); });
        CloseBtn.onClick.AddListener(() => { CloseAction(); });
    }
    public void Change(BagItem data)
    {
        Data = data;
        IconImage.sprite = Resources.Load<Sprite>("Img/" + data.icon);
        ShowUp();
    }
    public void ShowUp()
    {
        transform.position = new Vector3(300,500,0);
    }
    public void ShowDown()
    {
        transform.position = new Vector3(-2300, 60, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
