# LeetCode 1232：Check If It Is a Straight Line／綴點成線

這個 .NET 10 主控台專案使用向量叉積判斷所有座標是否共線。公開方法 `CheckStraightLine(int[][] coordinates)` 不修改輸入也不輸出文字；`Main` 統一負責八個確定性驗收案例、PASS/FAIL 統計與失敗結束碼。

## 題目連結

- [LeetCode（英文）](https://leetcode.com/problems/check-if-it-is-a-straight-line/)
- [LeetCode（中文）](https://leetcode.cn/problems/check-if-it-is-a-straight-line/)

## 題目敘述與限制

給定二維平面上一組互不重複的座標點 `coordinates`，若所有點都位於同一直線上則回傳 `true`，否則回傳 `false`。

- `2 <= coordinates.Length <= 1000`
- `coordinates[i].Length == 2`
- `-10^4 <= coordinates[i][0], coordinates[i][1] <= 10^4`
- `coordinates` 不包含重複座標

## 叉積不變量

以前兩點建立基準向量：

`baseline = (x1 - x0, y1 - y0)`

對每個後續點 `i`，建立從第一點出發的目前向量：

`current = (xi - x0, yi - y0)`

兩向量平行時二維叉積為零，因此共線條件可寫成：

`baselineX * currentY == baselineY * currentX`

迴圈會逐點維持「已檢查的點都與前兩點共線」這項不變量；任一叉積不相等即可立即回傳 `false`。

## 為何不用斜率除法

以斜率 `deltaY / deltaX` 比較會遇到垂直線的除以零問題，若使用浮點數也可能受精度誤差影響。交叉相乘不需要除法，可用同一個判斷處理垂直線、水平線及正負斜率。

座標差值可能達到 `20,000`；實作先將差值保存為 `long` 再相乘，避免中間乘積受 `int` 範圍限制。方法只讀取輸入座標，不排序也不改寫陣列。

## 實作設計與複雜度

`CheckStraightLine` 先計算第一點到第二點的基準向量，再從第三點開始逐一比較叉積。兩點必然能形成直線，因此最小輸入自然通過，不需要額外分支。

- 時間複雜度：`O(n)`，每個座標最多檢查一次。
- 輔助空間：`O(1)`，只保存固定數量的向量分量與索引。

## 範例推演

以 `[[1, 2], [2, 3], [3, 4]]` 為例：

1. 基準向量為 `(2 - 1, 3 - 2) = (1, 1)`。
2. 第一點到第三點的向量為 `(3 - 1, 4 - 2) = (2, 2)`。
3. 交叉比較為 `1 * 2 == 1 * 2`，成立，因此第三點與基準線共線。

## 驗收案例

專案沒有正式測試專案或獨立測試框架；可執行檔內的確定性 harness 是驗證機制。

| 案例 | 輸入 | 預期 | 驗證重點 |
| --- | --- | --- | --- |
| 1. Official true | `[[1,2],[2,3],[3,4],[4,5],[5,6],[6,7]]` | `true` | 官方共線範例 |
| 2. Official false | `[[1,1],[2,2],[3,4],[4,5],[5,6],[7,7]]` | `false` | 官方不共線範例 |
| 3. Minimum input | `[[0,0],[3,-7]]` | `true` | 兩點最小輸入 |
| 4. Vertical line | `[[2,-5],[2,0],[2,8],[2,10000]]` | `true` | 垂直線與除以零風險 |
| 5. Horizontal line | `[[-10000,7],[0,7],[10000,7]]` | `true` | 水平線與座標邊界 |
| 6. Negative slope | `[[-3,3],[-1,1],[1,-1],[3,-3]]` | `true` | 負斜率 |
| 7. Late deviation | `[[0,0],[1,1],[2,2],[3,3],[4,5]]` | `false` | 最後一點偏離 |
| 8. Maximum length | `x = -500..499`、`y = 2*x + 1` 的 1000 個不同點 | `true` | 最大點數與精簡輸出 |

## 建置與執行

從題目根目錄 `leetcode_1232/` 執行：

```bash
dotnet build leetcode_1232/leetcode_1232.csproj --nologo
dotnet run --no-build --project leetcode_1232/leetcode_1232.csproj
```

使用 `--no-build` 前請先完成建置。不要執行裸的 `dotnet build` 或 `dotnet test`，因為題目根目錄沒有根專案、solution 或正式測試專案。

## 實際驗證輸出

以下內容直接取自成功執行結果：

```text
LeetCode 1232 acceptance harness

Case 1: Official true
Input: [[1, 2], [2, 3], [3, 4], [4, 5], [5, 6], [6, 7]]
Expected: true
Actual: true
PASS

Case 2: Official false
Input: [[1, 1], [2, 2], [3, 4], [4, 5], [5, 6], [7, 7]]
Expected: false
Actual: false
PASS

Case 3: Minimum input
Input: [[0, 0], [3, -7]]
Expected: true
Actual: true
PASS

Case 4: Vertical line
Input: [[2, -5], [2, 0], [2, 8], [2, 10000]]
Expected: true
Actual: true
PASS

Case 5: Horizontal line
Input: [[-10000, 7], [0, 7], [10000, 7]]
Expected: true
Actual: true
PASS

Case 6: Negative slope
Input: [[-3, 3], [-1, 1], [1, -1], [3, -3]]
Expected: true
Actual: true
PASS

Case 7: Late deviation
Input: [[0, 0], [1, 1], [2, 2], [3, 3], [4, 5]]
Expected: false
Actual: false
PASS

Case 8: Maximum length
Input: 1000 distinct points: x = -500..499, y = 2*x + 1
Expected: true
Actual: true
PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1232/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1232/
    ├── Program.cs
    └── leetcode_1232.csproj
```
