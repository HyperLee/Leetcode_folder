# LeetCode 837 - 新 21 點 (New 21 Game)

專案說明
--
這個小專案包含對 LeetCode 題目「837. 新 21 點 (New 21 Game)」的 C# 解答（見 `Program.cs`）。README 提供題目摘要、詳細解法說明、複雜度分析與如何編譯與執行範例。

問題摘要
--
Alice 從 0 分開始，只要分數小於 k，就持續抽牌。每次抽牌隨機獲得 [1, maxPts] 的整數（等機率）。當分數達到 k 或以上時停止。請回傳 Alice 最終分數不超過 n 的機率。

解法概覽（高階）
--
採用動態規劃 (DP) 與滑動窗口優化。

- 定義 dp[i] 為「當前分數為 i 時，最終分數落在 [k, n] 的機率」。
- 當 i >= k 且 i <= n 時，dp[i] = 1（已停止且分數合法）。
- 當 i > n 時，dp[i] = 0（已停止但分數超過 n）。
- 當 i < k 時，會從 i 等概率地抽到 1..maxPts，轉移方程為：

  dp[i] = (dp[i+1] + dp[i+2] + ... + dp[i+maxPts]) / maxPts

直接計算上述和會導致 O(n * maxPts) 的時間複雜度；使用滑動窗口累加可以把時間複雜度降到 O(n)。

詳細推導與實作要點
--
1) 邊界條件
- 當 k = 0：Alice 不會抽牌，分數固定為 0，若 0 <= n 則機率為 1。
- 當 n >= k + maxPts - 1：理論上所有可能的最終分數都 <= n，答案為 1（可由 DP 與邊界快速判斷）。

2) DP 定義與遞推
- dp[i] 表示「從 i 開始，最終落在 [k, n] 的機率」。
- 當 i >= k 且 i <= n，dp[i] = 1；當 i > n，dp[i] = 0。
- 對於 i < k，因為下一步會變成 i + x (x in [1, maxPts])，因此：

  dp[i] = (1 / maxPts) * sum_{x=1..maxPts} dp[i + x]

3) 滑動窗口優化
- 在倒序計算 dp（從 n 到 0）時，令 sum = dp[i+1] + dp[i+2] + ... + dp[i+maxPts]，那麼 dp[i] = sum / maxPts。
- 當 i 下降一個時，更新 sum += dp[i] （將新計算的 dp[i] 加入窗口），並在需要時移除 dp[i+maxPts]。

4) 數值精度
- 本題要求 1e-5 的精度；使用 double 足以。累加時注意數值穩定性，但在本題規模下通常沒問題。

時間與空間複雜度
--
- 時間：O(n)（每個 i 只被常數次處理）
- 空間：O(n)（保存 dp 陣列）

範例
--
主程式 `Program.cs` 中提供測試：

- n=21, k=17, maxPts=10 => 約 0.73278
- n=6, k=1, maxPts=10 => 輸出
- n=10, k=1, maxPts=10 => 輸出

如何編譯與執行
--
以下指令適用於 macOS，有安裝 .NET SDK 時：

```bash
dotnet build leetcode_837/leetcode_837.csproj
dotnet run --project leetcode_837/leetcode_837.csproj
```

進一步改進（可選）
--
- 使用 O(k) 空間優化：由於 dp[i] 只依賴於接下來最多 maxPts 的狀態，可用環形緩衝區將空間降至 O(maxPts)（實作較複雜，尚未包含）。
- 針對極大 n 的特殊數學推導：在某些邊界可直接用封閉形式解，但複雜度與收益因案例而異。

檔案說明
--
- `leetcode_837/Program.cs`：C# 實作與測試。

作者/來源
--
此實作參考 LeetCode 討論與數位教學資源，並加入滑動窗口優化以提高效能。
