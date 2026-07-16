# LeetCode 1291：Sequential Digits／順次數

這個 .NET 10 主控台專案逐位建立十進位順次數，並回傳指定閉區間內的所有結果。公開方法 `SequentialDigits(int low, int high)` 不輸出文字；`Main` 統一負責八個確定性驗收案例、PASS/FAIL 統計與失敗結束碼。

## 題目連結

- [LeetCode（英文）](https://leetcode.com/problems/sequential-digits/)
- [LeetCode（中文）](https://leetcode.cn/problems/sequential-digits/)

## 題目敘述與限制

若某個整數的每一位數字都比前一位數字大 1，該整數就是順次數。給定整數 `low` 與 `high`，回傳閉區間 `[low, high]` 內的所有順次數，結果必須依遞增順序排列。

- `10 <= low <= high <= 10^9`

## 逐位擴展不變量

外層迴圈依序選擇起始數字 `1` 到 `9`。內層迴圈每次只附加比目前末位大 1 的數字：

```plaintext
firstDigit = 1
1 -> 12 -> 123 -> 1234 -> ... -> 123456789
```

因此 `number` 在每次狀態更新後都必然是順次數，不需要再逐位檢查。只有位於 `[low, high]` 的候選值會加入結果。不同起始數字產生的序列會交錯，最後排序即可符合公開 API 的遞增順序契約。

## 實作設計與複雜度

十進位數字只有 `0` 到 `9`，而合法順次數至少包含兩位，因此最多只有 `8 + 7 + ... + 1 = 36` 個候選。實作保留直接的雙迴圈生成方式，不使用字串切割、解析或額外搜尋結構。

- 時間複雜度：在題目固定十進位限制下為 `O(1)`；若以輸出數量 `k` 表示，排序步驟為 `O(k log k)`，且 `k <= 36`。
- 輔助空間：`O(1)`；回傳結果空間為 `O(k)`。

## 範例推演

以 `low = 58`、`high = 155` 為例：

1. 從 `5` 開始可建立 `56`、`567`；兩者都不在區間內。
2. 從 `6`、`7`、`8` 開始可取得 `67`、`78`、`89`。
3. 從 `1` 開始建立的 `12` 太小，但下一個 `123` 位於區間內。
4. 排序後回傳 `[67, 78, 89, 123]`。

## 驗收案例

專案沒有正式測試專案或獨立測試框架；可執行檔內的確定性 harness 是驗證機制。

| 案例 | 區間 | 預期 | 驗證重點 |
| --- | --- | --- | --- |
| 1. Official example 1 | `[100,300]` | `[123,234]` | 第一個官方範例 |
| 2. Official example 2 | `[1000,13000]` | `[1234,...,12345]` | 第二個官方範例與跨位數排序 |
| 3. Minimum range without result | `[10,10]` | `[]` | 最小合法邊界 |
| 4. First sequential digit | `[10,12]` | `[12]` | 第一個合法順次數 |
| 5. Exact single match | `[123,123]` | `[123]` | 閉區間雙邊界 |
| 6. Cross digit lengths | `[58,155]` | `[67,78,89,123]` | 二位數與三位數交錯排序 |
| 7. Range without result | `[90,100]` | `[]` | 區間內沒有答案 |
| 8. Full constraint range | `[10,10^9]` | 全部 36 個結果 | 上限、完整集合及遞增順序 |

## 建置與執行

從題目根目錄 `leetcode_1291/` 執行：

```bash
dotnet build leetcode_1291/leetcode_1291.csproj --nologo
dotnet run --no-build --project leetcode_1291/leetcode_1291.csproj
```

使用 `--no-build` 前請先完成建置。不要執行裸的 `dotnet build` 或 `dotnet test`，因為題目根目錄沒有根專案、solution 或正式測試專案。

## 實際驗證輸出

以下內容直接取自成功執行結果：

```text
LeetCode 1291 acceptance harness

Case 1: Official example 1
Input: low = 100, high = 300
Expected: [123, 234]
Actual: [123, 234]
PASS

Case 2: Official example 2
Input: low = 1000, high = 13000
Expected: [1234, 2345, 3456, 4567, 5678, 6789, 12345]
Actual: [1234, 2345, 3456, 4567, 5678, 6789, 12345]
PASS

Case 3: Minimum range without result
Input: low = 10, high = 10
Expected: []
Actual: []
PASS

Case 4: First sequential digit
Input: low = 10, high = 12
Expected: [12]
Actual: [12]
PASS

Case 5: Exact single match
Input: low = 123, high = 123
Expected: [123]
Actual: [123]
PASS

Case 6: Cross digit lengths
Input: low = 58, high = 155
Expected: [67, 78, 89, 123]
Actual: [67, 78, 89, 123]
PASS

Case 7: Range without result
Input: low = 90, high = 100
Expected: []
Actual: []
PASS

Case 8: Full constraint range
Input: low = 10, high = 1000000000
Expected: [12, 23, 34, 45, 56, 67, 78, 89, 123, 234, 345, 456, 567, 678, 789, 1234, 2345, 3456, 4567, 5678, 6789, 12345, 23456, 34567, 45678, 56789, 123456, 234567, 345678, 456789, 1234567, 2345678, 3456789, 12345678, 23456789, 123456789]
Actual: [12, 23, 34, 45, 56, 67, 78, 89, 123, 234, 345, 456, 567, 678, 789, 1234, 2345, 3456, 4567, 5678, 6789, 12345, 23456, 34567, 45678, 56789, 123456, 234567, 345678, 456789, 1234567, 2345678, 3456789, 12345678, 23456789, 123456789]
PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1291/
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
└── leetcode_1291/
    ├── Program.cs
    └── leetcode_1291.csproj
```
