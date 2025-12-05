using UnityEngine;
using System.Collections.Generic;
using TMPro; // TextMeshProを使用するための名前空間
using System.Linq;

public class GameManager : MonoBehaviour
{
    // IDにカラーコードと色名リストを紐づける辞書
    private Dictionary<string, (string colorCode, List<string> colorNames)> cardData =
        new Dictionary<string, (string, List<string>)>
    {
        { "01", ("#EE0026", new List<string> { "4R4.5/14", "カーマイン", "#EE0026" }) },
        { "02", ("#FD1A1C", new List<string> { "7R5/14", "スカーレット", "#FD1A1C" }) },
        { "03", ("#FE4118", new List<string> { "6R5.5/14", "朱色", "#FE4118" }) },
        { "04", ("#FE4118", new List<string> { "6R5.5/14", "バーミリオン", "#FE4118" }) },
        { "05", ("#F39800", new List<string> { "8YR7.5/13", "マリーゴールド", "#F39800" }) },
        { "06", ("#FFCC00", new List<string> { "6YR6.5/13", "山吹色", "#FFCC00" }) },
        { "07", ("#CCE700", new List<string> { "8Y8/12", "レモンイエロー", "#CCE700" }) },
        { "08", ("#009CD1", new List<string> { "7.5B6/10", "シアン", "#009CD1" }) },
        { "09", ("#0068B7", new List<string> { "3PB4/10", "コバルトブルー", "#0068B7" }) },
        { "10", ("#1D1A88", new List<string> { "7.5PB3.5/11", "ウルトラマリンブルー", "#1D1A88" }) },
        { "11", ("#1D1A88", new List<string> { "7.5PB3.5/11", "瑠璃色", "#1D1A88" }) },
        { "12", ("#1D1A88", new List<string> { "7.5PB3.5/11", "群青色", "#1D1A88" }) },
        { "13", ("#1D1A88", new List<string> { "7.5PB3.5/11", "杜若色", "#1D1A88" }) },
        { "14", ("#281285", new List<string> { "9PB3.5/13", "桔梗色", "#281285" }) },
        { "15", ("#714F9D", new List<string> { "2.5P4/11", "バイオレット", "#714F9D" }) },
        { "16", ("#56007D", new List<string> { "7P3.5/11.5", "モーブ", "#56007D" }) },
        { "17", ("#E55A9B", new List<string> { "C3RP5/14", "牡丹色", "#E55A9B" }) },
        { "18", ("#AF0065", new List<string> { "6RP4/14", "マゼンタ", "#AF0065" }) },
        { "19", ("#F9344C", new List<string> { "4R6.0/12.0", "韓紅花", "#F9344C" }) },
        { "20", ("#F9344C", new List<string> { "4R6.0/12.0", "珊瑚色", "#F9344C" }) },
        { "21", ("#FFF231", new List<string> { "5Y8.5/11.0", "カナリヤ", "#FFF231" }) },
        { "22", ("#aacf53", new List<string> { "4GY6.5/9", "萌黄", "#aacf53" }) },
        { "23", ("#33A65E", new List<string> { "4G7/9.0", "エメラルドグリーン", "#33A65E" }) },
        { "24", ("#33A65E", new List<string> { "4G7/9.0", "コバルトグリーン", "#33A65E" }) },
        { "25", ("#33A65E", new List<string> { "4G7/9.0", "若竹色", "#33A65E" }) },
        { "26", ("#1D86AE", new List<string> { "5B5.5/8.5", "ターコイズブルー", "#1D86AE" }) },
        { "27", ("#DF4C94", new List<string> { "7RP5/14", "パープル", "#DF4C94" }) },
        { "28", ("#DF4C94", new List<string> { "7RP5/14", "菖蒲色", "#DF4C94" }) },
        { "29", ("#9E002C", new List<string> { "4R3.5/11.5", "茜色", "#9E002C" }) },
        { "30", ("#04436D", new List<string> { "5B3.0/7.0", "マリーブルー", "#04436D" }) },
        { "31", ("#073E73", new List<string> { "3PB2.5/9.5", "藍色", "#073E73" }) },
        { "32", ("#740050", new List<string> { "10RP 3.0/9", "ワインレッド", "#740050" }) },
        { "33", ("#FA7482", new List<string> { "2.5R 6.5/7.5", "紅梅色", "#FA7482" }) },
        { "34", ("#FB8072", new List<string> { "8R 7.5/7.5", "サーモンピンク", "#FB8072" }) },
        { "35", ("#67B2CA", new List<string> { "6B 8/4", "空色", "#67B2CA" }) },
        { "36", ("#67B2CA", new List<string> { "6B 8/4", "スカイブルー", "#67B2CA" }) },
        { "37", ("#CCBA48", new List<string> { "2Y 7.5/7", "ブロンド", "#CCBA48" }) },
        { "38", ("#CCBA48", new List<string> { "2Y 7.5/7", "芥子色", "#CCBA48" }) },
        { "39", ("#66AC78", new List<string> { "7.5G 6.5/4", "青磁色", "#66AC78" }) },
        { "40", ("#8C5588", new List<string> { "5P 6/5", "ラベンダー", "#8C5588" }) },
        { "41", ("#B25938", new List<string> { "10YR 6/7.5", "黄土色", "#B25938" }) },
        { "42", ("#747E47", new List<string> { "3GY 5.5/5.5", "松葉色", "#747E47" }) },
        { "43", ("#5A804B", new List<string> { "4G 5.0/4.5", "ビリジアン", "#5A804B" }) },
        { "44", ("#743526", new List<string> { "8R3.5/7", "栗色", "#743526" }) },
        { "45", ("#743526", new List<string> { "8R3.5/7", "煉瓦色", "#743526" }) },
        { "46", ("#695B18", new List<string> { "5Y4.0/5.5", "カーキー", "#695B18" }) },
        { "47", ("#695B18", new List<string> { "5Y4.0/5.5", "オリーブ", "#695B18" }) },
        { "48", ("#56561A", new List<string> { "3GY3.5/5.0", "オリーブグリーン", "#56561A" }) },
        { "49", ("#16344F", new List<string> { "5PB3.0/4.0", "ネービーブルー", "#16344F" }) },
        { "50", ("#FBB4C4", new List<string> { "4R8.5/4", "ピーチ", "#FBB4C4" }) },
        { "51", ("#FBB4C4", new List<string> { "4R8.5/4", "ベビーピンク", "#FBB4C4" }) },
        { "52", ("#FEE6AA", new List<string> { "5YR8/5", "生成り色", "#FEE6AA" }) },
        { "53", ("#FFFFB3", new List<string> { "5Y8.5/3.5", "クリームイエロー", "#FFFFB3" }) },
        { "54", ("#B3CDE3", new List<string> { "10B7.5/3", "ベビーブルー", "#B3CDE3" }) },
        { "55", ("#fdeeef", new List<string> { "10RP9/2.5", "桜色", "#fdeeef" }) },
        { "56", ("#eedcb3", new List<string> { "10YR7/2.5", "ベージュ", "#eedcb3" }) },
        { "57", ("#f8f5e3", new List<string> { "2.5Y8.5/1.5", "アイボリー", "#f8f5e3" }) },
        { "58", ("#928c36", new List<string> { "1GY4.5/3.5", "鶯色", "#928c36" }) },
        { "59", ("#3A2C2E", new List<string> { "2.5R2.5/3", "ボルドー", "#3A2C2E" }) },
        { "60", ("#3A2C2A", new List<string> { "10R2.5/2.5", "チョコレート", "#3A2C2A" }) },
        { "61", ("#463B34", new List<string> { "10YR2.5/2", "セピア", "#463B34" }) },
        { "62", ("#2E2A31", new List<string> { "7P1.5/1.5", "茄子紺", "#2E2A31" }) },
        { "63", ("#afafb0", new List<string> { "N6.5", "シルバーグレイ", "#afafb0" }) },
        { "64", ("#4e4449", new List<string> { "5P3/1", "チャコールグレイ", "#4e4449" }) }
    };

