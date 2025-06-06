🌟 題目重點整理：
你給我一個整數 n，
我就要從 1 到 n 把每個數字依照「數字的總和」分成不同的群組。

然後我要找出「最大群組的大小」是多少，
最後回傳「有幾個群組是這個最大大小」。

🧠 什麼是「數字的總和」？
就是把每個數字的每一位數相加。

舉例：

123 → 1 + 2 + 3 = 6

45 → 4 + 5 = 9

7 → 7

👀 我們看個實際例子：
假設 n = 13，我們從 1 數到 13：

我們先計算每個數字的「數字總和」，並依照這個值分群：


| 數字 | 數字總和 | 群組 |
|------|---------|------|
| 1    | 1       | 群組 1： [1, 10] |
| 2    | 2       | 群組 2： [2, 11] |
| 3    | 3       | 群組 3： [3, 12] |
| 4    | 4       | 群組 4： [4, 13] |
| 5    | 5       | 群組 5： [5] |
| 6    | 6       | 群組 6： [6] |
| 7    | 7       | 群組 7： [7] |
| 8    | 8       | 群組 8： [8] |
| 9    | 9       | 群組 9： [9] |
| 10   | 1       | ← 已放在群組 1（1 + 0） |
| 11   | 2       | ← 群組 2（1 + 1） |
| 12   | 3       | ← 群組 3（1 + 2） |
| 13   | 4       | ← 群組 4（1 + 3） |
我們可以看到：

群組 1、2、3、4 裡面各有 2 個數字

其他群組都是 1 個數字

所以「最大的群組大小是 2」，而有 4 個群組達到這個大小，答案就是 4。


✅ 題目要你做的事就是：
把每個數字從 1 到 n 分類（依照數字總和）

看哪些群組人數最多

回傳這種「最大群組大小」的群組數量


🧠 解說摘要（步驟回顧）
從 1 遍歷到 n
每個數字都要根據它的「數字總和」來決定它屬於哪個群組。

用 Dictionary 儲存群組大小
groupSizes[sum] 表示這個「數字總和 sum」的群組中，有幾個數字。

統計最大群組大小
groupSizes.Values.Max() 幫我們找出人數最多的群組大小。

回傳有幾個群組達到這個最大大小
再次掃過所有群組數量，數出有幾個剛好等於最大值。