namespace leetcode_3946;

class Program
{
    /// <summary>
    /// 3946. Maximum Number of Items From Sale I
    /// https://leetcode.com/problems/maximum-number-of-items-from-sale-i/description/
    /// 3946. 购买最多物品数目 I
    /// https://leetcode.cn/problems/maximum-number-of-items-from-sale-i/description/
    ///
    /// English
    /// You are given a 2D integer array items, where items[i] = [factori, pricei] represents the ith item. You are also given an integer budget.
    /// There are unlimited copies of each item available for purchase.You may buy any number of copies of any items such that the total cost of the purchased copies is at most budget.
    /// After buying items, you may receive free copies according to the following rules:
    /// 
    /// For each item i that you bought at least one copy of, you receive one free copy of every item j such that j != i and factori divides factorj.
    /// Buying multiple copies of the same item i does not give additional free copies through item i.
    /// The same item j can be received multiple times for free if it is received from purchases of different item types.
    /// 
    /// Return the maximum total number of item copies you can obtain, including both purchased copies and free copies, while spending at most budget on purchased items.
    ///
    /// 繁體中文
    /// 給定一個二維整數陣列 items，其中 items[i] = [factorᵢ, priceᵢ] 代表第 i 種物品的因數與單價。另給定一個整數 budget。
    /// 每種物品都有無限件可供購買。你可以購買任意種類及數量的物品，購買總金額不得超過 budget。
    /// 購買後，你可以依下列規則獲得免費物品。
    /// 
    /// 對於每種你至少購買一件的物品 i，只要 j != i 且 factorᵢ 能整除 factorⱼ，你就會免費獲得一件物品 j。
    /// 購買多件同一種物品 i，不會透過該物品額外獲得免費物品。
    /// 如果購買不同種類的物品都能贈送物品 j，則同一種物品 j 可以免費獲得多件。
    /// 
    /// 回傳在購買總金額不超過 budget 的情況下，可以獲得的最大物品總件數，包含購買及免費獲得的物品。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
