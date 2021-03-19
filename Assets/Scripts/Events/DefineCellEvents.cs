using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefineCellEvents : MonoBehaviour
{

    

    enum KindOfEvent
    {
        Nothing = 0,//何も怒らない
        HealthBenefit,//HP回復とか
        Damage,//自身にダメージを与える
        Warp2,//2マス先に進む
        ReturnToHuridashi,
        Warp5
    }


    [SerializeField] KindOfEvent kindOfEvent;

    private void Awake()
    {
        kindOfEvent = (KindOfEvent)Random.Range(0, System.Enum.GetNames(typeof(KindOfEvent)).Length);

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
            case KindOfEvent.ReturnToHuridashi:
                sp.color = new Color(0f, 0f, 0f);//漆黒にする
                break;
            case KindOfEvent.Warp5:
                sp.color = new Color(0.5f, 0.5f, 0.5f);//灰になり
                break;
            default:
                break;
        }
    }


    public void ActivateMassEvent(PlayerVariables player, Text text)
    {
        switch (kindOfEvent)
        {
            case KindOfEvent.Nothing:
                //何もしない
                break;
            case KindOfEvent.HealthBenefit:
                player.UpdateLIfePoint(2);//HPを2回復
                text.text = "回復した";
                break;
            case KindOfEvent.Damage:
                player.UpdateLIfePoint(-3);//HPを-3
                text.text = "ダメージ受けた";
                break;
            case KindOfEvent.Warp2:
                player.UpdateCurrentCell(2);//2マス先に進める
                text.text = "2マス進んだ";
                break;
            case KindOfEvent.ReturnToHuridashi:
                player.UpdateCurrentCell(-999);//2マス先に進める
                text.text = "振出しに戻った";
                break;
            case KindOfEvent.Warp5:
                player.UpdateCurrentCell(5);//5マス先に進める
                text.text = "5マス進んだ";
                break;
            default:
                break;
        }
    }

}
