namespace leetcode_3510;

class Program
{
    /// <summary>
    /// 3510. Minimum Pair Removal to Sort Array II
    /// https://leetcode.com/problems/minimum-pair-removal-to-sort-array-ii
    ///
    /// English:
    /// Given an array nums, you can perform the following operation any number of times:
    /// - Select the adjacent pair with the minimum sum in nums. If multiple such pairs exist, choose the leftmost one.
    /// - Replace the pair with their sum.
    /// Return the minimum number of operations needed to make the array non-decreasing.
    /// An array is non-decreasing if each element is greater than or equal to its previous element.
    ///
    /// 繁體中文：
    /// 給定一個陣列 nums，你可以重複執行以下操作任意次數：
    /// - 在 nums 中選擇相鄰且和最小的一對；若存在多個，選取最左邊的那一對。
    /// - 用它們的和替換該對（將兩個元素合併為一個元素）。
    /// 回傳使陣列變為非遞減所需的最少操作次數。
    /// 若陣列為非遞減，則對於每個元素都滿足 a[i] >= a[i-1]。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
    }
}
