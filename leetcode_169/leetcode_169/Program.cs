using System.Security.Cryptography.X509Certificates;

namespace leetcode_169;

class Program
{
    /// <summary>
    /// 169. Majority Element
    /// https://leetcode.com/problems/majority-element/description/
    /// 169. 多数元素
    /// https://leetcode.cn/problems/majority-element/description/
    ///
    /// English:
    /// Given an array nums of size n, return the majority element.
    /// The majority element is the element that appears more than floor(n / 2) times.
    /// You may assume that the majority element always exists in the array.
    ///
    /// 繁體中文：
    /// 給定一個大小為 n 的陣列 nums，回傳其中的多數元素。
    /// 多數元素是指在陣列中出現次數大於 floor(n / 2) 的元素。
    /// 你可以假設多數元素一定存在於陣列中。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一: 字典統計
    /// Dictionary 統計每個出現的數字以及頻率
    /// key: 數字
    /// Value: 頻率
    /// 
    /// 統計完畢之後, 找出頻率超過 n 的第一個出現的回傳
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MajorityElement(int[] nums)
    {
        // key: nums[i], Value: 出現次數(頻率)
        Dictionary<int, int> dic = new Dictionary<int, int>();
        foreach(int num in nums)
        {
            if(dic.ContainsKey(num))
            {
                dic[num]++;
            }
            else
            {
                dic.Add(num, 1);
            }
        }

        int res = 0;
        // 題目要求, 要超過 n / 2 數量
        int n = nums.Length / 2;

        foreach(KeyValuePair<int, int> kvp in dic)
        {
            // value 超過 n
            if(kvp.Value > n)
            {
                // 回傳答案是 key
                res = kvp.Key;
                break;
            }
        }
        return res;
    }

    /// <summary>
    /// 解法二: 排序
    /// 排序後的特性
    /// 在陣列 nums 中，若某個數是 Majority Element，則它的出現次數超過了陣列長度的一半（n / 2）。
    /// 當陣列排序後，Majority Element 必定會佔據整個陣列的中間部分。
    ///  例如，假設 n = 9，n / 2 = 4，那麼排序後的陣列中間索引是 nums[4]。
    ///  Majority Element 的出現次數超過一半，因此中間索引必定是 Majority Element。
    ///  
    /// 數學性質
    /// 排序後，陣列中超過一半的元素（Majority Element）必定會覆蓋陣列的中間位置，因為：
    /// 1. 若某元素的出現次數超過 n / 2，那麼它至少佔據陣列的一半以上。
    /// 2. 當陣列排序後，該元素必然會成為中間部分的元素。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MajorityElement2(int[] nums)
    {
        // sort
        Array.Sort(nums);
        // 返回中間位置的元素
        return nums[nums.Length / 2];
    }

    /// <summary>
    /// 解法三: 摩爾投票
    /// 會用另一種情境來解說
    /// 
    /// 想象一众武林高手比武，谁会笑到最后？
    /// 我用「擂台赛」打比方：
    /// 1. 擂主登场：nums[0] 成为初始擂主，生命值为 1。
    /// 2. 挑战者出现：遍历后续元素，作为挑战者。
    /// 3. 比武：如果挑战者与擂主属于同一门派（值相同），那么擂主生命值加 1，否则擂主生命值减 1。
    /// 4. 擂主更迭：如果比武后，擂主生命值降为 0（同归于尽），那么下一个挑战者成为新的擂主，生命值为 1。
    /// 5. 最后在擂台上的那人，便是武林盟主（绝对众数）。
    /// 
    /// 为什么这样做是对的？
    /// 设出现次数最多的元素的出现次数为 a，其余元素的出现次数之和为 b=n−a。题目保证 a>b。
    /// 证明：上述过程中，每次擂主的生命值降为 0 时，相当于开了一个新的擂台赛，在 nums[i−1] 和 nums[i] 之间切一刀。这会把
    /// nums 分成若干段。依次考察这些段：
    /// - 对于除了最后一段的每一段（注意这些段的擂主不一定是绝对众数，比如绝对众数是 9，这一段是 [1,1,2,9]），设绝对众数在其
    /// 中出现了 x 次，其余元素的出现次数之和为 y，则必然有 x≤y。这可以用反证法证明，如果 x>y，那么绝对众数血多，不
    /// 可能被其余元素同归于尽，绝对众数的生命值在这段结束时必然大于 0，矛盾。由此可得 a−x>b−y，意思是，把 a 减去 x
    /// ，b 减去 y，所得到的 a′和 b′仍然满足 a′>b′。依此类推，每一段结束时，在剩余元素（未遍历到的元素）中，设出现次数最多的元素的出现次数为 a′，其余元素的出现次数之和为 
    /// b′，那么 a′>b′始终成立。
    /// 对于最后一段，由于a′>b′ ，绝对众数血多，不可能被其余元素同归于尽，绝对众数的生命值最终必然大于 0，所以最后在擂
    /// 台上的是绝对众数。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MajorityElement3(int[] nums)
    {
        int res = 0;
        int hp = 0;
        foreach(int num in nums)
        {
            if(hp == 0)
            {
                // x 是初始擂台主, 生命值為 1
                res = num;
                hp = 1;
            }
            else
            {
                // 比武, 同門加血量, 否則扣血量
                hp += num == res ? 1 : -1;
            }
        }
        return res;
    }
}
