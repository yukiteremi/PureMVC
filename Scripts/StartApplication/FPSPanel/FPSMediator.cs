using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMediator : Mediator
{
    private FPSProxy proxy;
    public const string NAME = "StatisticsMediator";
    private FPSview View
    {
        get { return (FPSview)ViewComponent; }
    }
    public FPSMediator(FPSview view) : base(NAME, view)
    {
        View.LevelUp += () => { SendNotification(GameSystemEvent.LEVELUP); };
        View.NoClick += () => { SendNotification(GameSystemEvent.CLICKFAIL); };
        View.Click += data => { SendNotification(GameSystemEvent.CLICKSUCCESS, data); };
    }
    public override void OnRegister()
    {
        proxy = facade.RetrieveProxy(FPSProxy.NAME) as FPSProxy;

    }
    public override void HandleNotification(INotification notification)
    {
        FPSdata data = proxy.data as FPSdata;
        if (notification.Name == GameSystemEvent.OPENFPS)
        {
            View.Show();
            View.HpBoxAndScoreInit(data);

            View.StartCoroutine(View.gameStart());
           
        }
        else if (notification.Name == GameSystemEvent.LEVELUP)
        {
            proxy.LevelUp();
        }
        else if (notification.Name == GameSystemEvent.CLICKFAIL)
        {
            proxy.HpDown();
        }
        else if (notification.Name == GameSystemEvent.HPDOWN)
        {
            View.HpChange(data);
        }
        else if (notification.Name == GameSystemEvent.CLICKSUCCESS)
        {
            int num =int.Parse(notification.Body.ToString());
            proxy.AddScore(num);
        }
        else if (notification.Name == GameSystemEvent.SCOREUP)
        {
            View.ScoreChange(data);
        }
        else if (notification.Name == GameSystemEvent.GAMEOVER)
        {
            Debug.Log("GameOver");
            View.GameOver();
        }
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>();
        notifications.Add(GameSystemEvent.OPENFPS);
        notifications.Add(GameSystemEvent.LEVELUP);
        notifications.Add(GameSystemEvent.HPDOWN);
        notifications.Add(GameSystemEvent.CLICKSUCCESS);
        notifications.Add(GameSystemEvent.CLICKFAIL);
        notifications.Add(GameSystemEvent.GAMEOVER);
        notifications.Add(GameSystemEvent.SCOREUP);
        return notifications;
    }
}
    

