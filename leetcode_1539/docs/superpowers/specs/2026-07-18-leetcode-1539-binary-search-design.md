# LeetCode 1539 Binary Search Solution Design

## 目標與範圍

在已完成的 .NET 10 `leetcode_1539` 專案中保留逐一枚舉解法，新增具獨立教學價值的
二分搜尋解法。所有變更限制在 `leetcode_1539/`，不改動專案格式、外部套件或 Issue #2
已完成的 checkbox。本文件擴充原 migration 設計；原文件保留為首次遷移的歷史紀錄。

## API 與演算法

保留 `FindKthPositive(int[] arr, int k)`，新增
`FindKthPositive2(int[] arr, int k)`。第二個方法在索引半開區間 `[left, right)` 執行
lower-bound 搜尋。索引 `i` 前理應出現 `i + 1` 個正整數，因此
`arr[i] - i - 1` 是截至 `arr[i]` 的缺失數量。

搜尋第一個缺失數量不少於 `k` 的索引；若中點缺失數量小於 `k`，捨棄中點及左側，否則
保留中點並收縮右界。結束時 `left` 等於答案前存在於陣列中的元素數量，所以答案是
`left + k`。時間為 `O(log n)`，結果與輔助空間皆為 `O(1)`，且不修改輸入。

## 驗收與文件

`Main` 以兩個具名 delegates 執行既有九個 fixtures。每次呼叫都使用獨立 clone，分別
驗證答案與輸入保存，合計三十六項檢查；成功摘要為
`Summary: 36/36 checks passed.`，失敗時 exit code 為 1。

README 新增兩種演算法的公式、走查、複雜度與取捨比較，並以 fresh run 更新唯一
`text` transcript。AGENTS 同步兩個 API 與驗收契約。發布使用單一 follow-up commit、
獨立唯讀 review、draft PR、expected-head squash merge 與 post-merge gate，不更新
Issue #2。
