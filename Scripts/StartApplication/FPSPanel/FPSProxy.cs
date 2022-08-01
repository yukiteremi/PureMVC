using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSProxy : Proxy
{
    public new const string NAME = "FPSProxy";
    FPSdata fPSdata;
    public FPSProxy() : base(NAME, null)
    {

    }

    public override void OnRegister()
    {
        fPSdata = new FPSdata(5, 0); ;
        data = fPSdata;
    }

    public void AddScore(int point)
    {
        int score = point * 5;
        fPSdata.score += score;
        SendNotification(GameSystemEvent.SCOREUP);
    }
    public void HpDown()
    {
        fPSdata.hp -= 1;
        SendNotification(GameSystemEvent.HPDOWN);
        if (fPSdata.hp<=0)
        {
            GameReStart();
            SendNotification(GameSystemEvent.GAMEOVER);
        }
    }
    public void GameReStart()
    {
        fPSdata = new FPSdata(5, 0); ;
        data = fPSdata;
    }
    public void LevelUp()
    {
        fPSdata.Level += 1;
        if (fPSdata.Level>=18)
        {
            fPSdata.Level = 18;
        }
    }
}
