# LeetCode 453：最小操作次數使陣列元素相等

![C#](https://img.shields.io/badge/C%23-68217A?logo=csharp&logoColor=white)
![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)

使用 C# 解題，示範如何把「每次增加 `n - 1` 個元素」轉換成簡單的最小值差額加總。

- [LeetCode 題目](https://leetcode.com/problems/minimum-moves-to-equal-array-elements/description/)
- [中文題目](https://leetcode.cn/problems/minimum-moves-to-equal-array-elements/description/)

## 題目說明

給定整數陣列 `nums`，每次操作可以將其中 `n - 1` 個元素各增加 `1`。請計算讓所有元素相等所需的最少操作次數。

例如 `nums = [1, 2, 3]` 的答案是 `3`。

## 限制條件

- `1 <= nums.length <= 10^5`
- `-10^9 <= nums[i] <= 10^9`
- 題目保證答案可用 32 位元有號整數表示。

## 解題概念與出發點

直接模擬每次增加 `n - 1` 個元素，會讓陣列中的數字持續變大，也不容易看出最少操作次數。觀察相對差異即可簡化問題：每次操作都替 `n - 1` 個元素加 `1`，等同於先替全部元素加 `1`，再把唯一未被選中的元素減 `1`。所有元素共同增加的部分只會讓整個陣列平移，不會改變元素間是否相等，因此可以把一次原操作視為降低一個元素 `1`。

在這個等價視角下，元素只能往下調整；要用最少次數相等，目標應是原陣列的最小值。每個元素 `nums[i]` 需要降低 `nums[i] - min(nums)` 次，將所有差額相加就是答案：

```text
moves = Σ(nums[i] - min(nums))
```

## 解法設計：最小值差額加總

目前專案實作一種解法，位於 `leetcode_453/Program.cs` 的 `MinMoves`。

1. 掃描陣列找出最小值 `minNum`。
2. 再掃描一次，累加每個元素與 `minNum` 的差 `num - minNum`。
3. 回傳差額總和。輸入陣列不會被修改。

### 範例演示

以 `nums = [1, 2, 3]` 為例：

程式中的這行會把「目前元素降到最小值還需要幾次」加進答案：

```csharp
res += num - minNum;
```

| 元素 `num` | 1 | 2 | 3 |
| --- | ---: | ---: | ---: |
| 最小值 `minNum` | 1 | 1 | 1 |
| 本次加入 `res` 的次數 `num - minNum` | 0 | 1 | 2 |
| 加總後的 `res` | 0 | 1 | 3 |

走訪第一個元素 `1` 時，差額是 `1 - 1 = 0`，所以 `res` 仍是 `0`；走訪 `2` 時加 `2 - 1 = 1`，`res` 變成 `1`；最後走訪 `3` 時加 `3 - 1 = 2`，`res` 變成 `3`。因此最後的 `res` 就是 `0 + 1 + 2 = 3`，也就是最少操作次數。

為什麼每個元素要加上 `num - minNum`？把每次原題操作拆開看，就是「全部元素都加 `1`，再把唯一沒被選中的元素減 `1`」。全部元素一起加 `1` 只會讓陣列平移，不會改變元素之間的差距；忽略這個共同增加後，一次操作就等同於降低一個元素 `1`。例如，原題中的操作序列：

```text
[1, 2, 3] → [2, 3, 3] → [3, 4, 3] → [4, 4, 4]
```

忽略每一步讓全部元素一起增加的部分後，相當於：

```text
[1, 2, 3] → [1, 2, 2] → [1, 2, 1] → [1, 1, 1]
```

在這個等價視角裡，`3` 降低了 `2` 次，`2` 降低了 `1` 次，`1` 不必降低，正好分別是 `3 - 1`、`2 - 1` 和 `1 - 1`。最小值 `1` 是可達成的最高目標；若選比 `1` 更低的目標，所有元素還得再多降低幾次。因此選 `minNum`，並把每個 `num - minNum` 加起來，就得到最少操作次數。

### 複雜度

- 時間：`O(n)`，找最小值與加總各掃描陣列一次。
- 額外空間：`O(1)`，只使用最小值與累加器，不建立與輸入長度相關的資料結構。

## 建置與執行範例

在專案根目錄使用 PowerShell 執行：

```powershell
dotnet build .\leetcode_453\leetcode_453.csproj
dotnet run --project .\leetcode_453\leetcode_453.csproj
```

專案沒有獨立測試專案；執行程式會用預期值驗證下列四組資料。任一結果不符時會拋出例外並使執行失敗。

```text
[PASS] nums=[1, 2, 3], expected=3, actual=3
[PASS] nums=[1, 1, 1], expected=0, actual=0
[PASS] nums=[42], expected=0, actual=0
[PASS] nums=[-1000000000, 1000000000], expected=2000000000, actual=2000000000
```

## 專案結構

```text
.
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   └── readme-template.md
├── leetcode_453/
│   ├── Program.cs
│   └── leetcode_453.csproj
└── README.md
```
