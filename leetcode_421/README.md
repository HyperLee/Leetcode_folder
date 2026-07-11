# LeetCode 421：Maximum XOR of Two Numbers in an Array

這是一個以 C# 撰寫的 .NET 10 主控台專案。它保留原本的 HashSet 前綴貪婪法，
用由高位到低位的方式求兩個陣列元素 XOR 後的最大值。`FindMaximumXOR` 只負責
計算，`Main` 則是可重複執行的 acceptance harness。

- [英文題目：421. Maximum XOR of Two Numbers in an Array](https://leetcode.com/problems/maximum-xor-of-two-numbers-in-an-array/)
- [中文題目：421. 陣列中兩個數的最大 XOR 值](https://leetcode.cn/problems/maximum-xor-of-two-numbers-in-an-array/)

## 題目說明

給定整數陣列 `nums`，回傳 `nums[i] XOR nums[j]` 的最大值，其中
`0 <= i <= j < nums.Length`。XOR 的同一個位元只有在兩個輸入位元不同時為 `1`；
因此要讓結果最大，必須優先讓最高位盡可能為 `1`。

## 限制條件

- `1 <= nums.Length <= 2 * 10^5`
- `0 <= nums[i] <= 2^31 - 1`
- `FindMaximumXOR` 遵循上述有效輸入契約，不額外定義 null、空陣列或負數行為。

## 核心不變量：由高位貪婪決定答案

整數上限只使用 bit `30` 到 bit `0`。當演算法正準備判定 bit `k` 時，
`maximumXor` 已經是更高位元所能達到的最大前綴。接著：

1. 將每個數右移 `k` 位，取得高位 prefix，並放入 `HashSet<int>`。
2. 暫時將本位設為 `1`，形成 `candidate = (maximumXor << 1) | 1`。
3. 若存在兩個 prefix `p`、`q`，使 `p XOR q == candidate`，等價於集合中同時有
   `p` 與 `candidate XOR p`，表示候選值可達成，保留這個 `1`。
4. 否則本位只能是 `0`，將候選值回退一位。

每一輪只保存該位的 prefix 集合；解法不排序、不修改輸入，也不需要建立 Trie。

## 範例走查

官方範例 `[3, 10, 5, 25, 2, 8]` 的最佳配對是 `5 XOR 25`。以五個低位觀察：

```plaintext
  5 = 00101
 25 = 11001
----------- XOR
 28 = 11100
```

演算法會先確認最高可用位能否為 `1`，再確認下一位；一旦某個 `candidate` 沒有對應
的兩個 prefix，就不能犧牲已確定的高位去換取低位，必須將該位回退為 `0`。

## 解法設計與複雜度

公開介面：

```csharp
public static int FindMaximumXOR(int[] nums)
```

- 時間複雜度：`O(31n)`，31 是固定的整數位數，因此等價於 `O(n)`。
- 結果空間：`O(1)`，方法只回傳一個 `int`。
- 輔助空間：`O(n)`，單輪 `HashSet<int>` 最多保存 `n` 個 prefix。

## 可執行驗證案例

`Main` 執行 8 項確定性檢查：

| 案例 | 輸入 | 預期值 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `[3,10,5,25,2,8]` | `28` | 官方範例一 |
| 2 | `[14,70,53,83,49,91,36,80,92,51,66,70]` | `127` | 官方範例二 |
| 3 | `[0]` | `0` | 最小合法輸入與 `i == j` |
| 4 | `[7,7]` | `0` | 重複值 |
| 5 | `[0, 1 << 30]` | `1073741824` | 最高合法 bit |
| 6 | `[0, int.MaxValue]` | `2147483647` | 所有合法結果位元 |
| 7 | `[8,1,2]` | `10` | 貪婪 prefix 回歸案例 |
| 8 | 長度 `200000`，含 `0` 與 `int.MaxValue` | `2147483647` | 題目上限 spot check |

每一項都會輸出輸入或案例說明、Expected、Actual 與 PASS/FAIL。只要有一項失敗，
程式會將 `Environment.ExitCode` 設為 `1`。本題沒有獨立測試專案；此 harness 是目前
的主要驗證機制。

## 建置與執行

請從此 README 所在的外層 `leetcode_421` 目錄執行：

```bash
dotnet build leetcode_421/leetcode_421.csproj --nologo
dotnet run --no-build --project leetcode_421/leetcode_421.csproj
```

以下為重新建置後執行第二個命令的完整輸出：

```text
LeetCode 421 acceptance harness

Case 1: Official example 1
Input: [3, 10, 5, 25, 2, 8]
PASS | Expected: 28 | Actual: 28

Case 2: Official example 2
Input: [14, 70, 53, 83, 49, 91, 36, 80, 92, 51, 66, 70]
PASS | Expected: 127 | Actual: 127

Case 3: Minimum valid input
Input: [0]
PASS | Expected: 0 | Actual: 0

Case 4: Duplicate values
Input: [7, 7]
PASS | Expected: 0 | Actual: 0

Case 5: Bit 30 boundary
Input: [0, 1073741824]
PASS | Expected: 1073741824 | Actual: 1073741824

Case 6: All legal value bits
Input: [0, 2147483647]
PASS | Expected: 2147483647 | Actual: 2147483647

Case 7: Greedy-prefix regression
Input: [8, 1, 2]
PASS | Expected: 10 | Actual: 10

Case 8: Upper-bound spot check
Input: generated length = 200000; values include 0 and 2147483647
PASS | Expected: 2147483647 | Actual: 2147483647

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
├── leetcode_421/
│   ├── Program.cs             # 純解法與可執行驗證器
│   └── leetcode_421.csproj    # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本專案協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
