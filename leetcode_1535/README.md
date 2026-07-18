# 1535. Find the Winner of an Array Game／找出陣列遊戲的贏家

在每一回合讓目前贏家與下一個數字比較；先達成 `k` 場連勝者就是答案。本專案以單次
掃描保留目前 champion 與連勝數，公開 API 是不輸出且不修改輸入的純函式。

- [LeetCode English](https://leetcode.com/problems/find-the-winner-of-an-array-game/)
- [LeetCode 中文](https://leetcode.cn/problems/find-the-winner-of-an-array-game/)

## 題目說明與限制條件

給定由相異整數構成的 `arr` 與正整數 `k`，每回合比較目前贏家與尚未比較的下一個值；
較大的值獲勝並留下繼續比賽。回傳第一個連贏 `k` 場的值。若 `k` 大於可明確模擬的
回合數，陣列最大值最終必然持續獲勝。

- `2 <= arr.Length <= 100,000`
- `1 <= arr[i] <= 1,000,000`，且所有值相異
- `1 <= k <= 1,000,000,000`
- `GetWinner` 只處理題目保證的有效輸入，未額外定義無效輸入行為

## 演算法、不變量與取捨

`GetWinner` 將 `arr[0]` 設為 champion，依序讀取其餘元素：較小元素使 champion 的
連勝數加一；較大元素則取代 champion，並將連勝重設為一。每次更新後立即檢查是否達成
`k` 場。

不變量是：掃描到索引 `i` 時，`champion` 是 `arr[0..i]` 的最大值，`consecutiveWins`
正確表示它從上次成為 champion 起已連勝的場數。因此若掃描結束仍未提早回傳，champion
就是全域最大值，必會贏得後續所有比賽。

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n)`；達成 `k` 時可提早結束 |
| 結果空間 | `O(1)`；只回傳一個整數 |
| 輔助空間 | `O(1)`；僅使用 champion、連勝數與索引 |

直接以佇列逐回合把落敗者移到尾端雖直觀，但當 `k` 很大時會重複處理同一批元素，最差會
隨 `k` 成長。一次掃描利用「最大值不會輸」的性質，避免這個陷阱，也不需要修改 `arr`。

## 走查

以 `arr = [2, 1, 3, 5, 4, 6, 7]`、`k = 2` 為例：

| 回合 | champion 與對手 | 本回合贏家 | 連勝數 |
| --- | --- | ---: | ---: |
| 1 | `2` vs `1` | `2` | 1 |
| 2 | `2` vs `3` | `3` | 1 |
| 3 | `3` vs `5` | `5` | 1 |
| 4 | `5` vs `4` | `5` | 2 |

第 4 回合的 `5` 達成兩連勝，立即回傳。這也說明為何「新 champion 的第一場勝利」必須
把連勝重設為 1，而不是 0 或沿用舊 champion 的數字。

## Acceptance Harness

`Main` 提供八個確定性案例。每案複製輸入快照，只有答案符合預期且呼叫後陣列逐元素不變
才會 PASS；任何失敗都會將 process exit code 設為 1。最大長度案例採緊湊格式顯示，避免
輸出 100,000 個數值。

| # | 案例 | 輸入 | `k` | 預期 |
| --- | --- | --- | ---: | ---: |
| 1 | 官方範例 1 | `[2, 1, 3, 5, 4, 6, 7]` | 2 | 5 |
| 2 | 官方範例 2 | `[3, 2, 1]` | 10 | 3 |
| 3 | 官方範例 3 | `[1, 9, 8, 2, 3, 7, 6, 4, 5]` | 7 | 9 |
| 4 | 官方範例 4 | `[1, 11, 22, 33, 44, 55, 66, 77, 88, 99]` | 1,000,000,000 | 99 |
| 5 | 兩元素 | `[1, 2]` | 1 | 2 |
| 6 | champion 保持連勝 | `[6, 1, 5, 4, 3, 2]` | 3 | 6 |
| 7 | 值上限 | `[1,000,000, 1]` | 1 | 1,000,000 |
| 8 | 最大長度 | `1..100,000` | 1,000,000,000 | 100,000 |

## 建置與執行

從 repository 根目錄執行：

```bash
dotnet build leetcode_1535/leetcode_1535/leetcode_1535.csproj --nologo
dotnet run --no-build --project leetcode_1535/leetcode_1535/leetcode_1535.csproj
```

若直接以 `leetcode_1535/` 作為 workspace，從題目根目錄執行：

```bash
dotnet build leetcode_1535/leetcode_1535.csproj --nologo
dotnet run --no-build --project leetcode_1535/leetcode_1535.csproj
```

以下為 fresh run 的完整輸出：

```text
Case: Official example 1
Input: [2, 1, 3, 5, 4, 6, 7]
k: 2
Expected: 5
Actual: 5
Input preserved: True
Result: PASS

Case: Official example 2
Input: [3, 2, 1]
k: 10
Expected: 3
Actual: 3
Input preserved: True
Result: PASS

Case: Official example 3
Input: [1, 9, 8, 2, 3, 7, 6, 4, 5]
k: 7
Expected: 9
Actual: 9
Input preserved: True
Result: PASS

Case: Official example 4
Input: [1, 11, 22, 33, 44, 55, 66, 77, 88, 99]
k: 1000000000
Expected: 99
Actual: 99
Input preserved: True
Result: PASS

Case: Two elements
Input: [1, 2]
k: 1
Expected: 2
Actual: 2
Input preserved: True
Result: PASS

Case: Champion keeps streak
Input: [6, 1, 5, 4, 3, 2]
k: 3
Expected: 6
Actual: 6
Input preserved: True
Result: PASS

Case: Value upper bound
Input: [1000000, 1]
k: 1
Expected: 1000000
Actual: 1000000
Input preserved: True
Result: PASS

Case: Maximum length
Input: [1, 2, 3, ..., 99998, 99999, 100000] (Length: 100000)
k: 1000000000
Expected: 100000
Actual: 100000
Input preserved: True
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1535/
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
│       ├── plans/2026-07-18-leetcode-1535-net10-migration.md
│       └── specs/2026-07-18-leetcode-1535-net10-migration-design.md
└── leetcode_1535/
    ├── Program.cs
    └── leetcode_1535.csproj
```
