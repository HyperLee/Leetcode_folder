# LeetCode 3364．最小正和子陣列

## 題目說明

給定整數陣列 `nums`，從中選出一段連續且非空的子陣列。子陣列長度必須介於 `l` 和 `r` 之間（含兩端），且元素總和必須嚴格大於 `0`。回傳所有符合條件的總和中的最小值。如果沒有符合條件的子陣列，回傳 `-1`。[LeetCode 3364 題目](https://leetcode.com/problems/minimum-positive-sum-subarray/description/)

### 限制條件

- `1 <= nums.length <= 100`
- `1 <= l <= r <= nums.length`
- `-1000 <= nums[i] <= 1000`

### 官方範例

| `nums` | `l` | `r` | 答案 | 說明 |
| --- | ---: | ---: | ---: | --- |
| `[3, -2, 1, 4]` | 2 | 3 | 1 | `[3, -2]` 的總和是 1，為最小正和。 |
| `[-2, 2, -3, 1]` | 2 | 3 | -1 | 合法長度的子陣列總和都小於或等於 0。 |
| `[1, 2, 3, 4]` | 2 | 4 | 3 | `[1, 2]` 的總和是 3，為最小正和。 |

## 解題出發點

子陣列可由起點 `start` 和長度 `length` 唯一決定，共涵蓋 `nums[start]` 到 `nums[start + length - 1]`。長度固定時，可用的起點為 `0` 到 `n - length`。起點固定時，合法長度為 `l` 到 `min(r, n - start)`。前兩種方法先枚舉長度，第三種方法先枚舉起點。

若為每個候選區間重新加總，許多重疊元素會被反覆讀取。滑動視窗沿用上個視窗的總和，前綴和用兩個累計值求區間總和，固定起點的做法則在延伸右端時沿用上一輪的總和。

三種方法都用 `int.MaxValue` 表示尚未找到正和，遇到 `sum > 0 && sum < minSum` 才更新答案。在題目限制下，區間總和的絕對值至多為 `100000`。掃描結束時，若 `minSum` 仍為初始值，回傳 `-1`。

以下三段演示共用 `nums = [4, -1, -2, 4]`、`l = 2`、`r = 3`。它共有五個合法長度的區間，其中 `[4, -1, -2]` 和 `[-1, -2, 4]` 同為最小正和 `1`。

## 解法一．固定長度的滑動視窗

`MinimumSumSubarray` 先將長度固定為 `length`，從陣列開頭往右滑動，每次移動一格。判斷完該長度的所有視窗後，再增加長度。

1. **初始化迴圈 `i`**先把前 `l - 1` 個元素加進 `initial`，刻意留一個元素給外層迴圈的第一輪補上。若 `l = 1`，迴圈本體執行零次，`initial` 從 `0` 開始。
2. **外層迴圈 `length`**從 `l` 跑到 `r`。第一輪 `length = l` 時，`initial += nums[length - 1]` 補上索引 `l - 1` 的元素，湊齊長度為 `l` 的第一個視窗。後續每輪沿用 `initial`，再加一個元素。每輪都先用 `sum = initial` 取得並檢查起點為 `0` 的視窗總和。
3. **內層迴圈 `hi`**從 `length` 走到 `n - 1`，逐格右移並檢查其餘視窗。每次加入右側的 `nums[hi]`、扣掉左側的 `nums[hi - length]`，所以用 `sum += nums[hi] - nums[hi - length]` 更新總和。新視窗的起點是 `hi - length + 1`，終點是 `hi`。滑動時用 `sum` 記錄目前視窗的總和，讓 `initial` 保留首個視窗的總和供下一輪使用。

每個長度先檢查起點 `0`，再由內層迴圈依序檢查到最後一個合法起點 `n - length`。外層走完 `l` 到 `r` 後，每個合法區間都恰好被檢查一次。

```text
index                        0   1   2   3
nums                         4  -1  -2   4

length = 2
                            [4  -1] -2   4   initial = 4 + (-1) = 3     minSum = 3
                             4 [-1  -2]  4   sum = 3 + (-2) - 4 = -3    minSum = 3
                             4  -1 [-2   4]  sum = -3 + 4 - (-1) = 2    minSum = 2

length = 3
                            [4  -1  -2]  4   initial = 3 + (-2) = 1     minSum = 1
                             4 [-1  -2   4]  sum = 1 + 4 - 4 = 1        minSum = 1
```

每輪檢查第一個視窗時，以及每次滑動後，都用 `sum > 0 && sum < minSum` 判斷是否更新。圖中總和 `-3` 小於零，直接略過。長度從 2 增到 3 時，`initial` 從首個視窗的總和 `3` 延伸，而 `minSum` 保留前一輪找到的 `2`，直到遇到更小的正和 `1` 才更新。最後一個視窗的總和也為 `1`，所以最小值維持原值。

掃完所有長度後，若 `minSum` 仍為初始值 `int.MaxValue`，代表沒有正和，回傳 `-1`。否則回傳 `minSum`。

時間複雜度為 `O(n(r-l+1))`，每個長度最多檢查 `n` 個視窗，每次檢查和滑動只需常數時間。額外空間為 `O(1)`。

## 解法二．前綴和

`MinimumSumSubarray2` 先把每個位置前方的元素總和存起來。令 `prefixSum[i]` 為前 `i` 個元素的總和，則 `prefixSum[0] = 0`，且 `prefixSum[i + 1] = prefixSum[i] + nums[i]`。陣列需要 `n + 1` 格，最後一格存整個陣列的和。

要求從 `start` 開始、長度為 `length` 的區間，先取 `end = start + length`。半開區間 `[start, end)` 包含 `start`，最後一個元素的索引是 `end - 1`。從 `prefixSum[end]` 扣除起點前方的 `prefixSum[start]`，就只剩下區間內的總和。

外層枚舉 `length = l ... r`，內層枚舉 `start = 0 ... n - length`。條件 `start + length <= n` 允許 `end = n`，因此剛好延伸到陣列末尾的區間也會被檢查。

```text
boundary      0       1       2       3       4
prefixSum     0       4       3       1       5
              |       |       |       |       |
nums index        0       1       2       3
nums              4      -1      -2       4

length   interval   subtraction                     sum   best
  2       [0,2)     prefixSum[2] - prefixSum[0]       3      3
  2       [1,3)     prefixSum[3] - prefixSum[1]      -3      3
  2       [2,4)     prefixSum[4] - prefixSum[2]       2      2
  3       [0,3)     prefixSum[3] - prefixSum[0]       1      1
  3       [1,4)     prefixSum[4] - prefixSum[1]       1      1
```

例如 `[1, 3)` 涵蓋 `-1` 與 `-2`。前綴和相減為 `1 - 4 = -3`，因此略過。每組合法的 `(length, start)` 都恰好計算一次，最後回傳 `1`。

建立前綴和花費 `O(n)` 時間，每個候選區間只需一次減法，總時間為 `O(n + n(r-l+1))`，額外空間為 `O(n)`。

## 解法三．固定起點，逐步累加

`MinimumSumSubarray3` 逐一固定 `start`，並把右端往後延伸。最晚的起點是 `n - l`，因為從這個起點到陣列末尾剛好還有 `l` 個元素。

1. 每換一個起點，將 `sum` 重設為 `0`，並令 `maxLength = min(r, n - start)`。這個上界同時限制題目允許的長度和陣列剩餘元素數。
2. 長度從 `1` 累加到 `maxLength`。每輪加上 `nums[start + length - 1]` 後，`sum` 就是從 `start` 開始、目前長度的區間總和。
3. 前 `l - 1` 輪累計的元素會留在 `sum` 裡，供第一次達到 `l` 時使用。只有長度至少為 `l` 的總和才會拿來更新答案。每個起點都走過該起點可用的全部合法長度。

```text
start   maxLength   extend right                         best
  0         3       len 1: [4]         sum =  4  skip      -
                    len 2: [4,-1]      sum =  3            3
                    len 3: [4,-1,-2]   sum =  1            1

  1         3       len 1: [-1]        sum = -1  skip      1
                    len 2: [-1,-2]     sum = -3            1
                    len 3: [-1,-2,4]   sum =  1            1

  2         2       len 1: [-2]        sum = -2  skip      1
                    len 2: [-2,4]      sum =  2            1
```

例如起點為 `1` 時，長度 `2` 的總和是 `-3`。繼續加上右側的 `4` 後，長度 `3` 的總和變成 `1`。即使目前總和小於或等於零，也要繼續檢查更長的區間。起點 `3` 只剩一個元素，無法形成合法長度的區間，因此外層到 `2` 就結束。

每個起點最多延伸 `r` 次，時間複雜度為 `O(nr)`，額外空間為 `O(1)`。

## 複雜度比較

`n` 表示陣列長度。空間欄位只計入解法額外使用的記憶體。

| 解法 | 枚舉順序 | 時間 | 額外空間 |
| --- | --- | --- | --- |
| 滑動視窗 | 長度 → 起點 | `O(n(r-l+1))` | `O(1)` |
| 前綴和 | 長度 → 起點 | `O(n + n(r-l+1))` | `O(n)` |
| 逐步累加 | 起點 → 長度 | `O(nr)` | `O(1)` |

當 `l == r`，前兩種解法只掃描指定長度。第三種解法從長度 `1` 開始累計，因此每個有效起點仍要累加到 `l` 才能檢查候選。

## 建置與執行

使用 .NET 10 SDK，在本 README 所在目錄執行下列指令。

```bash
dotnet build leetcode_3364/leetcode_3364.csproj
dotnet run --project leetcode_3364/leetcode_3364.csproj
dotnet format leetcode_3364/leetcode_3364.csproj --verify-no-changes
```

建置結果為 0 個警告、0 個錯誤，格式檢查通過。執行時，`Main` 將官方三例和額外五筆邊界案例各交給三種解法，逐筆比對固定的預期值。以下為輸出最後四行。

```text
最小值多次更新，MinimumSumSubarray，預期 1，實際 1，PASS
最小值多次更新，MinimumSumSubarray2，預期 1，實際 1，PASS
最小值多次更新，MinimumSumSubarray3，預期 1，實際 1，PASS
通過 24/24 次比較。
```

全部通過時，程序結束碼為 `0`。如有任何結果不符，該筆會顯示 `FAIL`，程序在輸出所有比較結果後以結束碼 `1` 結束。
