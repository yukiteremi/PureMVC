using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMediator : Mediator
{
    public const string NAME = "LoginMediator";
    private LoginView View
    {
        get { return (LoginView)ViewComponent; }
    }
    public LoginMediator(LoginView view) : base(NAME, view)
    {
        View.Open += () => { SendNotification(GameSystemEvent.GAMESTARTBTNCLICK); };
    }
    public override void HandleNotification(INotification notification)
    {
        if (notification.Name== GameSystemEvent.GAMESTARTBTNCLICK)
        {
            SendNotification(GameSystemEvent.GAMEFPSSTART);
        }
        else if (notification.Name == GameSystemEvent.LOGINCLOSE)
        {
            View.Close();
        }
        else if (notification.Name == GameSystemEvent.GAMEOVER)
        {
            View.Show();
        }
    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>();
        notifications.Add(GameSystemEvent.GAMESTARTBTNCLICK);
        notifications.Add(GameSystemEvent.LOGINCLOSE);
        notifications.Add(GameSystemEvent.GAMEOVER);
        return notifications;
    }
}
