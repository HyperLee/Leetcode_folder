# leetcode_3304

## 題目：3304. 找出第 K 個字符 I (Find the K-th Character in String Game I)

### 題目描述

Alice 和 Bob 正在玩一個遊戲。最初，Alice 有一個字串 word = "a"。你給定一個正整數 k。

現在 Bob 會要求 Alice 永遠執行以下操作：
- 將 word 中的每個字元變為英文字母表中的下一個字元 (z 變 a)，然後將其附加到原始 word 之後。
- 例如，對 "c" 執行操作會生成 "cd"，對 "zb" 執行操作會生成 "zbac"。

請返回經過足夠多次操作後，word 至少有 k 個字元時的第 k 個字元。

[LeetCode 題目連結 (英文)](https://leetcode.com/problems/find-the-k-th-character-in-string-game-i/)
[LeetCode 題目連結 (中文)](https://leetcode.cn/problems/find-the-k-th-character-in-string-game-i/)

---

## 專案結構

- `Program.cs`：主程式與解題邏輯，包含兩種解法（數學反推法、bitCount 高效法）。
- `.vscode/launch.json`、`.vscode/tasks.json`：VS Code 偵錯與建構設定。
- `leetcode_3304.csproj`：C# 專案檔。

---

## 解題思路

1. **數學反推法**
   - 每次操作後字串長度倍增，後半段是前半段每個字元 + 1 (z 變 a)。
   - 目標：找出第 k 個字元是由哪一層、哪個位置推導而來。
   - 反推過程：
     - 觀察每一層的長度都是 2 的冪次 (1, 2, 4, 8...)。
     - 透過計算 k 的最高位 2 的冪次 t，判斷 k 屬於哪一層。
     - 若 k 剛好是 2 的冪，需特殊處理 (t--)，回溯到上一層的最後一個字元。
     - 每回溯一次，代表經過一次「+1」操作。
     - 直到 k 回到 1，累計經過幾次「+1」，最後用 'a' 加上次數 (mod 26) 得到答案。
   - 適合理解字串生成規律，數學推導清楚。
   - 時間複雜度 O (log k)。

2. **bitCount 高效法**
   - 利用 (k-1) 的二進位 1 的個數，直接計算第 k 個字元。
   - 原理：每個 1 代表一次進入後半段 (即一次「+1」運算)，這是因為字串生成過程本質上是二分結構。
   - 只需計算 (k-1) 的二進位有幾個 1，'a' 經過這麼多次「+1」即可。
   - **為什麼是 k-1？**
     - 因為初始字串的第一個字元 (k=1) 就是 'a'，尚未經過任何「+1」運算。
     - 從第 2 個位置開始，才會因為進入後半段而產生「+1」效果。
     - 所以要計算第 k 個字元經過幾次「+1」，其實是看 k-1 這個數的二進位 1 的個數。
     - 這樣才能正確對應到每個字元的來源與運算次數。
   - 不需顯式回溯層級，直接位元運算，程式碼極為簡潔。
   - 時間複雜度 O (1) \~ O (log k)。

---

### 兩種解法差異對比

| 解法           | 思路                   | 運算方式         | 直觀性 | 效率 |
|----------------|------------------------|------------------|--------|------|
| 數學反推法     | 層級回溯、數學推導     | 迴圈+log+減法    | 高     | O(log k) |
| bitCount 高效法| 位元運算、結構觀察     | 二進位 1 的個數   | 中     | O(1)~O(log k) |

- 數學反推法適合理解字串生成規律，推導過程清楚，適合學習與面試講解。
- bitCount 法則是利用位元結構的巧思，程式碼最短、效率極高，適合實戰快速 AC。

---

## 執行方式

1. 於專案根目錄執行：
   ```
   dotnet build
   dotnet run --project leetcode_3304/leetcode_3304.csproj
   ```
2. 或直接於 VS Code 按 F5 偵錯。

---

## 測試資料
main 函式內建多組測資，涵蓋小 k、大 k、跨越 26、52 等情境。

---

## 參考連結
- [LeetCode 官方討論](https://leetcode.cn/problems/find-the-k-th-character-in-string-game-i/solutions/3708678/zhao-chu-di-k-ge-zi-fu-i-by-leetcode-sol-9epa/)
- [O(1) 寫法討論](https://leetcode.cn/problems/find-the-k-th-character-in-string-game-i/solutions/2934326/o1-zuo-fa-yi-xing-dai-ma-jie-jue-pythonj-zgqh/)

---

## 版權聲明
本專案僅供學習與 LeetCode 題解參考用途。
