# 3876. Construct Uniform Parity Array II

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)

這是一個使用 C# 與 .NET 10 撰寫的 LeetCode 3876 解題範例。專案保留
`UniformArray`、`UniformArray2` 與 `UniformArray3` 三種寫法，並在 `Main`
提供不需要輸入資料的固定案例，可直接比較三種解法的 PASS/FAIL 結果。

- 題目：[Construct Uniform Parity Array II](https://leetcode.com/problems/construct-uniform-parity-array-ii/description/)
- 核心概念：奇偶性、最小值不變性、分類討論
- 執行環境：.NET 10 console application

## 題目說明

給定一個由 `n` 個互異整數組成的陣列 `nums1`，要建立另一個長度同樣為
`n` 的陣列 `nums2`，使 `nums2` 的所有元素全部為奇數或全部為偶數。

對每個索引 `i`，必須恰好選擇下列其中一種操作：

1. 直接保留原值：`nums2[i] = nums1[i]`
2. 選擇另一個索引 `j`：
   `nums2[i] = nums1[i] - nums1[j]`，其中 `j != i`，而且差值必須
   `>= 1`

方法只需要判斷是否存在合法的 `nums2`，不需要實際回傳建構後的陣列。

### 官方範例

| 範例 | `nums1` | 結果 | 原因 |
| --- | --- | --- | --- |
| 1 | `[1, 4, 7]` | `true` | 保留 `1`、`7`，令 `4 - 1 = 3`，可得到全奇數陣列 `[1, 3, 7]` |
| 2 | `[2, 3]` | `false` | 最小值 `2` 無法變成奇數，而 `3` 也無法透過合法減法變成偶數 |
| 3 | `[4, 6]` | `true` | 直接保留兩個偶數即可 |

## 限制條件

依照官方題目限制：

- `1 <= n == nums1.length <= 10^5`
- `1 <= nums1[i] <= 10^9`
- `nums1` 中的元素互不相同

陣列一定非空，因此三種解法都可以安全地尋找最小值。實作遵循 LeetCode
輸入契約，不另外處理 `null`、空陣列、重複值或超出範圍的輸入。

## 解題概念與出發點

### 1. 先觀察什麼操作會改變奇偶性

減法的奇偶關係如下：

| 被減數 | 減數 | 差值 |
| --- | --- | --- |
| 偶數 | 偶數 | 偶數 |
| 偶數 | 奇數 | 奇數 |
| 奇數 | 偶數 | 奇數 |
| 奇數 | 奇數 | 偶數 |

如果想改變一個數原本的奇偶性，必須減去奇數；減去偶數只會維持原本的
奇偶性。因此真正重要的不是所有可能的差值，而是能否找到一個合法的較小
奇數作為減數。

### 2. 最小值的奇偶性不能被改變

第二種操作要求：

```text
nums1[i] - nums1[j] >= 1
```

這等價於：

```text
nums1[i] > nums1[j]
```

設整個陣列的最小值為 `min`。因為不存在比 `min` 更小的陣列元素，所以
`min` 無法使用第二種操作，只能直接保留。換句話說，`min` 的奇偶性一定會
出現在最終陣列中，也就限制了 `nums2` 必須統一成哪一種奇偶性。

### 3. 分成四種情況

| 輸入情況 | 最小值 | 是否有奇數 | 結果 | 理由 |
| --- | --- | --- | --- | --- |
| 全部為偶數 | 偶數 | 否 | `true` | 全部直接保留即可 |
| 全部為奇數 | 奇數 | 是 | `true` | 全部直接保留即可 |
| 奇偶混合，最小值為奇數 | 奇數 | 是 | `true` | 每個偶數減去最小奇數後會變成正奇數 |
| 奇偶混合，最小值為偶數 | 偶數 | 是 | `false` | 無法統一成全奇數，也無法統一成全偶數 |

當最小值是奇數時，所有偶數都比它大。讓每個偶數減去這個最小奇數，差值
一定至少為 `1`，而且「偶數減奇數」會得到奇數；原本的奇數直接保留，便能
把整個陣列統一成奇數。

當最小值是偶數且陣列中存在奇數時：

- 若想統一成奇數，最小偶數找不到更小的奇數可減，因此無法變成奇數。
- 若想統一成偶數，陣列中最小的奇數找不到另一個更小的奇數可減；減去偶數
  又不會改變它的奇偶性，因此它無法變成偶數。

兩個目標都無法達成，所以這是唯一必須回傳 `false` 的情況。

### 4. 最終判斷式

上述分類可以濃縮成：

```text
最小值是奇數，或陣列中完全沒有奇數 => true
最小值是偶數，而且陣列中存在奇數 => false
```

對應的布林式為：

```csharp
min % 2 != 0 || !hasOdd
```

## 解法一：LINQ 簡潔判斷

### 設計說明

`UniformArray` 使用兩個 LINQ 操作取得判斷所需資訊：

```csharp
int min = nums1.Min();
bool hasOdd = nums1.Any(x => x % 2 != 0);

return min % 2 != 0 || !hasOdd;
```

- `Min()` 找出無法改變奇偶性的最小值。
- `Any(...)` 判斷陣列中是否至少存在一個奇數；找到第一個奇數後就會停止。
- 最後直接套用推導完成的布林條件。

這個版本最精簡，適合已理解證明、希望程式碼直接表達結論的情境。最壞情況
下，`Min()` 與 `Any()` 各走訪陣列一次。

### 範例演示：`[1, 4, 7]`

1. `Min()` 得到 `min = 1`。
2. `Any(...)` 找到奇數 `1`，得到 `hasOdd = true`。
3. 判斷式為 `true || false`，所以回傳 `true`。
4. 實際構造時可保留 `1`、`7`，並令 `4 - 1 = 3`，得到
   `nums2 = [1, 3, 7]`。

## 解法二：顯式分類討論

### 設計說明

`UniformArray2` 取得與解法一相同的 `min` 和 `hasOdd`，但使用 `if` 將推理
過程展開：

```text
如果 min 是奇數：
    回傳 true

如果陣列中沒有奇數：
    回傳 true

否則：
    回傳 false
```

第一個分支表示可以用最小奇數把所有較大的偶數轉成奇數；第二個分支表示
原陣列已經全為偶數。兩個分支都不成立時，正好就是「最小值為偶數且仍有
奇數」的唯一失敗情況。

這個版本的程式較長，但控制流程與分類證明一一對應，適合初次閱讀此題或
需要逐步除錯時使用。最壞情況同樣會走訪陣列兩次。

### 範例演示：`[2, 3]`

1. 找到 `min = 2`，並得到 `hasOdd = true`。
2. `min` 不是奇數，因此略過第一個 `return true`。
3. `hasOdd` 是 `true`，所以 `!hasOdd` 不成立，也略過第二個 `return true`。
4. 執行最後的 `return false`。
5. 最小偶數 `2` 無法變成奇數；奇數 `3` 也沒有更小的奇數可減，因此結果
   確實不可行。

## 解法三：單次迴圈掃描

### 設計說明

`UniformArray3` 不使用兩個獨立 LINQ 查詢，而是在同一個 `foreach` 中同步
維護：

- `min`：目前看過的最小值，初始為 `int.MaxValue`。
- `hasOdd`：目前是否看過奇數，初始為 `false`。

每讀取一個 `num`，就用 `Math.Min(min, num)` 更新最小值；若 `num` 是奇數，
則將 `hasOdd` 設為 `true`。掃描完成後仍套用相同的最終判斷式。

這個版本只走訪陣列一次，適合希望明確控制遍歷次數、避免多次 LINQ 列舉的
情境。它與前兩種方法有相同的漸進時間與空間複雜度，但常數操作較集中。

### 範例演示：`[4, 6]`

| 讀取元素 | 更新後的 `min` | 更新後的 `hasOdd` |
| ---: | ---: | --- |
| 初始狀態 | `int.MaxValue` | `false` |
| `4` | `4` | `false` |
| `6` | `4` | `false` |

掃描完成後，`min` 是偶數，但 `hasOdd` 為 `false`，所以判斷式
`false || true` 得到 `true`。兩個元素直接保留後，`nums2 = [4, 6]` 已經全部
是偶數。

## 正確性證明

設 `min` 是 `nums1` 的最小值。

1. 若 `min` 是奇數，保留所有奇數；對每個偶數 `x`，因元素互異且 `min`
   是最小值，所以 `x > min`，因此 `x - min >= 1`。偶數減奇數為奇數，故可
   構造出全奇數的 `nums2`。
2. 若陣列中沒有奇數，所有元素原本都是偶數，全部使用第一種操作即可構造
   出全偶數的 `nums2`。
3. 剩餘情況中，`min` 是偶數且陣列含有奇數。`min` 無法改成奇數，所以不能
   構造全奇數陣列；最小的奇數也找不到更小的奇數來改成偶數，所以不能構造
   全偶數陣列。

這三種情況涵蓋所有合法輸入，因此判斷式
`min % 2 != 0 || !hasOdd` 必要且充分，三種實作皆正確。

## 複雜度比較

| 解法 | 陣列走訪 | 時間複雜度 | 額外空間複雜度 | 特點 |
| --- | --- | --- | --- | --- |
| `UniformArray` | 最壞兩次 | `O(n)` | `O(1)` | 程式最精簡 |
| `UniformArray2` | 最壞兩次 | `O(n)` | `O(1)` | 分支最貼近分類證明 |
| `UniformArray3` | 一次 | `O(n)` | `O(1)` | 單次掃描收集所有狀態 |

三種方法都只回傳可行性，不會配置或修改 `nums2`，也不會改動呼叫端傳入的
`nums1`。

## Main 固定測試案例

`Main` 不讀取命令列或主控台輸入。下列六組資料會分別交給三種解法，共執行
18 項檢查：

| 案例 | 輸入 | 預期結果 | 覆蓋重點 |
| --- | --- | --- | --- |
| 官方範例 1 | `[1, 4, 7]` | `true` | 奇偶混合、最小值為奇數 |
| 官方範例 2 | `[2, 3]` | `false` | 唯一失敗分類 |
| 官方範例 3 | `[4, 6]` | `true` | 全部為偶數 |
| 全部為奇數 | `[1, 3, 5]` | `true` | 不需要執行減法 |
| 單一元素 | `[9]` | `true` | 最小長度邊界 |
| 最大長度 | `[1, 2, ..., 100000]` | `true` | `n = 10^5` 上限與大量輸入 |

## 執行方式

請在本 README 所在的專案根目錄執行。

### 建置

```powershell
dotnet build .\leetcode_3876\leetcode_3876.csproj --nologo
```

### 執行固定案例

```powershell
dotnet run --project .\leetcode_3876\leetcode_3876.csproj --no-build
```

也可以在 VS Code 選擇 `Run leetcode_3876` 後按 F5；既有啟動設定不要求輸入
任何參數。

### 實際輸出

```text
=== 3876. Construct Uniform Parity Array II ===
官方範例 1：最小值為奇數 | UniformArray：預期 True，實際 True，結果 PASS
官方範例 1：最小值為奇數 | UniformArray2：預期 True，實際 True，結果 PASS
官方範例 1：最小值為奇數 | UniformArray3：預期 True，實際 True，結果 PASS
官方範例 2：最小值為偶數且存在奇數 | UniformArray：預期 False，實際 False，結果 PASS
官方範例 2：最小值為偶數且存在奇數 | UniformArray2：預期 False，實際 False，結果 PASS
官方範例 2：最小值為偶數且存在奇數 | UniformArray3：預期 False，實際 False，結果 PASS
官方範例 3：全部為偶數 | UniformArray：預期 True，實際 True，結果 PASS
官方範例 3：全部為偶數 | UniformArray2：預期 True，實際 True，結果 PASS
官方範例 3：全部為偶數 | UniformArray3：預期 True，實際 True，結果 PASS
全部為奇數 | UniformArray：預期 True，實際 True，結果 PASS
全部為奇數 | UniformArray2：預期 True，實際 True，結果 PASS
全部為奇數 | UniformArray3：預期 True，實際 True，結果 PASS
單一元素 | UniformArray：預期 True，實際 True，結果 PASS
單一元素 | UniformArray2：預期 True，實際 True，結果 PASS
單一元素 | UniformArray3：預期 True，實際 True，結果 PASS
最大長度 | UniformArray：預期 True，實際 True，結果 PASS
最大長度 | UniformArray2：預期 True，實際 True，結果 PASS
最大長度 | UniformArray3：預期 True，實際 True，結果 PASS
總結：18/18 通過，0 個失敗。
```

## 專案結構

```text
README.md
docs/
└── readme-template.md
leetcode_3876/
├── Program.cs
└── leetcode_3876.csproj
```

## 驗證範圍

- 專案可使用 .NET 10 成功建置，沒有編譯警告或錯誤。
- 三種解法對六組固定案例都回傳預期結果。
- README 的命令、案例及輸出與實際程式一致。
- `Main` 原有的題目描述 XML 維持不變。
- 變更通過 `git diff --check` 與額外的行尾空白檢查。