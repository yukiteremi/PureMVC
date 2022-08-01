using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FPSview : MonoBehaviour
{
    public UnityAction LevelUp = null;

    public UnityAction NoClick = null;
    public UnityAction<int> Click = null;

    public List<Image> HpBox = new List<Image>();
    public Dictionary<FPSItemView, GameObject> Pool = new Dictionary<FPSItemView, GameObject>();
    public List<FPSItemView> pool = new List<FPSItemView>();
    public GameObject prefab,TargetPrefab;
    public Transform parent;
    public Transform Parent
    {
        get
        {
            if (parent == null)
            {
                parent = transform.Find("HpBox");
            }
            return parent;
        }
    }
    public Transform targetParent;
    public Transform TargetParent
    {
        get
        {
            if (targetParent == null)
            {
                targetParent = transform.Find("Target");
            }
            return targetParent;
        }
    }
    public Text scoreText;
    public Text ScoreText
    {
        get
        {
            if (scoreText == null)
            {
                scoreText = transform.Find("Text").GetComponent<Text>();
            }
            return scoreText;
        }
    }
    FPSdata Mydata;
    float countTime = 0;
    bool Game = true;
    void Start()
    {
        prefab = Resources.Load<GameObject>("FPS/HpimageBox");
        TargetPrefab = Resources.Load<GameObject>("FPS/ClickTarget");
        
    }
    public void HpBoxAndScoreInit(FPSdata data)
    {
        foreach (var item in pool)
        {
            if (item.IsExist)
            {
                item.gameObject.SetActive(false);
                item.IsExist = false;
            }
        }
        Mydata = data;
        Game = true;
        countTime = 0;
        if (HpBox.Count==data.hp)
        {
            for (int i = 0; i < HpBox.Count; i++)
            {
                HpBox[i].color = Color.red;
            }
        }
        else
        {
            for (int i = HpBox.Count; i < data.hp; i++)
            {
                GameObject hp=GameObject.Instantiate(prefab,Parent,false);
                HpBox.Add(hp.GetComponent<Image>());
            }
        }
        ScoreText.text = "分数：" + data.score;
    }
    public void HpChange(FPSdata data)
    {
        for (int i = 0; i < HpBox.Count; i++)
        {
            HpBox[i].color = Color.black;
        }
        for (int i = 0; i < data.hp; i++)
        {
            HpBox[i].color = Color.red;
        }
    }
    public void ScoreChange(FPSdata data)
    {
        ScoreText.text = "分数：" + data.score;
    }
    public void Show()
    {
        transform.position = new Vector3(0,0,0);
        gameObject.SetActive(true);
    }
    public void GameOver()
    {
        Game = false;
        gameObject.SetActive(false);
    }
    public IEnumerator gameStart()
    {
        while (Game)
        {
            AddNewTarget();
            yield return new WaitForSeconds(3f - (Mydata.Level * 0.125f));
        }
    }
    public void AddNewTarget()
    {
        Vector3 pos = new Vector3(Random.Range(0,800), Random.Range(0, 800), 0);
        foreach (var item in pool)
        {
            if (item.IsExist==false)
            {
                item.Init(Mydata.Level, pos);
                return;
            }
        }
        GameObject clone= GameObject.Instantiate(TargetPrefab,TargetParent,false);
        FPSItemView view= clone.AddComponent<FPSItemView>();
        view.Init(Mydata.Level, pos);
        view.Click = Click;
        view.NoClickEvent = NoClick;
        pool.Add(view);
    }
    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime >= 10)
        {
            countTime = 0;
            LevelUp();
        }
    }
}
