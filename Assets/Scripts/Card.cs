using UnityEngine;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    private string id;
    private string colorCode;
    private List<string> colorNames;
    
    public void SetId(string cardId)
    {
        id = cardId;
    }

    // カラーコードと色名リストをセットするメソッド
    public void SetColorData(string code, List<string> names)
    {
        colorCode = code;
        colorNames = new List<string>(names); // データのコピー
        ApplyColor(); // カラーコードがセットされたら色を反映
        PrintCardData(); // データをコンソールに表示
    }

    // カードのデータをコンソールに表示する
    private void PrintCardData()
    {
        string names = colorNames != null ? string.Join(", ", colorNames) : "なし";
        Debug.Log($"カードID: {id}, カラーコード: {colorCode}, 色名: {names}");
    }

    // カラーコードをColor型に変換してカードの色を変更
    private void ApplyColor()
    {
        if (ColorUtility.TryParseHtmlString(colorCode, out Color color))
        {
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // 必要に応じて初期化処理を追加
    }

    // Update is called once per frame
    private void Update()
    {
        // 必要に応じて更新処理を追加
    }
}
