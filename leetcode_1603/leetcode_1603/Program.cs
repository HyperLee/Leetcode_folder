using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1603
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1603
        /// https://leetcode.com/problems/design-parking-system/
        /// Design Parking System
        /// 
        /// 设计停车系统
        /// https://leetcode.cn/problems/design-parking-system/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        int big, medium, small;
        /// <summary>
        /// Initializes object of the ParkingSystem class
        /// 初始化各種停車格數量
        /// </summary>
        /// <param name="big"></param>
        /// <param name="medium"></param>
        /// <param name="small"></param>

        public ParkingSystem(int big, int medium, int small)
        {
            this.big = big;
            this.medium = medium;
            this.small = small;

        }

        /// <summary>
        /// 停入車輛
        /// 符合停車格式且有空車位
        /// 有車位就為true
        /// 沒有就 false
        /// 
        /// 題目設計
        /// big     : 1
        /// mediun  : 2
        /// small   : 3
        /// 
        /// 停入車輛之後, 要把停車格 扣一
        /// 因為有初始化設計
        /// 停滿就不能再停
        /// </summary>
        /// <param name="carType"></param>
        /// <returns></returns>
        public bool AddCar(int carType)
        {
            if(carType == 1)
            {
                if(big > 0)
                {
                    big--;
                    return true;
                }
            }

            if(carType == 2)
            {
                if(medium > 0)
                {
                    medium--;
                    return true;
                }
            }

            if(carType == 3)
            {
                if(small > 0)
                {
                    small--;
                    return true;
                }
            }

            return false;
        }

    }
}