    [SerializeField] private TextMeshProUGUI colorNameText;
    [SerializeField] private TextMeshProUGUI scoreText; // スコア表示用のTextMeshProUGUI

    [SerializeField] private TextMeshProUGUI TimeScoreText; // スコア表示用のTextMeshProUGUI
    [SerializeField] private TextMeshProUGUI CardScoreText; // スコア表示用のTextMeshProUGUI

    [SerializeField] private TextMeshProUGUI totalScoreText; // 総合スコア表示用のTextMeshProUGUI

    // 表示モード: 0 = ランダム、1 = 2番目の色名リスト
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
        isTimerRunning = (mode != 1); // mode==1ならカウントダウンしない
        if (mode != 1) UpdateTimerText();
        else if (timerText != null) timerText.text = ""; // カウント表示しない

        // 64種類のIDリスト
        List<string> allCardIds = new List<string>();
        for (int i = 1; i <= 64; i++)
        {
            allCardIds.Add(i.ToString("D2"));
        }

        // --- ここから: カラーコードが重複しないように32件選ぶロジック ---
        // カラーコードごとにIDをグループ化
        var codeToIds = new Dictionary<string, List<string>>();
        foreach (var id in allCardIds)
        {
            if (!cardData.TryGetValue(id, out var tuple)) continue;
            string code = tuple.colorCode ?? string.Empty;
            if (!codeToIds.ContainsKey(code)) codeToIds[code] = new List<string>();
            codeToIds[code].Add(id);
        }

