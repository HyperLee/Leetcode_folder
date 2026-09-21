# LeetCode 3524：求出陣列的 X 值 I

這個專案以 .NET 10 console project 示範 LeetCode 3524「Find X Value of Array I」的乘積餘數動態規劃解法，並加入一個只供小型測資驗證的暴力參考解法。

- 題目：[3524. Find X Value of Array I](https://leetcode.com/problems/find-x-value-of-array-i/description/)
- 中文題目：[3524. 求出陣列的 X 值 I](https://leetcode.cn/problems/find-x-value-of-array-i/description/)
- 專案檔：[leetcode_3524/leetcode_3524.csproj](leetcode_3524/leetcode_3524.csproj)
- 主要程式：[leetcode_3524/Program.cs](leetcode_3524/Program.cs)

## 快速開始

請在本 README 所在的專案根目錄執行：

```powershell
dotnet restore .\leetcode_3524\leetcode_3524.csproj
dotnet build .\leetcode_3524\leetcode_3524.csproj --nologo --no-restore
dotnet run --project .\leetcode_3524\leetcode_3524.csproj --no-build --no-restore
```

程式不要求互動式輸入。`Main` 會直接執行固定測試資料，同時驗證正式 DP 解法與暴力參考解法；任一檢查失敗時，程式結束碼會是 `1`。

## 題目說明

給定一個由正整數組成的陣列 `nums`，以及一個正整數 `k`。

可以移除任意不重疊的前綴與後綴，但移除後的陣列必須保持非空。請計算每個餘數 `x` 的操作方式數量：剩餘元素的乘積除以 `k` 後的餘數為 `x`，就將 `result[x]` 加一。

### 從移除操作轉換成連續子陣列

假設移除前綴後從索引 `left` 開始，移除後綴後在索引 `right` 結束，剩餘內容就是：

```text
nums[left..right]
```

因為剩餘內容必須非空，所以 `left <= right`。因此每一種合法操作，都唯一對應到一個非空連續子陣列；反過來，每個非空連續子陣列也都能透過移除它左右兩側的內容得到。

所以題目可以改寫成：

> 統計所有非空連續子陣列的乘積，依照乘積對 `k` 的餘數分組。

## 限制條件

依照官方題目規格：

- `1 <= nums[i] <= 10^9`
- `1 <= nums.length <= 10^5`
- `1 <= k <= 5`

非空連續子陣列最多有：

```text
n × (n + 1) / 2
= 100000 × 100001 / 2
= 5,000,050,000
```

這個數值超過 C# `int` 的上限，因此程式使用 `long[]` 儲存每個餘數的計數。

## 解題概念與出發點

直接列舉每個連續子陣列並計算完整乘積，在 `n = 100000` 時會產生太多區間；而且完整乘積也可能非常大。

這題的關鍵是只保留乘積的餘數。若目前乘積對 `k` 的餘數是 `r`，加入新元素 `value` 後：

```text
(r × value) % k
```

就等於新乘積對 `k` 的餘數。因此不必保存完整乘積，只需要記錄 `0` 到 `k - 1` 的狀態。

由於 `k <= 5`，對每個陣列元素走訪所有餘數狀態的成本很小。

## 方法一：以右端點分組的滾動動態規劃

正式解法是 `ResultArray`。

### 狀態定義

在處理 `nums[i]` 之前：

```text
dp[r] = 以 nums[i - 1] 結尾、乘積除以 k 的餘數為 r 的非空連續子陣列數量
```

處理 `nums[i]` 後，新的 `dp` 只保存「以 `nums[i]` 結尾」的子陣列。

另外使用 `res[r]` 保存目前看過的所有右端點中，餘數為 `r` 的子陣列總數。

### 狀態轉移

每次建立新的 `ndp`：

1. `nums[i]` 自己可以形成新的長度 1 子陣列：

   ```text
   ndp[nums[i] % k] += 1
   ```

2. 將前一輪每個子陣列接上 `nums[i]`：

   ```text
   nextRemainder = (r × nums[i]) % k
   ndp[nextRemainder] += dp[r]
   ```

3. 將本輪所有以 `nums[i]` 結尾的子陣列加入答案：

   ```text
   res[r] += ndp[r]
   ```

程式對應的核心流程如下：

```csharp
long[] ndp = new long[k];
ndp[nums[i] % k]++;

for (int r = 0; r < k; r++)
{
    int nextRemainder = (int)(((long)r * nums[i]) % k);
    ndp[nextRemainder] += dp[r];
}

dp = ndp;

for (int r = 0; r < k; r++)
{
    res[r] += dp[r];
}
```

### 為什麼不會重複計數

每個非空連續子陣列都有唯一的右端點。當掃描到這個右端點時：

- 長度 1 的子陣列由「從目前元素重新開始」建立。
- 長度大於 1 的子陣列由上一個右端點的子陣列延伸而來。

因此每個子陣列只會在自己的右端點被建立一次；將每輪 `dp` 加入 `res`，就能完整統計所有合法操作。

### DP 範例：`nums = [1, 2, 3]`、`k = 3`

狀態索引依序為餘數 `0、1、2`：

| 處理索引 `i` | `nums[i]` | 本輪 `dp[0], dp[1], dp[2]` | 累計 `res[0], res[1], res[2]` |
| ---: | ---: | --- | --- |
| 0 | 1 | `[0, 1, 0]` | `[0, 1, 0]` |
| 1 | 2 | `[0, 0, 2]` | `[0, 1, 2]` |
| 2 | 3 | `[3, 0, 0]` | `[3, 1, 2]` |

逐輪原因：

- `i = 0`：只有 `[1]`，乘積餘數為 `1`。
- `i = 1`：`[2]` 與 `[1,2]` 的乘積餘數都是 `2`。
- `i = 2`：`[3]`、`[2,3]`、`[1,2,3]` 的乘積餘數都是 `0`。

最後所有子陣列的分布是：

```text
[1]       -> 1
[1, 2]    -> 2
[1, 2, 3] -> 0
[2]       -> 2
[2, 3]    -> 0
[3]       -> 0

result = [3, 1, 2]
```

### 複雜度

- 時間：`O(n × k)`。每個元素都掃描 `k` 個餘數狀態。
- 額外空間：`O(k)`。使用 `dp` 與 `ndp` 兩個滾動陣列；`res` 是長度為 `k` 的輸出陣列。

## 方法二：列舉所有連續子陣列

`BruteForceResultArray` 是直觀的參考實作，只用於 `Main` 的小型固定案例。

### 設計流程

使用 `start` 固定左端點，再使用 `end` 向右延伸：

1. 每次固定新的 `start` 時，將乘積餘數初始化為 `1 % k`。
2. `end` 每向右移動一格，就把 `nums[end]` 加入目前乘積。
3. 立刻將新的餘數對應的計數加一。
4. 完成一個 `start` 後，再換下一個左端點。

偽代碼：

```text
for start = 0..n-1
    productRemainder = 1 % k
    for end = start..n-1
        productRemainder = productRemainder × nums[end] % k
        result[productRemainder] += 1
```

初始化使用 `1 % k`，因此 `k = 1` 時仍然會得到合法的餘數 `0`。

### 暴力法範例：`nums = [1, 2, 3]`、`k = 3`

```text
start = 0:
  [1]       -> 1，result[1] += 1
  [1, 2]    -> 2，result[2] += 1
  [1, 2, 3] -> 0，result[0] += 1

start = 1:
  [2]       -> 2，result[2] += 1
  [2, 3]    -> 0，result[0] += 1

start = 2:
  [3]       -> 0，result[0] += 1

最後 result = [3, 1, 2]
```

這個流程直接列出每個合法剩餘陣列，所以很適合拿來驗證 DP 是否遺漏或重複計數。

### 複雜度

- 時間：`O(n²)`，共有 `n × (n + 1) / 2` 個非空連續子陣列。
- 額外空間：`O(k)`，只保存餘數計數與目前乘積餘數。
- 用途限制：官方 `n` 最大為 `100000`，因此此方法只適合小型測資與教學對照，不是正式提交解法。

## 兩種方法比較

| 方法 | 核心想法 | 時間複雜度 | 額外空間 | 用途 |
| --- | --- | --- | --- | --- |
| `ResultArray` | 以右端點分組，使用乘積餘數做滾動 DP | `O(n × k)` | `O(k)` | 符合官方限制的正式解法 |
| `BruteForceResultArray` | 列舉每個 `[start..end]` 並逐步更新餘數 | `O(n²)` | `O(k)` | 小型測資的直觀參考與交叉驗證 |

## 可執行驗證

目前沒有額外的單元測試專案，因此 `Main` 是這個 console project 的可執行 smoke test。固定資料包含三個官方案例，以及單元素與 `k = 1` 邊界案例；每個案例由兩種方法各驗證一次，共 `10` 項檢查。

### 測試案例

| 案例 | `nums` | `k` | 預期結果 | 覆蓋重點 |
| ---: | --- | ---: | --- | --- |
| 1 | `[1,2,3,4,5]` | 3 | `[9,2,4]` | 官方範例、一般連續子陣列計數 |
| 2 | `[1,2,4,8,16,32]` | 4 | `[18,1,2,0]` | 官方範例、餘數 0 的大量狀態 |
| 3 | `[1,1,2,1,1]` | 2 | `[9,6]` | 官方範例、重複值與兩種餘數 |
| 4 | `[7]` | 5 | `[0,0,1,0,0]` | 最小陣列長度與單一非空子陣列 |
| 5 | `[1,1,1]` | 1 | `[6]` | 最小 `k` 與全部 6 個子陣列落在餘數 0 |

### 實際執行輸出

執行：

```powershell
dotnet run --project .\leetcode_3524\leetcode_3524.csproj --no-build --no-restore
```

輸出：

```text
Case 1: nums = [1, 2, 3, 4, 5], k = 3, Expected = [9, 2, 4]
  ResultArray (DP): Actual = [9, 2, 4], Result = PASS
  BruteForceResultArray: Actual = [9, 2, 4], Result = PASS
Case 2: nums = [1, 2, 4, 8, 16, 32], k = 4, Expected = [18, 1, 2, 0]
  ResultArray (DP): Actual = [18, 1, 2, 0], Result = PASS
  BruteForceResultArray: Actual = [18, 1, 2, 0], Result = PASS
Case 3: nums = [1, 1, 2, 1, 1], k = 2, Expected = [9, 6]
  ResultArray (DP): Actual = [9, 6], Result = PASS
  BruteForceResultArray: Actual = [9, 6], Result = PASS
Case 4: nums = [7], k = 5, Expected = [0, 0, 1, 0, 0]
  ResultArray (DP): Actual = [0, 0, 1, 0, 0], Result = PASS
  BruteForceResultArray: Actual = [0, 0, 1, 0, 0], Result = PASS
Case 5: nums = [1, 1, 1], k = 1, Expected = [6]
  ResultArray (DP): Actual = [6], Result = PASS
  BruteForceResultArray: Actual = [6], Result = PASS
Summary: 10/10 checks passed.
```

## 建置、格式與差異檢查

從專案根目錄執行：

```powershell
dotnet restore .\leetcode_3524\leetcode_3524.csproj
dotnet build .\leetcode_3524\leetcode_3524.csproj --nologo --no-restore
dotnet run --project .\leetcode_3524\leetcode_3524.csproj --no-build --no-restore
git diff --check
```

題目 XML 依需求保留原始文字，其中包含未跳脫的 `<=` 比較符號，因此不另外啟用 XML 文件產生器；一般建置仍會驗證程式碼，XML summary 則依原始題目區塊保留規則與新增方法文件逐段檢視。`git diff --check` 用來確認差異中沒有尾端空白或其他空白問題。

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_3524/
    ├── Program.cs
    └── leetcode_3524.csproj
```

- `leetcode_3524/Program.cs`：題目 XML、DP 正式解法、暴力參考解法與固定測試入口。
- `leetcode_3524/leetcode_3524.csproj`：.NET 10 console project 設定。
- `docs/readme-template.md`：README 初始建立時採用的文件結構指引。
- `bin/` 與 `obj/`：建置產物，不納入版本控制。
