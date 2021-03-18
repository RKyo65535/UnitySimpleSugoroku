using System.Collections;
using System.Collections.Generic;
using System.Linq;//これを使ってデータベース風のコレクション操作ができる！
using UnityEngine;
using UnityEngine.UI;//これをusingしないとUI系は使えない

public class PlayerVariables : MonoBehaviour
{

    bool canTakeDice;//ダイスを振れるか否か

    Transform myTF;//キャッシングパワー
    int currentCell;//現在の座標

    int lifePoint;//現在のライフ
    [SerializeField] Text diceText;//何マス進むか表示するテキストへの参照。SerializeFieldでprivateなまま、インスペクターから参照設定できる
    [SerializeField] Text currentLifeText;//現在HPを表示する。その参照を入れて
    [SerializeField] GameObject parentOfCells;//マスの親への参照を入れて。
    GameObject[] cells;


    int CurrentCell
    {
        get
        {
            return currentCell;
        }
        set
        {
            currentCell = Mathf.Clamp(value, 0, cells.Length-1);
            Debug.Log(value);
        }
    }

    private void Awake()
    {
        //マスのGameObjectをアライざらいゲットする。タグの設定を忘れずに
        List<GameObject> tempCells = new List<GameObject>();
        for(int i = 0; i < parentOfCells.transform.childCount; i++)
        {
            if (parentOfCells.transform.GetChild(i).tag == "Cell(Mass)")
            {               
                tempCells.Add(parentOfCells.transform.GetChild(i).gameObject);
            }
        }
        cells = tempCells.ToArray();


        myTF = transform;//キャッシュすることで高速化
        CurrentCell = 0;//一応0で明示的に初期化
        lifePoint = 10;
        currentLifeText.text = YoGoSyu.LifeTextHeader + lifePoint;//HPをきちんと表示する
        canTakeDice = true;//最初は振れる
    }

    /// <summary>
    /// サイコロを振る時に呼ぶ関数です。
    /// </summary>
    public void TakeDice()
    {
        if (canTakeDice)
        {
            canTakeDice = false;
            int tempDiceValue = Random.Range(0, 6) + 1;//ダイスの値をランダムに設定
            diceText.text = tempDiceValue + "";//表示する文字を決定する。


            StartCoroutine(GoAheadWithAnim(tempDiceValue));//順々に移動するようにする。(コルーチンなので怖い)

        }

    }

    /// <summary>
    /// 回復量を引数にとる関数。HPが回復します。
    /// </summary>
    /// <param name="cureValue"></param>
    public void UpdateLIfePoint(int cureValue)
    {
        lifePoint += cureValue;
        currentLifeText.text = YoGoSyu.LifeTextHeader + lifePoint;
    }

    /// <summary>
    /// 現在位置を更新します。
    /// </summary>
    public void UpdateCurrentCell(int change)
    {
        CurrentCell += change;
        myTF.position = cells[currentCell].transform.position;//座標移動

    }


    IEnumerator GoAheadWithAnim(int num)
    {
        for(int i = 0; i < num; i++)
        {
            yield return UpdateCurrentCellWithAnim(1,20);//0.4秒で移動
        }
        cells[currentCell].GetComponent<DefineCellEvents>().ActivateMassEvent(this);//止まったマスのイベント発動
        canTakeDice = true;
        yield return null;
    }

    IEnumerator UpdateCurrentCellWithAnim(int change,int flame)
    {
        CurrentCell += change;

        Vector2 startPos = myTF.position;
        Vector2 endPos = cells[CurrentCell].transform.position;

        for (int i = 0; i < flame; i++)
        {
            myTF.position = startPos + (endPos - startPos) / flame * i;
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

}
