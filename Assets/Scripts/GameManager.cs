using UnityEngine;
using System.Collections.Generic;
using TMPro; // TextMeshProを使用するための名前空間


public class GameManager : MonoBehaviour
{
    // IDにカラーコードと色名リストを紐づける辞書
    private Dictionary<string, (string colorCode, List<string> colorNames)> cardData =
        new Dictionary<string, (string, List<string>)>
    {
        { "01", ("#FF0000", new List<string> { "RED", "Aka","FF0000" }) },
        { "02", ("#00FF00", new List<string> { "GREEN", "Midori", "#00FF00"}) },
        { "03", ("#0000FF", new List<string> { "BLUE", "Ao", "#0000FF"}) },
        { "04", ("#FFFF00", new List<string> { "YELLOW", "Ki", "#FFFF00"}) }
    };

    [SerializeField] private TextMeshProUGUI colorNameText;
    [SerializeField] private TextMeshProUGUI scoreText; // スコア表示用のTextMeshProUGUI

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
        // 画面に表示された色名の色をしたカードをクリックすると点数が増える処理
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Raycast2D hit: " + hit.collider.name);
                Card clickedCard = hit.collider.GetComponent<Card>();
                if (clickedCard != null)
                {
                    // カードの色名が正解の色名と一致するかチェック
                    if (clickedCard.colorNames.Contains(colorNameText.text))
                    {
                        Debug.Log("正解のカードがクリックされました！");
                        // 点数を増やす処理をここに追加
                        int currentScore = int.Parse(scoreText.text);
                        currentScore += 10; // 例えば10点加算
                        scoreText.text = currentScore.ToString(); // スコアを更新
                        // カードを非表示にする
                        clickedCard.gameObject.SetActive(false);
                        //cardDataからクリックされたカードのIDを削除
                        //cardDataからクリックされたカードの色名を値に持つ項目を削除
                        string removeKey = null;
                        foreach (var pair in cardData)
                        {
                            if (pair.Value.colorNames.Contains(colorNameText.text))
                            {
                                removeKey = pair.Key;
                                break;
                            }
                        }
                        if (removeKey != null)
                        {
                            cardData.Remove(removeKey);
                            //cardDataの項目が空になった場合、ゲームを終了する処理を追加
                            if (cardData.Count == 0)
                            {
                                Debug.Log("全てのカードがクリアされました。ゲーム終了！");
                                // ゲーム終了の処理をここに追加（例えば、ゲームオーバー画面を表示するなど）
                            }
                        }
                        // 正解の色を再度ランダムに決定
                        string newCorrectColor = SetColor();
                    }
                    else
                    {
                        Debug.Log("不正解のカードがクリックされました。");
                        //減点処理をここに追加
                        int currentScore = int.Parse(scoreText.text);
                        currentScore -= 5; // 例えば5点減点
                        //if (currentScore < 0) currentScore = 0; // スコアがマイナスにならないように
                        scoreText.text = currentScore.ToString(); // スコアを更新
                    }
                }
            }
        }
    }


    //正解の色をランダムに決定する関数SetColor()
    private string SetColor()
    {
        List<string> keys = new List<string>(cardData.Keys);
        if (keys.Count == 0)
        {
            colorNameText.text = ""; // 何も表示しない
            Debug.Log("カードが残っていません。");
            return null;
        }

        // ランダムにカードIDを選択
        int randomCardIndex = Random.Range(0, keys.Count);
        var selectedCardData = cardData[keys[randomCardIndex]];

        // 選ばれたカードの色名リストからランダムに1つの色名を選択
        int randomColorNameIndex = Random.Range(0, selectedCardData.colorNames.Count);
        string selectedColorName = selectedCardData.colorNames[randomColorNameIndex];

        Debug.Log($"正解に選ばれた色名: {selectedColorName}");
        colorNameText.text = selectedColorName; // ランダムに選ばれた色名を表示
        return keys[randomCardIndex];
    }

}
