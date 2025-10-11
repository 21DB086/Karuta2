using UnityEngine;
using System.Collections.Generic;
using TMPro; // TextMeshProを使用するための名前空間


public class GameManager : MonoBehaviour
{
    // IDにカラーコードと色名リストを紐づける辞書
    private Dictionary<string, (string colorCode, List<string> colorNames)> cardData =
        new Dictionary<string, (string, List<string>)>
    {
        { "01", ("#FF0000", new List<string> { "RED", "赤", "#FF0000" }) },
        { "02", ("#00FF00", new List<string> { "GREEN", "緑", "#00FF00" }) },
        { "03", ("#0000FF", new List<string> { "BLUE", "青", "#0000FF" }) },
        { "04", ("#FFFF00", new List<string> { "YELLOW", "黄", "#FFFF00" }) },
        { "05", ("#FF00FF", new List<string> { "MAGENTA", "紫", "#FF00FF" }) },
        { "06", ("#00FFFF", new List<string> { "CYAN", "水色", "#00FFFF" }) },
        { "07", ("#FFA500", new List<string> { "ORANGE", "オレンジ", "#FFA500" }) },
        { "08", ("#808080", new List<string> { "GRAY", "グレー", "#808080" }) },
        { "09", ("#800000", new List<string> { "MAROON", "えんじ", "#800000" }) },
        { "10", ("#008000", new List<string> { "DARK GREEN", "深緑", "#008000" }) },
        { "11", ("#000080", new List<string> { "NAVY", "紺", "#000080" }) },
        { "12", ("#808000", new List<string> { "OLIVE", "オリーブ", "#808000" }) },
        { "13", ("#800080", new List<string> { "PURPLE", "パープル", "#800080" }) },
        { "14", ("#008080", new List<string> { "TEAL", "ティール", "#008080" }) },
        { "15", ("#C0C0C0", new List<string> { "SILVER", "シルバー", "#C0C0C0" }) },
        { "16", ("#FFD700", new List<string> { "GOLD", "ゴールド", "#FFD700" }) },
        { "17", ("#DC143C", new List<string> { "CRIMSON", "クリムゾン", "#DC143C" }) },
        { "18", ("#ADFF2F", new List<string> { "GREEN YELLOW", "黄緑", "#ADFF2F" }) },
        { "19", ("#1E90FF", new List<string> { "DODGER BLUE", "ドジャーブルー", "#1E90FF" }) },
        { "20", ("#FF69B4", new List<string> { "HOT PINK", "ホットピンク", "#FF69B4" }) },
        { "21", ("#A52A2A", new List<string> { "BROWN", "茶色", "#A52A2A" }) },
        { "22", ("#7FFF00", new List<string> { "CHARTREUSE", "シャルトリューズ", "#7FFF00" }) },
        { "23", ("#00CED1", new List<string> { "DARK TURQUOISE", "ターコイズ", "#00CED1" }) },
        { "24", ("#B8860B", new List<string> { "DARK GOLDENROD", "ダークゴールド", "#B8860B" }) },
        { "25", ("#B22222", new List<string> { "FIREBRICK", "レンガ色", "#B22222" }) },
        { "26", ("#228B22", new List<string> { "FOREST GREEN", "フォレストグリーン", "#228B22" }) },
        { "27", ("#DAA520", new List<string> { "GOLDENROD", "ゴールデンロッド", "#DAA520" }) },
        { "28", ("#4B0082", new List<string> { "INDIGO", "インディゴ", "#4B0082" }) },
        { "29", ("#F08080", new List<string> { "LIGHT CORAL", "ライトコーラル", "#F08080" }) },
        { "30", ("#20B2AA", new List<string> { "LIGHT SEA GREEN", "ライトシーグリーン", "#20B2AA" }) },
        { "31", ("#87CEFA", new List<string> { "LIGHT SKY BLUE", "ライトスカイブルー", "#87CEFA" }) },
        { "32", ("#778899", new List<string> { "LIGHT SLATE GRAY", "ライトスレートグレー", "#778899" }) }
    };

    [SerializeField] private TextMeshProUGUI colorNameText;
    [SerializeField] private TextMeshProUGUI scoreText; // スコア表示用のTextMeshProUGUI

    void Start()
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card");

        // 32枚分のIDリスト
        List<string> cardIds = new List<string>();
        for (int i = 1; i <= 32; i++)
        {
            cardIds.Add(i.ToString("D2"));
        }

        // シャッフル
        for (int i = 0; i < cardIds.Count; i++)
        {
            int rnd = Random.Range(i, cardIds.Count);
            (cardIds[i], cardIds[rnd]) = (cardIds[rnd], cardIds[i]);
        }

        float startX = -7f; // 横8枚分の配置
        float startY = 3f;  // 縦4枚分の配置（上から下へ）
        float intervalX = 2.0f;
        float intervalY = -2.0f;

        int columns = 8;
        //int rows = 4;

        for (int i = 0; i < 32; i++)
        {
            int col = i % columns;
            int row = i / columns;
            Vector3 spawnPosition = new Vector3(startX + col * intervalX, startY + row * intervalY, 0);
            GameObject cardInstance = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);

            Card cardScript = cardInstance.GetComponent<Card>();
            if (cardScript != null)
            {
                cardScript.SetId(cardIds[i]);
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
