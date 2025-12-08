# leetcode_1925 - Count Square Sum Triples

此專案包含 LeetCode 題目 1925：計算平方和三元組的數目（Count Square Sum Triples）之兩種實作：一個為優化版本（雙迴圈 + 開平方檢查），另一個為簡單暴力法（三重迴圈）。

題目連結
- LeetCode: https://leetcode.com/problems/count-square-sum-triples/
- 中文（LeetCode 題庫）：https://leetcode.cn/problems/count-square-sum-triples/

問題描述（繁體中文）
----------------
一個平方和三元組 (a, b, c) 指三個整數 a、b、c 滿足 a^2 + b^2 = c^2。
給定整數 n，請回傳所有滿足 1 <= a, b, c <= n 的平方和三元組的數目。

範例
----
- 輸入：n = 5
- 輸出：1（即 (3,4,5)）

解題概念與想法
----------------
我們提供兩種方式：

1) 優化版（雙迴圈 + 平方根檢查）
  - 對於每一對 a 與 b（此實作從 b = a 開始以避免 a, b 的重複計數），計算 c^2 = a^2 + b^2。
  - 使用 Math.Sqrt 取得 c 的近似值，轉成 int 再平方以檢查是否為完整平方數，同時確認 c <= n。
  - 時間複雜度：O(n^2)。
  - 空間複雜度：O(1)。

2) 暴力法（三重迴圈）
  - 直接對 a, b, c 三個變數作三重迴圈，檢查是否滿足 a^2 + b^2 = c^2。
  - 直觀但效率極低，僅用於檢查或教學使用。
  - 時間複雜度：O(n^3)。

程式範例
---------------
專案中的 `Program.cs` 包含兩個實作：
- `CountTriples(n)`: 優化版本，雙迴圈 + 平方根檢查。
- `CountTriples2(n)`: 暴力窮舉，三重迴圈。

使用方法（在 Windows Powershell）
```pwsh
dotnet build ./leetcode_1925/leetcode_1925.csproj -c Debug
dotnet run --project ./leetcode_1925/leetcode_1925.csproj -c Debug
```

比較
-----
- 在 n 較小的情況下，兩者在結果上是一致的（如果 CountTriples 是以 b 從 a 開始，表示不重複計算 (a,b) 的排列；暴力法會計算所有排列）。
- 在效能上，雙迴圈 + 平方根檢查為實務上可接受的解法，而三重迴圈會隨 n 增長而迅速變慢。

注意事項
-----------
- 如果想要將 (a,b) 視為不同的有序組合（例如 (3,4,5) 與 (4,3,5) 視為兩組），可把 CountTriples 的內部 b 從 1 到 n（而非從 a 開始），或者將雙迴圈結果乘以 2（需要注意 a==b 的情況）。

貢獻
----
如果你想要新增更多解法，例如使用生成法（Euclid's formula）來改良效率與精準度，歡迎提出 PR。
