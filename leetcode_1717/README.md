# LeetCode 1717 — Maximum Score From Removing Substrings

## 題目說明

給定一個字串 `s` 和兩個整數 `x`、`y`，你可以不限次數地進行以下兩種操作：
1. 移除子字串 "ab" 並獲得 x 分。
2. 移除子字串 "ba" 並獲得 y 分。

請回傳對 s 執行上述操作後，所能獲得的最大分數。

---

## 解法總覽

本專案以 C# 撰寫，主程式與解法皆於 `Program.cs` 中。核心解法函式為 `MaximumGain`，採用**貪心策略**，流程與複雜度分析如下：

### 解法核心：MaximumGain

#### 解題思路

- **貪心原則**：每次都優先移除分數較高的子字串（"ab" 或 "ba"），以最大化總分。
- 若 x < y，則將 x、y 交換，並將字串中所有 'a'、'b' 互換，問題等價，後續只需考慮一種情況。
- 依序遍歷 s，遇到 'a' 累加計數，遇到 'b' 且前面有 'a' 則消去一組 "ab" 並加分，否則累加 b 計數。
- 最後剩下的 a、b 可組成 "ba"，取 min(cntA, cntB) 組，每組加 y 分。

#### 詳細流程

1. **分數與字母互換**
   - 若 x < y，交換 x、y，並將 s 中所有 'a'、'b' 互換，這樣只需考慮一種情境（優先移除 "ab"）。
2. **主迴圈遍歷**
   - 針對每一段連續的 a、b，優先消去所有 "ab"（高分），剩下的 a、b 再消去 "ba"（低分）。
   - 其他字元會分隔區段，a、b 只會在同一區段內配對，不會跨區段。
3. **複雜度分析**
   - 時間複雜度：O(n)，n 為字串長度。每個字元最多被處理兩次（一次進 while，一次配對）。
   - 空間複雜度：O(1)，只需常數額外變數。

#### 主要程式片段

```csharp
public int MaximumGain(string s, int x, int y)
{
    if (x < y)
    {
        (x, y) = (y, x);
        char[] arr = s.ToCharArray();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 'a') arr[i] = 'b';
            else if (arr[i] == 'b') arr[i] = 'a';
        }
        s = new string(arr);
    }
    int res = 0;
    for (int i = 0; i < s.Length; i++)
    {
        int cntA = 0, cntB = 0;
        while (i < s.Length && (s[i] == 'a' || s[i] == 'b'))
        {
            if (s[i] == 'a') cntA++;
            else {
                if (cntA > 0) { cntA--; res += x; }
                else cntB++;
            }
            i++;
        }
        res += Math.Min(cntA, cntB) * y;
    }
    return res;
}
```

#### 註解與流程說明
- 先將分數較高的操作統一為 "ab"，簡化後續邏輯。
- 只在同一段連續 a、b 內配對，不會跨區段。
- 每次遇到 b，若前面有 a 就消去一組 "ab"，否則累加 b。
- while 結束後，剩下的 a、b 盡量配對成 "ba"。

### 關於 `res += Math.Min(cntA, cntB) * y;` 的補充說明

這段程式碼的用意是：
- 在同一段連續的 a、b 處理完所有高分的 "ab" 配對後，若還有剩下 a 和 b，這些 a、b 會呈現所有 a 在前、所有 b 在後的排列。
- 這時可以將剩下的 a 和 b 兩兩配對組成 "ba"，每組可再獲得 y 分。
- 能配對的組數為 `min(cntA, cntB)`。

#### 會成立的範例

- 範例 1：
  - 輸入：s = "aabbbba", x = 5, y = 2
  - 處理過程：
    - 先消去兩組 "ab"，得 10 分，剩下 2 個 b 和 1 個 a
    - 這時剩下的 a、b 可配對 1 組 "ba"，再得 2 分
    - 最終答案：12

- 範例 2：
  - 輸入：s = "aaabb", x = 5, y = 2
  - 處理過程：
    - 先消去兩組 "ab"，得 10 分，剩下 1 個 a 和 2 個 b
    - 剩下的 a、b 可配對 1 組 "ba"，再得 2 分
    - 最終答案：12

#### 注意事項
- 這段程式碼**只會在同一段連續的 a、b 內生效**，不會跨區段配對。
- 若 a、b 不連續（中間有其他字元），則各自區段獨立計算。
- 這樣設計完全符合題目規則，確保每個 a、b 都被最有效率地利用。

#### 範例

- 輸入：s = "aabbb", x = 5, y = 2
- 步驟：
  - 先消去兩組 "ab"，得 10 分，剩下 1 個 b，無法再配對。
  - 最終答案：10

- 輸入：s = "aabbbba", x = 5, y = 2
- 步驟：
  - 先消去兩組 "ab"，得 10 分，剩下 2 個 b 和 1 個 a。
  - 剩下的 a、b 可配對 1 組 "ba"，再得 2 分。
  - 最終答案：12

#### 邊界與特殊情況
- 若 a、b 不連續（中間有其他字元），只會在同一區段內配對，不會跨區段。
- 空字串或無法配對時，答案為 0。

---

## 測試與驗證

`Main` 方法內建多組測試資料，涵蓋一般情境與邊界情形，方便驗證正確性。

---

## 檔案結構

- `Program.cs`：主程式與解法實作，包含詳細註解與測試。
- `README.md`：本說明文件。

---

## 參考資料
- [LeetCode 1717 題目連結](https://leetcode.com/problems/maximum-score-from-removing-substrings/)
- [LeetCode 中文題解](https://leetcode.cn/problems/maximum-score-from-removing-substrings/)

---

## 版權與授權

本專案僅供學習與交流，歡迎參考與改作。
