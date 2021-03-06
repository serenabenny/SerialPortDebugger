﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace LeafSoft.Units
{
    public static class ExtMethod
    {
        /// <summary>
        /// 左边开始取字串子集
        /// </summary>
        /// <param name="strSrc">字符串</param>
        /// <param name="iCount">位数</param>
        /// <returns></returns>
        public static string Left(this string strSrc, int iCount)
        {
            if (strSrc == null || strSrc.Length <= iCount)
                return strSrc;
            return strSrc.Substring(0, iCount);
        }
        /// <summary>
        /// 从右边开始取字串子集
        /// </summary>
        /// <param name="strSrc">字符串</param>
        /// <param name="iCount">位数</param>
        /// <returns></returns>
        public static string Right(this string strSrc, int iCount)
        {
            if (strSrc == null || strSrc.Length <= iCount)
                return strSrc;
            return strSrc.Substring(strSrc.Length - iCount);
        }
        /// <summary>
        /// 16进制字节数组转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStr(this byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                returnStr = bytes.Aggregate(returnStr, (current, t) => current + " " + t.ToString("X2"));
            }
            return returnStr;
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrToToHexByte(this string hexString)
        {

            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 计算校验码
        /// </summary>
        /// <param name="Instruct"></param>
        /// <returns></returns>
        public static string CalcationCRC(this string Instruct)
        {
            try
            {
                var sByte = Instruct.Split(' ');
                var nSum = 0;
                for (var j = 0; j < sByte.Length ; j++)
                {
                    nSum = nSum + int.Parse(sByte[j], NumberStyles.HexNumber);
                }
                nSum = nSum % 256;
                return nSum.ToString("X").Right(2).PadLeft(2, '0');
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }

    
    public class RNG 
    { 
        private static RNGCryptoServiceProvider rngp = new RNGCryptoServiceProvider(); 
        private static byte[] rb = new byte[4];

        /// <summary> 
        /// 产生一个非负数的乱数 
        /// </summary> 
        public static int Next() 
        { 
            rngp.GetBytes(rb); 
            int value = BitConverter.ToInt32(rb, 0); 
            if (value < 0) value = -value; 
            return value; 
        } 
        /// <summary> 
        /// 产生一个非负数且最大值在 max 以下的乱数 
        /// </summary> 
        /// <param name="max">最大值</param> 
        public static int Next(int max) 
        { 
            rngp.GetBytes(rb); 
            int value = BitConverter.ToInt32(rb, 0); 
            value = value % (max + 1); 
            if (value < 0) value = -value; 
            return value; 
        } 
        /// <summary> 
        /// 产生一个非负数且最小值在 min 以上最大值在 max 以下的乱数 
        /// </summary> 
        /// <param name="min">最小值</param> 
        /// <param name="max">最大值</param> 
        public static int Next(int min, int max) 
        { 
            int value = Next(max - min) + min; 
            return value; 
        } 
    }

}
