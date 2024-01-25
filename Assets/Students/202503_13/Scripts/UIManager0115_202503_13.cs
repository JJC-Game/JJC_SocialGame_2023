using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager0115_202503_13 : MonoBehaviour
{
    public int CharaID;
    [SerializeField] int month111;
    [SerializeField] string aisatu = "よろしくお願いします。";
    string front1, middle1, back1;
    string imagePathfinder = "Assets/Resources/Textures/Enemy";
    // Start is called before the first frame update
    void Start()
    {
        /*SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Application.fixDataManager.GetCharaImagePath getCharaImagePath;
        spriteRenderer.sprite = Resources.Load<Sprite>(getCharaImagePath);*/


        Debug.Log(Application.fixDataManager.GetCharaName(CharaID));//入れた数字によって出力されるキャラの名前が変わる。

        string kaeritiaisatu = kaeriti(month111,aisatu);//文字列返り値挨拶に返り値に月の数字を入れて出力させる
        Debug.Log(kaeritiaisatu);

        string charaName = Application.fixDataManager.GetCharaName(5);//キャラ名　先生の指定した通り
        Debug.Log(charaName);

        CharaTeam();//バトルに出ているキャラ名の表示

        battlegrafical();
    }
    void Update()
    {
        
    }
    /*void Month()
    {
        Debug.Log("今日は"  + "です。");
    }*/
    string kaeriti(int month　, string webnesday)
    {
        return "今日は" + month + "月です" + webnesday;
    }
    public string GetCharaname(int charaID)
    {
        return Application.fixDataManager.GetCharaName(CharaID);//先生の指定の仕方だと出来なかったため違う方法にしている
    }
    public void CharaTeam()
    {
       BattleData.BattleFixData data = Application.fixDataManager.GetBattleData(1);
        Debug.Log(Application.fixDataManager.GetCharaName( data.battle1_front));
        Debug.Log(Application.fixDataManager.GetCharaName( data.battle1_middle));
        Debug.Log(Application.fixDataManager.GetCharaName( data.battle1_back));

    }
    public void SpriteImageChara()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(front1);
        
    }
    public void battlegrafical()
    {
        /*BattleData.BattleFixData dataid = Application.fixDataManager.GetBattleData(1);
        Debug.Log(Application.fixDataManager.GetCharaImagePath(dataid.battle1_front));
        Application.fixDataManager.GetCharaImagePath(dataid.battle1_front = front1);
        Application.fixDataManager.GetCharaImagePath(dataid.battle1_middle = middle1);
        Application.fixDataManager.GetCharaImagePath(dataid.battle1_back = back1);*/
    }
}
