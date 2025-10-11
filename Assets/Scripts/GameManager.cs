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
        { "32", ("#778899", new List<string> { "LIGHT SLATE GRAY", "ライトスレートグレー", "#778899" }) },
        { "33", ("#FFB6C1", new List<string> { "LIGHT PINK", "ライトピンク", "#FFB6C1" }) },
        { "34", ("#8B0000", new List<string> { "DARK RED", "ダークレッド", "#8B0000" }) },
        { "35", ("#006400", new List<string> { "DARK GREEN2", "ダークグリーン2", "#006400" }) },
        { "36", ("#00008B", new List<string> { "DARK BLUE", "ダークブルー", "#00008B" }) },
        { "37", ("#BDB76B", new List<string> { "DARK KHAKI", "ダークカーキ", "#BDB76B" }) },
        { "38", ("#8B008B", new List<string> { "DARK MAGENTA", "ダークマゼンタ", "#8B008B" }) },
        { "39", ("#556B2F", new List<string> { "DARK OLIVE GREEN", "ダークオリーブグリーン", "#556B2F" }) },
        { "40", ("#FF8C00", new List<string> { "DARK ORANGE", "ダークオレンジ", "#FF8C00" }) },
        { "41", ("#9932CC", new List<string> { "DARK ORCHID", "ダークオーキッド", "#9932CC" }) },
        { "42", ("#8FBC8F", new List<string> { "DARK SEA GREEN", "ダークシーグリーン", "#8FBC8F" }) },
        { "43", ("#483D8B", new List<string> { "DARK SLATE BLUE", "ダークスレートブルー", "#483D8B" }) },
        { "44", ("#2F4F4F", new List<string> { "DARK SLATE GRAY", "ダークスレートグレー", "#2F4F4F" }) },
        { "45", ("#00BFFF", new List<string> { "DEEP SKY BLUE", "ディープスカイブルー", "#00BFFF" }) },
        { "46", ("#696969", new List<string> { "DIM GRAY", "ディムグレー", "#696969" }) },
        { "47", ("#1E90FF", new List<string> { "DODGER BLUE2", "ドジャーブルー2", "#1E90FF" }) },
        { "48", ("#B22222", new List<string> { "FIREBRICK2", "レンガ色2", "#B22222" }) },
        { "49", ("#FFFAF0", new List<string> { "FLORAL WHITE", "フローラルホワイト", "#FFFAF0" }) },
        { "50", ("#228B22", new List<string> { "FOREST GREEN2", "フォレストグリーン2", "#228B22" }) },
        { "51", ("#DCDCDC", new List<string> { "GAINSBORO", "ゲインズボロ", "#DCDCDC" }) },
        { "52", ("#F8F8FF", new List<string> { "GHOST WHITE", "ゴーストホワイト", "#F8F8FF" }) },
        { "53", ("#FFD700", new List<string> { "GOLD2", "ゴールド2", "#FFD700" }) },
        { "54", ("#DAA520", new List<string> { "GOLDENROD2", "ゴールデンロッド2", "#DAA520" }) },
        { "55", ("#808080", new List<string> { "GRAY2", "グレー2", "#808080" }) },
        { "56", ("#008000", new List<string> { "GREEN2", "グリーン2", "#008000" }) },
        { "57", ("#ADFF2F", new List<string> { "GREEN YELLOW2", "黄緑2", "#ADFF2F" }) },
        { "58", ("#F0FFF0", new List<string> { "HONEYDEW", "ハニーデュー", "#F0FFF0" }) },
        { "59", ("#FF69B4", new List<string> { "HOT PINK2", "ホットピンク2", "#FF69B4" }) },
        { "60", ("#CD5C5C", new List<string> { "INDIAN RED", "インディアンレッド", "#CD5C5C" }) },
        { "61", ("#4B0082", new List<string> { "INDIGO2", "インディゴ2", "#4B0082" }) },
        { "62", ("#FFFFF0", new List<string> { "IVORY", "アイボリー", "#FFFFF0" }) },
        { "63", ("#F0E68C", new List<string> { "KHAKI", "カーキ", "#F0E68C" }) },
        { "64", ("#E6E6FA", new List<string> { "LAVENDER", "ラベンダー", "#E6E6FA" }) }
    };

    [SerializeField] private TextMeshProUGUI colorNameText;
    [SerializeField] private TextMeshProUGUI scoreText; // スコア表示用のTextMeshProUGUI

    [SerializeField] private TextMeshProUGUI TimeScoreText; // スコア表示用のTextMeshProUGUI
    [SerializeField] private TextMeshProUGUI CardScoreText; // スコア表示用のTextMeshProUGUI

    [SerializeField] private TextMeshProUGUI totalScoreText; // 総合スコア表示用のTextMeshProUGUI

    // 表示モード: 0 = ランダム、1 = 2番目の色名
    public int mode = 0; // 0 = ランダム、1 = 2番目の色名

    // 制限時間 (秒) を 180 秒に設定
    [SerializeField] private float timeLimitSeconds = 180f;
    [SerializeField] private TextMeshProUGUI timerText; // 制限時間表示用TextMeshProUGUI

    private float timeRemaining;
    private bool isTimerRunning = false;
    private bool gameEnded = false;
    private int totalScore; // 残り秒を加算した総合スコアを格納する変数（新規追加）

    void Awake()
    {
        // TitleUIからmodeを取得
        mode = GameSettings.Mode;
        //mode = PlayerPrefs.GetInt("GameMode", 0); // デフォルトは0（ランダム）
        Debug.Log("Game Mode: " + mode);
    }

    void Start()
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card");

        // タイマー初期化（180秒）
        timeRemaining = timeLimitSeconds;
        isTimerRunning = true;
        UpdateTimerText();

        // 64種類のIDリスト
        List<string> allCardIds = new List<string>();
        for (int i = 1; i <= 64; i++)
        {
            allCardIds.Add(i.ToString("D2"));
        }

        // シャッフルして32枚分だけ選択
        for (int i = 0; i < allCardIds.Count; i++)
        {
            int rnd = Random.Range(i, allCardIds.Count);
            (allCardIds[i], allCardIds[rnd]) = (allCardIds[rnd], allCardIds[i]);
        }
        List<string> cardIds = allCardIds.GetRange(0, 32);

        // cardDataを画面に表示する32枚分だけに絞る
        var newCardData = new Dictionary<string, (string colorCode, List<string> colorNames)>();
        foreach (var id in cardIds)
        {
            if (cardData.ContainsKey(id))
            {
                newCardData.Add(id, cardData[id]);
            }
        }
        cardData = newCardData;

        float startX = -7f;
        float startY = 3f;
        float intervalX = 2.0f;
        float intervalY = -2.0f;
        int columns = 8;

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
        if (gameEnded) return; // ゲームが終了していたら何もしない

        // タイマー処理
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 0f)
            {
                isTimerRunning = false;
                timeRemaining = 0f;
                Debug.Log("時間切れ！ ゲーム終了。");
                // 時間切れの処理をここに追加（例えば、ゲームオーバー画面を表示するなど）
                EndGame(false); // クリアせず終了
            }
        }

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
                        int currentScore = 0;
                        int.TryParse(scoreText.text, out currentScore);
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
                                EndGame(true); // クリアして終了
                            }
                        }
                        // 正解の色を再度ランダムに決定
                        string newCorrectColor = SetColor();
                    }
                    else
                    {
                        Debug.Log("不正解のカードがクリックされました。");
                        //減点処理をここに追加
                        int currentScore = 0;
                        int.TryParse(scoreText.text, out currentScore);
                        currentScore -= 5; // 例えば5点減点
                        //if (currentScore < 0) currentScore = 0; // スコアがマイナスにならないように
                        scoreText.text = currentScore.ToString(); // スコアを更新
                    }
                }
            }
        }
    }

    // 正解の色をランダムに決定する関数SetColor()
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

        string selectedColorName;

        // modeが1なら2番目（index=1）を優先表示。存在しない場合はフォールバックでランダム。
        if (mode == 1 && selectedCardData.colorNames.Count > 1)
        {
            selectedColorName = selectedCardData.colorNames[1];
        }
        else
        {
            int randomColorNameIndex = Random.Range(0, selectedCardData.colorNames.Count);
            selectedColorName = selectedCardData.colorNames[randomColorNameIndex];
        }

        Debug.Log($"正解に選ばれた色名: {selectedColorName}");
        colorNameText.text = selectedColorName; // ランダムに選ばれた色名を表示
        return keys[randomCardIndex];
    }

    // タイマーを更新して表示する関数
    private void UpdateTimerText()
    {
        if (timerText == null) return;

        // 負の値を表示しないようにクランプ
        float t = Mathf.Max(0f, timeRemaining);

        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);

        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    // ゲーム終了処理: clearedAll=true の場合は残り秒をスコアに加算
    private void EndGame(bool clearedAll)
    {
        if (gameEnded) return;

        // 現在スコア取得（安全に）
        int baseScore = 0;
        int.TryParse(scoreText.text, out baseScore);

        totalScore = baseScore; // ゲーム中のスコアを基に

        if (clearedAll)
        {
            // 残り秒をボーナス（切り上げ）
            int bonusSeconds = Mathf.CeilToInt(Mathf.Max(0f, timeRemaining));
            totalScore += bonusSeconds; // 残り秒を加算したスコアをtotalScore変数に格納
            Debug.Log($"全カードクリア。残り秒ボーナス: {bonusSeconds} -> 最終スコア: {totalScore}");
            // タイマーを停止し、残り時間を表示したままにする
            isTimerRunning = false;
            UpdateTimerText(); // 現在の残り時間を表示
        }
        else
        {
            Debug.Log($"時間切れ。最終スコア: {totalScore}");
            if (timerText != null) timerText.text = "00:00";
        }


        ////
        if (TimeScoreText != null) TimeScoreText.text = "Time Score: " + Mathf.CeilToInt(Mathf.Max(0f, timeRemaining)).ToString();

        if (CardScoreText != null) CardScoreText.text = "Card Score: " + baseScore.ToString();

        // 総合スコアをtotalScoreTextに表示（ゲーム中のscoreTextは変更せず）
        if (totalScoreText != null) totalScoreText.text = "Total Score: " + totalScore.ToString();


        // ゲーム終了フラグ・停止処理
        gameEnded = true;

        // 必要ならここで結果画面遷移やリザルト処理を呼ぶ
        // ShowResult(totalScore, clearedAll);
    }
}
