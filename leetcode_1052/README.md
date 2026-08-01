# LeetCode 1052 — Grumpy Bookstore Owner

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)

本專案使用 C# 實作 LeetCode 1052「Grumpy Bookstore Owner」，提供固定長度
滑動視窗與前綴和兩種解法，並內建七組可直接執行、會同時驗證答案與輸入
保留性的測試資料。

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：固定長度滑動視窗](#解法一固定長度滑動視窗)
- [解法二：前綴和](#解法二前綴和)
- [兩種解法比較](#兩種解法比較)
- [測試案例](#測試案例)
- [建置與執行](#建置與執行)

## 題目說明

書店營業 `n` 分鐘：

- `customers[i]` 表示第 `i` 分鐘開始時進入書店的顧客數，這些顧客會在該分鐘
  結束後離開。
- `grumpy[i] == 1` 表示老闆在第 `i` 分鐘生氣，該分鐘進入的顧客不滿意。
- `grumpy[i] == 0` 表示老闆沒有生氣，該分鐘進入的顧客自然滿意。

老闆可以使用一次密技，使自己在連續 `minutes` 分鐘內不生氣。題目要求選擇
密技的使用區間，使整天滿意的顧客總數最大。

### 官方範例一

```text
輸入：customers = [1, 0, 1, 2, 1, 1, 7, 5]
      grumpy   = [0, 1, 0, 1, 0, 1, 0, 1]
      minutes  = 3
輸出：16
```

老闆原本不生氣的第 0、2、4、6 分鐘共有 `1 + 1 + 1 + 7 = 10` 位滿意
顧客。若把密技用在最後三分鐘，還能挽回第 5、7 分鐘的 `1 + 5 = 6` 位
顧客，因此答案是 `10 + 6 = 16`。

### 官方範例二

```text
輸入：customers = [1]
      grumpy   = [0]
      minutes  = 1
輸出：1
```

唯一一分鐘內老闆原本就沒有生氣，所以唯一一位顧客會滿意。

## 限制條件

依 [LeetCode 1052 官方題面](https://leetcode.com/problems/grumpy-bookstore-owner/description/)：

- `n == customers.length == grumpy.length`
- `1 <= minutes <= n <= 2 * 10^4`
- `0 <= customers[i] <= 1000`
- `grumpy[i]` 只會是 `0` 或 `1`

兩個公開方法都假設輸入陣列不是 `null`，而且符合上述正式題目限制；方法不會
修改 `customers` 或 `grumpy`。

## 解題概念與出發點

密技不會影響老闆原本就不生氣的分鐘。這些分鐘的顧客無論密技放在哪裡都會
滿意，因此可先把答案拆成兩部分：

```text
最多滿意顧客 = 原本就滿意的顧客 + 密技額外挽回的最大顧客
```

對 `grumpy[i] == 0` 的分鐘，`customers[i]` 計入「原本就滿意」；對
`grumpy[i] == 1` 的分鐘，只有當索引落在密技區間內，`customers[i]` 才是
「額外挽回」。

問題因此轉化為：在所有長度固定為 `minutes` 的連續區間中，找出
`grumpy[i] == 1` 時顧客數總和最大的區間。

本專案用兩種方式取得這個區間總和：

- 滑動視窗用「移除左端、加入右端」更新相鄰區間，僅需常數額外空間。
- 前綴和先保存每個前綴的累計值，再用兩個前綴值相減查詢任意區間。

## 解法一：固定長度滑動視窗

### 設計說明

`MaxSatisfied` 先走訪整天，累加所有 `grumpy[i] == 0` 的顧客，得到
`alwaysSatisfied`。接著建立前 `minutes` 分鐘的視窗，但視窗只累加
`grumpy[i] == 1` 的顧客，因為只有這些顧客是密技帶來的額外收益。

視窗向右移動一分鐘時：

1. 若離開視窗的分鐘原本生氣，從 `windowGain` 減去該分鐘顧客數。
2. 若進入視窗的分鐘原本生氣，將該分鐘顧客數加入 `windowGain`。
3. 用目前收益更新 `maxWindowGain`。

流程可寫成：

```text
alwaysSatisfied = 所有 grumpy[i] == 0 的 customers[i] 總和
windowGain = 前 minutes 分鐘內原本不滿意的顧客總和
maxWindowGain = windowGain

逐一把視窗向右移動：
    移除左端原本不滿意的顧客
    加入右端原本不滿意的顧客
    更新 maxWindowGain

回傳 alwaysSatisfied + maxWindowGain
```

### 關鍵不變量

每次比較 `maxWindowGain` 時，`windowGain` 恰好等於目前長度為 `minutes` 的
視窗內、原本因老闆生氣而不滿意的顧客數。移動時只刪除離開視窗的貢獻並加入
新進入視窗的貢獻，因此不需要重新掃描整個區間。

### 正確性說明

`alwaysSatisfied` 包含所有不受密技位置影響、原本就滿意的顧客。滑動視窗會
依序檢查每一個合法的密技區間，而 `windowGain` 正是該區間能額外挽回的人數；
因此 `maxWindowGain` 是所有密技使用方式中的最大額外收益。兩者相加即為整天
最多可滿意的顧客數。

### 複雜度

- 時間複雜度：`O(n)`。一次累加原本滿意的顧客，並以一次滑動掃描所有視窗。
- 額外空間複雜度：`O(1)`。只維護固定數量的整數變數。
- 是否修改輸入：否。

### 範例演示流程

使用官方範例一：

```text
customers = [1, 0, 1, 2, 1, 1, 7, 5]
grumpy    = [0, 1, 0, 1, 0, 1, 0, 1]
minutes   = 3
```

原本不生氣的分鐘為 0、2、4、6，因此：

```text
alwaysSatisfied = 1 + 1 + 1 + 7 = 10
```

依序檢查每個長度為 3 的視窗：

| 視窗索引 | 視窗內原本生氣的顧客 | `windowGain` | `maxWindowGain` |
|:---:|:---|---:|---:|
| `[0..2]` | 第 1 分鐘：0 | 0 | 0 |
| `[1..3]` | 第 1、3 分鐘：0 + 2 | 2 | 2 |
| `[2..4]` | 第 3 分鐘：2 | 2 | 2 |
| `[3..5]` | 第 3、5 分鐘：2 + 1 | 3 | 3 |
| `[4..6]` | 第 5 分鐘：1 | 1 | 3 |
| `[5..7]` | 第 5、7 分鐘：1 + 5 | 6 | 6 |

最大額外收益是 6，所以回傳 `10 + 6 = 16`。

## 解法二：前綴和

### 設計說明

`MaxSatisfied2` 建立長度為 `n + 1` 的 `dissatisfiedPrefix`。其中：

```text
dissatisfiedPrefix[i]
= 索引 [0, i) 內原本因老闆生氣而不滿意的顧客總和
```

建立前綴和時，只有 `grumpy[i] == 1` 才加入 `customers[i]`；如果老闆沒有
生氣，該分鐘的前綴增量是 0，同時把顧客加入 `alwaysSatisfied`。

任一半開區間 `[start, end)` 可額外挽回的顧客數為：

```text
dissatisfiedPrefix[end] - dissatisfiedPrefix[start]
```

只要列舉所有滿足 `end - start == minutes` 的區間並取最大值，就能找出最佳
密技位置。

### 關鍵不變量

建立到索引 `i` 後，`dissatisfiedPrefix[i + 1]` 完整記錄前 `i + 1` 分鐘內
所有原本不滿意的顧客。兩個前綴的共同部分相減後會被抵消，所以留下的正好是
指定視窗內可被密技挽回的顧客。

### 正確性說明

前綴和精確保存每個前綴中的原本不滿意顧客數，因此任一密技區間的額外收益
都能由區間兩端的前綴差取得。演算法列舉所有合法的固定長度區間並保留最大值，
所以得到的最大收益與最佳密技位置相同；再加上不受密技位置影響的
`alwaysSatisfied`，即為正確答案。

### 複雜度

- 時間複雜度：`O(n)`。建立前綴和與列舉固定長度區間各需一次線性掃描。
- 額外空間複雜度：`O(n)`，用來保存 `dissatisfiedPrefix`。
- 是否修改輸入：否。

### 範例演示流程

同樣使用官方範例一。每分鐘只有在老闆生氣時才產生前綴增量：

| `i` | `customers[i]` | `grumpy[i]` | 新增的不滿意顧客 | `dissatisfiedPrefix[i + 1]` |
|---:|---:|---:|---:|---:|
| 0 | 1 | 0 | 0 | 0 |
| 1 | 0 | 1 | 0 | 0 |
| 2 | 1 | 0 | 0 | 0 |
| 3 | 2 | 1 | 2 | 2 |
| 4 | 1 | 0 | 0 | 2 |
| 5 | 1 | 1 | 1 | 3 |
| 6 | 7 | 0 | 0 | 3 |
| 7 | 5 | 1 | 5 | 8 |

完整前綴和為：

```text
dissatisfiedPrefix = [0, 0, 0, 0, 2, 2, 3, 3, 8]
```

例如最後三分鐘對應半開區間 `[5, 8)`：

```text
區間收益 = dissatisfiedPrefix[8] - dissatisfiedPrefix[5]
         = 8 - 2
         = 6
```

所有長度為 3 的區間收益依序為 `0、2、2、3、1、6`，最大值為 6。加上
`alwaysSatisfied = 10`，最終回傳 16。

## 兩種解法比較

| 項目 | 解法一：滑動視窗 | 解法二：前綴和 |
|:---|:---|:---|
| 區間總和來源 | 由前一個視窗移除左端、加入右端 | 由兩個前綴值相減 |
| 時間複雜度 | `O(n)` | `O(n)` |
| 額外空間複雜度 | `O(1)` | `O(n)` |
| 是否修改輸入 | 否 | 否 |
| 優點 | 空間最佳，直接利用固定視窗長度 | 區間查詢概念通用，容易擴充多次查詢 |
| 取捨 | 更新視窗時要正確處理移出與移入值 | 需要配置與輸入長度成正比的陣列 |

本題只需要掃描一次固定長度視窗，因此滑動視窗是較精簡的正式解法。前綴和
則清楚展示如何把區間總和轉換成常數時間查詢，適合作為另一種思考方式。

## 測試案例

`RunSamples` 使用下列固定案例。每個案例會分別檢查兩種解法，所以共有 14 項
檢查：

| 案例 | `customers` | `grumpy` | `minutes` | 預期 | 驗證重點 |
|:---|:---|:---|---:|---:|:---|
| 官方範例一 | `[1,0,1,2,1,1,7,5]` | `[0,1,0,1,0,1,0,1]` | 3 | 16 | 一般案例、最佳視窗在尾端 |
| 官方範例二 | `[1]` | `[0]` | 1 | 1 | 最小正式輸入 |
| 密技覆蓋全時段 | `[4,10,10]` | `[1,1,1]` | 3 | 24 | 視窗長度等於陣列長度 |
| 全程不生氣 | `[2,3,4]` | `[0,0,0]` | 2 | 9 | 密技沒有額外收益 |
| 最佳視窗位於中間 | `[3,8,2,5,4]` | `[0,1,1,1,0]` | 2 | 17 | 正確更新視窗兩端 |
| 單分鐘密技 | `[5,0,6,2]` | `[1,1,0,1]` | 1 | 11 | 最小密技長度與零顧客 |
| 顧客數皆為零 | `[0,0,0]` | `[1,0,1]` | 2 | 0 | 結果下界與重複零值 |

兩種解法各自取得 `customers` 與 `grumpy` 的獨立副本。只有在答案等於預期值，
而且兩個輸入陣列都保持原樣時，該解法才會顯示 `Result: PASS`。若任何檢查
失敗，程式會設定非零結束碼。

## 專案結構

```text
leetcode_1052/
├── docs/
│   └── readme-template.md
├── leetcode_1052/
│   ├── leetcode_1052.csproj
│   └── Program.cs
├── leetcode_1052.sln
└── README.md
```

## 建置與執行

需求：

- .NET 10 SDK

從 `leetcode_1052` repository 根目錄依序執行：

```powershell
dotnet restore leetcode_1052/leetcode_1052.csproj
dotnet build leetcode_1052/leetcode_1052.csproj --no-restore --nologo
dotnet run --project leetcode_1052/leetcode_1052.csproj --no-build
```

本專案目前沒有獨立的自動測試專案；`dotnet build` 加上可執行的自我驗證案例
就是目前的驗收方式。

### 執行結果

```text
Case: 官方範例一
Customers: [1, 0, 1, 2, 1, 1, 7, 5]
Grumpy: [0, 1, 0, 1, 0, 1, 0, 1]
Minutes: 3
Expected: 16
MaxSatisfied: 16 | Inputs preserved: PASS | Result: PASS
MaxSatisfied2: 16 | Inputs preserved: PASS | Result: PASS

Case: 官方範例二
Customers: [1]
Grumpy: [0]
Minutes: 1
Expected: 1
MaxSatisfied: 1 | Inputs preserved: PASS | Result: PASS
MaxSatisfied2: 1 | Inputs preserved: PASS | Result: PASS

Case: 密技覆蓋全時段
Customers: [4, 10, 10]
Grumpy: [1, 1, 1]
Minutes: 3
Expected: 24
MaxSatisfied: 24 | Inputs preserved: PASS | Result: PASS
MaxSatisfied2: 24 | Inputs preserved: PASS | Result: PASS

Case: 全程不生氣
Customers: [2, 3, 4]
Grumpy: [0, 0, 0]
Minutes: 2
Expected: 9
MaxSatisfied: 9 | Inputs preserved: PASS | Result: PASS
MaxSatisfied2: 9 | Inputs preserved: PASS | Result: PASS

Case: 最佳視窗位於中間
Customers: [3, 8, 2, 5, 4]
Grumpy: [0, 1, 1, 1, 0]
Minutes: 2
Expected: 17
MaxSatisfied: 17 | Inputs preserved: PASS | Result: PASS
MaxSatisfied2: 17 | Inputs preserved: PASS | Result: PASS

Case: 單分鐘密技
Customers: [5, 0, 6, 2]
Grumpy: [1, 1, 0, 1]
Minutes: 1
Expected: 11
MaxSatisfied: 11 | Inputs preserved: PASS | Result: PASS
MaxSatisfied2: 11 | Inputs preserved: PASS | Result: PASS

Case: 顧客數皆為零
Customers: [0, 0, 0]
Grumpy: [1, 0, 1]
Minutes: 2
Expected: 0
MaxSatisfied: 0 | Inputs preserved: PASS | Result: PASS
MaxSatisfied2: 0 | Inputs preserved: PASS | Result: PASS

Summary: 14/14 checks passed.
```