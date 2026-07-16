# 1423. Maximum Points You Can Obtain from Cards（可獲得的最大點數）

[LeetCode 題目](https://leetcode.com/problems/maximum-points-you-can-obtain-from-cards/) · [力扣中文題目](https://leetcode.cn/problems/maximum-points-you-can-obtain-from-cards/)

給定一列卡牌點數，每次只能從最左端或最右端取一張，恰好取出 `k` 張後，求可取得的
最大總點數。本專案使用 .NET 10 主控台程式與補集滑動視窗，並由 `Main` 執行八個
確定性 acceptance checks。

## 題目與限制

公開 API 為 `public static int MaxScore(int[] cardPoints, int k)`。解法不輸出且不修改
輸入，只處理 LeetCode 保證的有效資料：

- `1 <= cardPoints.length <= 100,000`。
- `1 <= cardPoints[i] <= 10,000`。
- `1 <= k <= cardPoints.length`。

## 解法：最小補集滑動視窗

從兩端恰好取走 `k` 張後，未取走的卡牌必然是長度 `n-k` 的連續中段。全部卡牌總和
固定，因此讓這個中段的點數總和最小，就等價於讓兩端取走的點數總和最大。

演算法先計算全部點數，再以固定長度 `n-k` 的視窗由左向右掃描。每次右移只加入一張
新卡並移除一張舊卡，因此不必重算整個視窗。當 `k == n` 時，中段長度為零，直接
回傳全部總和。

### 容易出錯的地方

- 視窗長度是「未取走」的 `n-k`，不是取牌數 `k`。
- 視窗右移時，移除位置為 `right - windowLength`，不能多移或少移一格。
- `k == n` 會得到零長度視窗，必須避免使用不存在的視窗元素。
- 公開方法只計算結果，不能為了驗證而修改輸入或輸出主控台。

### 逐步走查

以 `cardPoints = [10,1,1,10,1]`、`k = 3` 為例，未取走視窗長度為 `2`：

```plaintext
全部總和：23
長度 2 的連續視窗總和：11、2、11、11
最小未取走總和：2
最大可取點數：23 - 2 = 21
```

## 複雜度

令 `n = cardPoints.Length`：

- 時間複雜度：`O(n)`；計算總和與掃描視窗各為線性時間。
- 結果空間複雜度：`O(1)`；只回傳一個整數。
- 輔助空間複雜度：`O(1)`；只保存總和、視窗長度與視窗總和。

## 八項 acceptance checks

| 案例 | 預期 | 驗證目的 |
| --- | ---: | --- |
| 官方範例一 `[1,2,3,4,5,6,1]`, `k=3` | `12` | 一般滑動視窗。 |
| 官方範例二 `[2,2,2]`, `k=2` | `4` | 重複點數。 |
| 官方範例三，`k` 等於長度 | `55` | 官方零長度補集視窗。 |
| 單張卡牌 | `42` | 最小有效輸入。 |
| `k=1` 且中間值最大 | `5` | 只能比較兩端，不能誤取中間。 |
| 左右混合且檢查輸入不變 | `21` | 混合取牌最佳解與純函式契約。 |
| 四張全部取走 | `9` | 額外的零長度視窗回歸。 |
| 100,000 張、`k=50,000` | `500,000` | 長度上限與大總和。 |

每個案例都印出名稱、Input、Expected、Actual 與 PASS/FAIL；任一失敗會設定
`Environment.ExitCode = 1`。

## 在本題目根目錄建置與執行

本資料夾沒有獨立 solution 或正式測試專案。請在外層 `leetcode_1423/` 內使用明確
的巢狀 project 路徑：

```bash
dotnet build leetcode_1423/leetcode_1423.csproj --nologo
dotnet run --no-build --project leetcode_1423/leetcode_1423.csproj
```

VS Code 開啟外層 `leetcode_1423/` 後，選擇 `Debug leetcode_1423`；它會先執行
`build leetcode_1423`，再啟動 .NET 10 DLL。

## 已驗證的執行輸出

下列內容來自新鮮執行 `dotnet run --no-build --project leetcode_1423/leetcode_1423.csproj`：

```text
LeetCode 1423 acceptance harness

Case 1: official example 1
Input: cardPoints = [1,2,3,4,5,6,1], k = 3
Expected: 12
Actual: 12
PASS

Case 2: official example 2
Input: cardPoints = [2,2,2], k = 2
Expected: 4
Actual: 4
PASS

Case 3: official example 3
Input: cardPoints = [9,7,7,9,7,7,9], k = 7
Expected: 55
Actual: 55
PASS

Case 4: single card
Input: cardPoints = [42], k = 1
Expected: 42
Actual: 42
PASS

Case 5: k equals one
Input: cardPoints = [5,100,2], k = 1
Expected: 5
Actual: 5
PASS

Case 6: mixed ends and input preservation
Input: cardPoints = [10,1,1,10,1], k = 3
Expected: 21; input unchanged: True
Actual: 21; input unchanged: True
PASS

Case 7: k equals card count
Input: cardPoints = [3,1,4,1], k = 4
Expected: 9
Actual: 9
PASS

Case 8: maximum length
Input: cardPoints = 100,000 cards [1,1,1,...,10,10,10], k = 50000
Expected: 500000
Actual: 500000
PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1423/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/
│       └── specs/
└── leetcode_1423/
    ├── leetcode_1423.csproj
    └── Program.cs
```

舊式 `leetcode_1423.sln`、`App.config` 與 `Properties/AssemblyInfo.cs` 已移除。
