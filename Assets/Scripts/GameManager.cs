using UnityEngine;
using System.Collections.Generic;
using TMPro; // TextMeshProを使用するための名前空間


public class GameManager : MonoBehaviour
{
    // IDにカラーコードと色名リストを紐づける辞書
    private Dictionary<string, (string colorCode, List<string> colorNames)> cardData =
        new Dictionary<string, (string, List<string>)>
    {
        { "01", ("#FF0000", new List<string> { "RED", "Aka" }) },
        { "02", ("#00FF00", new List<string> { "GREEN", "Midori" }) },
        { "03", ("#0000FF", new List<string> { "BLUE", "Ao" }) },
        { "04", ("#FFFF00", new List<string> { "YELLOW", "Ki" }) }
    };

    [SerializeField] private TextMeshProUGUI colorNameText;

    void Start()
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card");

        // IDリストを作成
        List<string> cardIds = new List<string> { "01", "02", "03", "04" };

        // シャッフル
        for (int i = 0; i < cardIds.Count; i++)
        {
            int rnd = Random.Range(i, cardIds.Count);
            (cardIds[i], cardIds[rnd]) = (cardIds[rnd], cardIds[i]);
        }

        float startX = -3f;
        float interval = 2.0f;

        for (int i = 0; i < 4; i++)
        {
            Vector3 spawnPosition = new Vector3(startX + i * interval, 0, 0);
            GameObject cardInstance = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);

            // カードのIDをセット（Cardスクリプトが必要）
            Card cardScript = cardInstance.GetComponent<Card>();
            if (cardScript != null)
            {
                cardScript.SetId(cardIds[i]);
                // カラーコードと色名もセット（Cardクラスに対応メソッドが必要）
                if (cardData.TryGetValue(cardIds[i], out var data))
                {
                    cardScript.SetColorData(data.colorCode, data.colorNames);
                }
            }
        }


        // 正解の色をランダムに決定
        string correctColor = SetColor();
    }

    void Update()
    {
        //画面に表示された色名の色をしたカードをクリックすると点数が増える処理
        // ここではクリックイベントの処理を行うことができます。

    }


    //正解の色をランダムに決定する関数SetColor()
    private string SetColor()
    {
        List<string> keys = new List<string>(cardData.Keys);
        int randomIndex = Random.Range(0, keys.Count);
        //色名を表示する
        Debug.Log($"正解に選ばれた色名: {cardData[keys[randomIndex]].colorNames[0]}");
        //色名をTextMeshPro「ColorName」に表示する
        colorNameText.text = cardData[keys[randomIndex]].colorNames[0]; // 色名を設定


        return keys[randomIndex];





    }

}
