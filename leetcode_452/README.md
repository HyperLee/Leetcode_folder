# LeetCode 452：Minimum Number of Arrows to Burst Balloons（用最少數量的箭引爆氣球）

這是一個以 C# 撰寫的 .NET 10 主控台專案。公開的
`FindMinArrowShots(int[][] points)` 會依區間右端點進行原地排序，再以貪心策略
求出引爆全部氣球所需的最少箭數；`Main` 則提供可重複執行的 acceptance harness。

- [英文題目：452. Minimum Number of Arrows to Burst Balloons](https://leetcode.com/problems/minimum-number-of-arrows-to-burst-balloons/)
- [中文題目：452. 用最少數量的箭引爆氣球](https://leetcode.cn/problems/minimum-number-of-arrows-to-burst-balloons/)

## 題目說明

牆面上的每個氣球以水平直徑 `points[i] = [xStart, xEnd]` 表示。一支沿正 y 軸垂直
射出的箭，若其 x 座標滿足 `xStart <= x <= xEnd`，即可引爆該氣球，且箭會持續飛行並
引爆路徑上的其他氣球。請回傳引爆所有氣球所需的最少箭數。

## 限制條件

- `1 <= points.length <= 10^5`
- `points[i].length == 2`
- `-2^31 <= xStart < xEnd <= 2^31 - 1`

實作只處理 LeetCode 定義的有效、非空二元素區間；不另外定義無效輸入的例外或回傳
規則。

## 右端點貪心不變量

每一輪都在尚未引爆的氣球中，選擇右端點最小者的右端點作為箭的位置。這個位置能
引爆目前氣球，也不會比任何更靠右的選擇限制更多後續氣球；因此保留最大的後續相容
空間。

1. 以每個區間的右端點遞增排序。
2. 第一支箭放在排序後第一個區間的右端點 `arrowPosition`。
3. 若下一個區間的左端點 `<= arrowPosition`，現有箭仍能引爆它，不必新增箭。
4. 若左端點 `> arrowPosition`，它已在現有箭右側，必須新增一支箭並將位置更新為
   該區間的右端點。

這也是程式使用嚴格 `>` 判斷的原因：例如 `[1, 2]` 與 `[2, 3]` 共用端點 `2`，一支
射在 `2` 的箭即可引爆兩者。

`Array.Sort` 會就地重新排列傳入的 `points`。這使公開 API 不需配置額外的區間副本，
但呼叫端若要保留原始順序，必須自行複製輸入；acceptance harness 在每個案例呼叫前
皆以 `ClonePoints` 深複製資料。

## 巢狀區間走查

以 `[[1, 10], [2, 3], [4, 5]]` 為例。若錯誤地先保留長區間 `[1, 10]`，可能會以為
一支箭足夠；排序後可看出短區間沒有共同交集。

| 依右端點排序後 | 現有 `arrowPosition` | 動作 | 箭數 |
| --- | ---: | --- | ---: |
| `[2, 3]` | — | 第一支箭射在 `3` | 1 |
| `[4, 5]` | 3 | `4 > 3`，新增箭並射在 `5` | 2 |
| `[1, 10]` | 5 | `1 <= 5`，第二支箭可命中 | 2 |

因此最少需要兩支箭。

## 複雜度

- 時間複雜度：`O(n log n)`，主要來自依右端點排序；後續掃描為 `O(n)`。
- 結果空間：`O(1)`，只維護箭數與目前箭位置。
- 輔助空間：除排序實作所需的堆疊空間外為 `O(1)`；不會建立與輸入長度等比例的
  演算法副本。

## 可執行驗證案例

`Main` 共執行八項確定性檢查。每個案例都會列出輸入摘要、預期值、實際值與 PASS/FAIL；
任一失敗都會設定非零 exit code。

| 案例 | 預期最少箭數 | 驗證目的 |
| --- | ---: | --- |
| 官方範例 1 | 2 | 重疊區間可由兩支箭引爆 |
| 官方範例 2 | 4 | 全部互斥時每個氣球都需要一支箭 |
| 官方範例 3 | 2 | 共用端點仍可由同一支箭引爆 |
| 單一座標極值區間 | 1 | 有效最小輸入與完整 `int` 範圍 |
| 端點鏈沒有共同交集 | 2 | 防止把相鄰重疊鏈誤判成一支箭 |
| 巢狀區間與短鏈 | 2 | 驗證最早右端點的貪心選擇 |
| 100000 個相同極值區間 | 1 | 題目長度上限與完全重疊 |
| 100000 個互斥區間 | 100000 | 題目長度上限與每輪新增箭 |

## 建置與執行

請從此 README 所在的外層 `leetcode_452` 目錄執行：

```bash
dotnet build leetcode_452/leetcode_452.csproj --nologo
dotnet run --no-build --project leetcode_452/leetcode_452.csproj
```

以下是完成建置後執行第二個命令的完整輸出：

```text
LeetCode 452 acceptance harness

Case 1: Official example 1
Input: [[10, 16], [2, 8], [1, 6], [7, 12]]
PASS | Minimum arrows | Expected: 2 | Actual: 2

Case 2: Official example 2
Input: [[1, 2], [3, 4], [5, 6], [7, 8]]
PASS | Minimum arrows | Expected: 4 | Actual: 4

Case 3: Official example 3: touching endpoints
Input: [[1, 2], [2, 3], [3, 4], [4, 5]]
PASS | Minimum arrows | Expected: 2 | Actual: 2

Case 4: Single interval at coordinate limits
Input: [[int.MinValue, int.MaxValue]]
PASS | Minimum arrows | Expected: 1 | Actual: 1

Case 5: Endpoint chain has no global overlap
Input: [[int.MinValue, -1], [-1, 0], [0, int.MaxValue]]
PASS | Minimum arrows | Expected: 2 | Actual: 2

Case 6: Nested interval cannot replace short chain
Input: [[1, 10], [2, 3], [4, 5]]
PASS | Minimum arrows | Expected: 2 | Actual: 2

Case 7: Maximum identical intervals
Input: 100000 identical [int.MinValue, int.MaxValue] intervals
PASS | Minimum arrows | Expected: 1 | Actual: 1

Case 8: Maximum disjoint intervals
Input: 100000 disjoint intervals [2i, 2i + 1]
PASS | Minimum arrows | Expected: 100000 | Actual: 100000

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig              # C# 與結構化檔案的格式規範
├── .gitattributes             # 文字與二進位檔案屬性
├── .gitignore                 # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json            # 直接偵錯 net10.0 輸出
│   └── tasks.json             # 預設建置工作
├── docs/
│   └── readme-template.md     # 初次建立 README 的範本
├── leetcode_452/
│   ├── Program.cs             # 右端點貪心解法與可執行驗證器
│   ├── leetcode_452.csproj    # .NET 10 SDK 專案設定
│   └── 圖解座標.xls             # 歷史教學圖解資產
├── AGENTS.md                  # 本題協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
