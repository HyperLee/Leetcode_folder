# LeetCode 1442：形成两个异或相等数组的三元组数目

這個專案以 .NET 10 console project 示範 LeetCode 1442「Count Triplets That Can Form Two Arrays of Equal XOR」的解法。程式保留題目要求的三元組定義，並在 `Main` 中以固定測試資料輸出 Expected、Actual 與 PASS/FAIL，方便直接執行與理解演算法。

題目連結：

- [LeetCode](https://leetcode.com/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/description/)
- [力扣中文版](https://leetcode.cn/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/description/)

## 題目說明

給定一個整數陣列 `arr`，選擇三個索引 `i`、`j`、`k`，並且滿足：

```text
0 <= i < j <= k < arr.length
```

將陣列切成兩段：

```text
a = arr[i] ^ arr[i + 1] ^ ... ^ arr[j - 1]
b = arr[j] ^ arr[j + 1] ^ ... ^ arr[k]
```

其中 `^` 代表位元 XOR。若 `a == b`，就算一組符合條件的 `(i, j, k)`。請回傳所有符合條件的三元組數量。

### 限制條件

- `1 <= arr.length <= 300`
- `0 <= arr[i] <= 10^8`
- `i`、`j`、`k` 必須符合 `0 <= i < j <= k < arr.length`
- 程式中的 `CountTriplets` 接受非空整數陣列；`Main` 的案例均符合題目輸入限制。

## 解題概念與出發點

題目要比較左右兩段的 XOR：

```text
a == b
```

利用 XOR 的基本性質，可以改寫成：

```text
a ^ b == 0
```

而 `a` 與 `b` 剛好涵蓋連續區間 `arr[i..k]`，所以：

```text
a ^ b = arr[i] ^ arr[i + 1] ^ ... ^ arr[k]
```

因此固定 `i` 與 `k` 後，只要 `arr[i..k]` 的 XOR 等於 `0`，所有合法的切點 `j` 都會形成答案。這是本解法的核心觀察：把原本需要逐一比較左右兩段的問題，轉換成判斷整段區間 XOR 是否為 `0`。

## 解法：列舉區間端點並累積 XOR

專案中的 `CountTriplets` 使用三層流程：

1. 以第一層迴圈列舉左端點 `i`。
2. 以第二層迴圈列舉右端點 `k`，只考慮 `k > i`，確保至少能選出一個合法的 `j`。
3. 以第三層迴圈從 `i` 到 `k` 累積 `arr[i..k]` 的 XOR。
4. 如果區間 XOR 不為 `0`，這組 `(i, k)` 不增加答案。
5. 如果區間 XOR 為 `0`，合法切點為 `j = i + 1` 到 `k`，共有 `k - i` 個，因此將 `k - i` 加入總數。

### 為什麼可以一次加入 `k - i`？

固定 `i` 和 `k` 後，對任意合法切點 `j` 都有：

```text
a ^ b = arr[i] ^ ... ^ arr[j - 1] ^ arr[j] ^ ... ^ arr[k]
       = arr[i] ^ ... ^ arr[k]
```

如果整段 XOR 為 `0`，就表示 `a ^ b == 0`，也就是 `a == b`。因此不需要重新計算每一個 `j` 的左右段 XOR，只要把合法切點數量 `k - i` 一次加入即可。

### 演算法流程

以 `i = 0`、`k = 2` 為例，區間是 `[2, 3, 1]`：

```text
xor = 2 ^ 3 ^ 1 = 0
```

這時 `j` 可以是 `1` 或 `2`，共有 `2 = k - i` 種：

```text
j = 1：a = 2，     b = 3 ^ 1 = 2
j = 2：a = 2 ^ 3， b = 1       = 1
```

兩組都符合 `a == b`，所以答案增加 2。

### 範例演示：`[2, 3, 1, 6, 7]`

下表列出所有 `i < k` 的區間判斷。只有區間 XOR 為 `0` 時才會增加答案：

| `i` | `k` | 區間 `arr[i..k]` | 區間 XOR | 增加數量 |
| ---: | ---: | --- | ---: | ---: |
| 0 | 1 | `[2, 3]` | 1 | 0 |
| 0 | 2 | `[2, 3, 1]` | 0 | 2 |
| 0 | 3 | `[2, 3, 1, 6]` | 6 | 0 |
| 0 | 4 | `[2, 3, 1, 6, 7]` | 1 | 0 |
| 1 | 2 | `[3, 1]` | 2 | 0 |
| 1 | 3 | `[3, 1, 6]` | 4 | 0 |
| 1 | 4 | `[3, 1, 6, 7]` | 3 | 0 |
| 2 | 3 | `[1, 6]` | 7 | 0 |
| 2 | 4 | `[1, 6, 7]` | 0 | 2 |
| 3 | 4 | `[6, 7]` | 1 | 0 |

總數為 `2 + 2 = 4`。實際符合的三元組可以列為：

```text
(i, j, k) = (0, 1, 2)
(i, j, k) = (0, 2, 2)
(i, j, k) = (2, 3, 4)
(i, j, k) = (2, 4, 4)
```

### 正確性說明

對於任一合法的 `(i, j, k)`，左右兩段 XOR 設為 `a` 與 `b`。因為 XOR 具有結合律與交換律：

```text
a == b
<=> a ^ b == 0
<=> arr[i] ^ ... ^ arr[k] == 0
```

所以當程式固定的 `(i, k)` 區間 XOR 為 `0` 時，`i + 1` 到 `k` 的每一個 `j` 都是有效切點；反之，若區間 XOR 不為 `0`，則沒有任何 `j` 能讓兩段 XOR 相等。程式完整列舉所有 `i < k`，並對每個有效區間加入全部 `k - i` 個切點，因此會得到所有且僅有符合條件的三元組數量。

### 複雜度

- 時間複雜度：`O(n^3)`。`i`、`k` 與區間 XOR 累積各自形成一層迴圈。
- 空間複雜度：`O(1)`。只使用計數器、索引與目前區間的 XOR 值，沒有建立額外與輸入大小相關的資料結構。

## 可執行範例

`Main` 內建五組固定案例，涵蓋：

| 案例 | 輸入 | 預期結果 | 用途 |
| ---: | --- | ---: | --- |
| 1 | `[2, 3, 1, 6, 7]` | 4 | 題目典型案例，驗證多個有效區間 |
| 2 | `[1, 2]` | 0 | 最短可切分長度且沒有符合區間 |
| 3 | `[1]` | 0 | 陣列長度下界，沒有合法的 `j` |
| 4 | `[1, 1, 1, 1, 1]` | 10 | 重複值造成多組區間 XOR 為 0 |
| 5 | `[0, 0, 0]` | 4 | XOR 為 0 的區間集中出現 |

程式會逐筆列印 `Expected`、`Actual` 與 `Result`。若任一案例失敗，會將程序結束碼設為 `1`，方便在指令列或 CI 中辨識失敗；全部通過時則維持成功結束。

## 執行方式

請在本專案根目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_1442` 執行：

```bash
dotnet restore leetcode_1442/leetcode_1442.csproj
dotnet build leetcode_1442/leetcode_1442.csproj --nologo
dotnet run --project leetcode_1442/leetcode_1442.csproj --no-build
```

專案沒有獨立的 automated test project，因此以 `Main` 的固定案例、建置結果與程序結束碼作為目前的驗證方式。

## 實際執行結果

以下內容來自完成建置後執行 `dotnet run --project leetcode_1442/leetcode_1442.csproj --no-build` 的實際輸出：

```text
LeetCode 1442 - Count Triplets That Can Form Two Arrays of Equal XOR
=== 測試結果 ===
Case 1: [2, 3, 1, 6, 7]
Expected: 4, Actual: 4, Result: PASS
Case 2: [1, 2]
Expected: 0, Actual: 0, Result: PASS
Case 3: [1]
Expected: 0, Actual: 0, Result: PASS
Case 4: [1, 1, 1, 1, 1]
Expected: 10, Actual: 10, Result: PASS
Case 5: [0, 0, 0]
Expected: 4, Actual: 4, Result: PASS
Summary: 5/5 checks passed
```

## 專案結構

```text
leetcode_1442/
├── leetcode_1442/
│   ├── Program.cs                 # 演算法與 Main 測試入口
│   └── leetcode_1442.csproj       # .NET 10 console project
├── docs/
│   └── readme-template.md         # README 初始建立模板
├── .vscode/                       # VS Code 建置與偵錯設定
├── leetcode_1442.sln              # Solution 檔案
└── README.md                      # 題目與解法說明
```

編譯產生的 `bin/` 與 `obj/` 目錄屬於建置產物，不應納入版本控制。
