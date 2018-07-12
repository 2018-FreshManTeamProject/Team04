using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Util
{
    class Range
    {
        private int first;//範囲の最初の番号
        private int end;//終端番号

        public Range(int first , int end)
        {
            this.first = first;
            this.end = end;
        }

        public int First()
        {
            return first;
        }

        public int End()
        {
            return end;
        }

        public bool IsWithin(int num)
        {
            //最初の番号よりも小さい
            if(num < first)
            {
                return false;
            }
            //終端番号より大きい
            if(num > end)
            {
                return false;
            }
            //範囲内
            return true;
        }

        public bool IsOutOfRange()
        {
            return first >= end;
        }

        public bool IsOutOfRange(int num)
        {
            //範囲内でなければtrue
            return !IsWithin(num);
        }
    }
}