        // 各カラーコードからランダムに1つずつ取り出す（これでカラーコードはユニークになる）
        var uniqueIds = new List<string>();
        foreach (var kv in codeToIds)
        {
            var list = kv.Value;
            int idx = Random.Range(0, list.Count);
            uniqueIds.Add(list[idx]);
        }

        // ユニーク候補をシャッフル
        for (int i = 0; i < uniqueIds.Count; i++)
        {
            int rnd = Random.Range(i, uniqueIds.Count);
            (uniqueIds[i], uniqueIds[rnd]) = (uniqueIds[rnd], uniqueIds[i]);
        }

        List<string> cardIds = new List<string>();

        if (uniqueIds.Count >= 32)
        {
            // ユニークなカラーコードが32以上あればその中から32件取る
            cardIds = uniqueIds.GetRange(0, 32);
        }
        else
        {
            // ユニーク数が足りない場合は残りを他のIDでランダム補充（ここで重複を許可して埋める）
            cardIds.AddRange(uniqueIds);
            // 残り候補（重複していない残り)
            var remainingCandidates = new List<string>(allCardIds);
            remainingCandidates.RemoveAll(id => cardIds.Contains(id));

            // シャッフル remainingCandidates
            for (int i = 0; i < remainingCandidates.Count; i++)
            {
                int rnd = Random.Range(i, remainingCandidates.Count);
                (remainingCandidates[i], remainingCandidates[rnd]) = (remainingCandidates[rnd], remainingCandidates[i]);
            }

            int need = 32 - cardIds.Count;
            for (int i = 0; i < need && i < remainingCandidates.Count; i++)
            {
                cardIds.Add(remainingCandidates[i]);
            }

            // 万が一まだ足りなければ、既存IDの中から繰り返し追加（極めて稀）
            int refillIndex = 0;
            while (cardIds.Count < 32)
            {
                if (remainingCandidates.Count > 0)
                {
                    cardIds.Add(remainingCandidates[refillIndex % remainingCandidates.Count]);
                }
                else if (uniqueIds.Count > 0)
                {
                    cardIds.Add(uniqueIds[refillIndex % uniqueIds.Count]);
                }
                else
                {
                    break;
                }
                refillIndex++;
            }
        }
        // --- ここまで ---

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

            cardInstance.transform.localScale = new Vector3(1f, 1.5f, 1f);


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
        if (gameEnded) return;

        // タイマー処理
        if (isTimerRunning && mode != 1)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 0f)
            {
                isTimerRunning = false;
                timeRemaining = 0f;
                Debug.Log("時間切れ！ ゲーム終了。");
                EndGame(false);
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

        // 2番目（index=1）を優先表示。存在しない場合はフォールバックでランダム。
        if (selectedCardData.colorNames.Count > 1)
        {
            selectedColorName = selectedCardData.colorNames[1];
        }
        else
        {
            int randomColorNameIndex = Random.Range(0, selectedCardData.colorNames.Count);
            selectedColorName = selectedCardData.colorNames[randomColorNameIndex];
        }

        Debug.Log($"正解に選ばれた色名: {selectedColorName}");
        colorNameText.text = selectedColorName; // 2番目の色名を表示
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

        int baseScore = 0;
        int.TryParse(scoreText.text, out baseScore);

        totalScore = baseScore;

        if (clearedAll)
        {
            int bonusSeconds = (mode == 1) ? 0 : Mathf.CeilToInt(Mathf.Max(0f, timeRemaining));
            totalScore += bonusSeconds;
            Debug.Log($"全カードクリア。残り秒ボーナス: {bonusSeconds} -> 最終スコア: {totalScore}");
            isTimerRunning = false;
            if (mode != 1) UpdateTimerText();
            else if (timerText != null) timerText.text = "";
        }
        else
        {
            Debug.Log($"時間切れ。最終スコア: {totalScore}");
            if (timerText != null) timerText.text = "00:00";
        }

        if (mode != 1)
        {
            if (TimeScoreText != null) TimeScoreText.text = "Time Score: " + ((mode == 1) ? "0" : Mathf.CeilToInt(Mathf.Max(0f, timeRemaining)).ToString());
            if (CardScoreText != null) CardScoreText.text = "Card Score: " + baseScore.ToString();
            if (totalScoreText != null) totalScoreText.text = "Total Score: " + totalScore.ToString();
        }
        else
        {
            // mode==1のときはscoreTextのみ "Score: " を付けて表示
            if (scoreText != null) scoreText.text = "Score: " + baseScore.ToString();
            if (TimeScoreText != null) TimeScoreText.text = "";
            if (CardScoreText != null) CardScoreText.text = "";
            if (totalScoreText != null) totalScoreText.text = "";
        }

        gameEnded = true;
        // 必要ならここで結果画面遷移やリザルト処理を呼ぶ
        // ShowResult(totalScore, clearedAll);
    }
}