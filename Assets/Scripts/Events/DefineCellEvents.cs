using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineCellEvents : MonoBehaviour
{
    enum KindOfEvent
    {
        Nothing,//何も怒らない
        HealthBenefit,//HP回復とか
        Damage,//自身にダメージを与える
        Warp2,//2マス先に進む
        ReturnToHuridashi
    }


    [SerializeField] KindOfEvent kindOfEvent;

    private void Awake()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();

        switch (kindOfEvent)
        {
            case KindOfEvent.Nothing:
                sp.color = Color.white;//しろくなれ
                break;
            case KindOfEvent.HealthBenefit:
                sp.color = new Color(0.4f,0.4f,0.8f);//青っぽい色にする
                break;
            case KindOfEvent.Damage:
                sp.color = new Color(0.8f, 0.4f, 0.6f);//赤っぽい色にする
                break;
            case KindOfEvent.Warp2:
                sp.color = new Color(0.8f, 0.7f, 0.3f);//黄色色にする
                break;
            default:
                break;
        }
    }


    public void ActivateMassEvent(PlayerVariables player)
    {
        switch (kindOfEvent)
        {
            case KindOfEvent.Nothing:
                //何もしない
                break;
            case KindOfEvent.HealthBenefit:
                player.UpdateLIfePoint(2);//HPを2回復
                break;
            case KindOfEvent.Damage:
                player.UpdateLIfePoint(-3);//HPを-3
                break;
            case KindOfEvent.Warp2:
                player.UpdateCurrentCell(2);//2マス先に進める
                break;
            case KindOfEvent.ReturnToHuridashi:
                player.UpdateCurrentCell(-999);//2マス先に進める
                break;
            default:
                break;
        }
    }

}
