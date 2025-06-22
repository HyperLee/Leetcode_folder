# DivideString2 方法說明

`DivideString2` 方法的目的是將一個字串按照指定長度進行分組，並對最後一組不足長度的部分進行填充。以下為詳細解析：

## 方法概述

此函式接收三個參數：
- **s**：要分割的字串
- **k**：每組的長度
- **fill**：用於填充的字元

回傳一個字串陣列，每個元素都是長度為 k 的子字串。

## 核心演算法

方法使用 `List<string>` 來動態儲存分組結果，比預先計算陣列大小更靈活。透過 `while` 迴圈遍歷整個字串：

```csharp
while (curr < n)
{
    int end = Math.Min(curr + k, n);
    res.Add(s.Substring(curr, end - curr));
    curr += k;
}
```

這裡的關鍵是使用 `Math.Min(curr + k, n)` 來確保不會超出字串邊界。每次迭代都會提取一個子字串並加入到結果清單中。

## 填充處理

最後的填充邏輯如下：

```csharp
string lastGroup = res[res.Count - 1];
if (lastGroup.Length < k)
{
    lastGroup += new string(fill, k - lastGroup.Length);
    res[res.Count - 1] = lastGroup;
}
```

會檢查最後一組的長度，如果不足 k 個字元，就用 `new string(fill, count)` 建立填充字元，並更新清單中的最後一個元素。

## 潛在注意事項

- **字串不可變性**：C# 中字串為不可變型別，`lastGroup += ...` 會產生新字串，需重新賦值給清單對應位置。
- **效能考量**：`List<string>` 提供良好靈活性，最後用 `ToArray()` 轉為陣列，這個轉換會複製資料，但對大多數情境來說是可接受的。

---

此實作方式簡潔且易於理解，適合需要按固定長度分組字串的場景。
