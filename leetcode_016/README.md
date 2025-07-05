# leetcode_016 最接近的三數之和

## 題目說明
給定一個長度為 n 的整數陣列 `nums` 和一個整數 `target`，請你從 `nums` 中找出三個整數，使得它們的和最接近 `target`。
返回這三個整數的和。
你可以假設每組輸入只會對應一個答案。

- 題目連結：[LeetCode 16. 3Sum Closest](https://leetcode.com/problems/3sum-closest/description/)
- 中文連結：[LeetCode CN 16. 最接近的三數之和](https://leetcode.cn/problems/3sum-closest/description/)

## 專案結構
```
leetcode_016.sln                # Visual Studio 解決方案檔
leetcode_016/
  leetcode_016.csproj           # C# 專案檔
  Program.cs                    # 主程式與解題邏輯
  ...
```

## 程式說明
- 主程式在 `Program.cs`，包含 `ThreeSumClosest` 函式與測試資料。
- 使用排序 + 雙指針法，時間複雜度 O(n^2)。
- 已內建多組測試資料，執行後會自動印出結果。

## 如何執行
1. 需安裝 [.NET 8 SDK](https://dotnet.microsoft.com/zh-tw/download/dotnet/8.0)
2. 在終端機執行：
   ```sh
   dotnet build
   dotnet run --project leetcode_016/leetcode_016.csproj
   ```
3. 或於 VS Code 直接按 F5 進行偵錯。

## 偵錯與開發
- `.vscode/launch.json` 及 `tasks.json` 已設定好 VS Code 偵錯與建構任務。
- 可直接於 VS Code 下中斷點進行 step-by-step 偵錯。

## 主要檔案說明
- `Program.cs`：主程式與解題邏輯，含詳細註解與測試資料。
- `.vscode/launch.json`、`.vscode/tasks.json`：VS Code 偵錯與建構設定。
- `.editorconfig`：C# 程式碼風格設定。
- `.gitignore`：Git 版本控制忽略規則。

## 解題流程詳細步驟

1. **排序陣列**
   - 先將輸入的整數陣列 `nums` 進行排序，這樣可以方便後續使用雙指針技巧。
   - 例如：`nums = [-1, 2, 1, -4]` 排序後為 `[-4, -1, 1, 2]`。

2. **遍歷每個元素作為第一個數字 a**
   - 使用 for 迴圈，從頭到尾依序選定每個元素作為三數之和的第一個數字 a。
   - 若遇到與前一個元素相同的 a，則跳過，避免重複計算。

3. **雙指針尋找剩下兩個數字 b, c**
   - 對於每個 a，設置兩個指針：
     - j 指向 a 右邊的第一個元素（i+1）
     - k 指向陣列的最後一個元素（n-1）
   - 進入 while 迴圈，只要 j < k 就持續進行：
     1. 計算三數和 sum = nums[i] + nums[j] + nums[k]
     2. 若 sum 恰好等於 target，直接回傳 target（最佳解）
     3. 若 sum 與 target 的距離比目前最佳答案更近，則更新答案
     4. 若 sum > target，代表總和太大，k 向左移動，並跳過重複元素
     5. 若 sum < target，代表總和太小，j 向右移動，並跳過重複元素

4. **跳過重複元素**
   - 當移動 j 或 k 指針時，若遇到與前一個相同的數字，則持續移動，直到遇到不同的數字，避免重複計算。

5. **回傳最接近 target 的三數和**
   - 若所有組合都遍歷完畢，則回傳目前找到最接近 target 的三數和。

---

### 範例流程說明
以 `nums = [-1, 2, 1, -4]`，`target = 1` 為例：
1. 排序後：`[-4, -1, 1, 2]`
2. 以 a = -4 為起點，j = -1, k = 2，計算 sum = -4 + (-1) + 2 = -3
   - -3 與 1 的距離為 4，更新答案
   - sum < target，j 右移
   - j = 1, k = 2，sum = -4 + 1 + 2 = -1
   - -1 與 1 的距離為 2，更新答案
   - sum < target，j 右移，j == k，結束
3. 以 a = -1 為起點，j = 1, k = 2，sum = -1 + 1 + 2 = 2
   - 2 與 1 的距離為 1，更新答案
   - sum > target，k 左移，j == k，結束
4. 以 a = 1 為起點，j = 2, k = 2，j == k，結束
5. 最終回傳答案 2

---

此流程可保證在 O(n^2) 時間內找到唯一解，並有效避免重複組合與提升效率。

---
本專案僅用於 LeetCode 題目解題與 C# 練習，歡迎參考與討論。
