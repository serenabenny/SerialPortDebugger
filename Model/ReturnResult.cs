using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeafSoft.Units;
using NDatabase;

namespace LeafSoft.Model
{
    public enum DataType
    {
        IsSource = 0,
        IsRandom = 1,
        IsFix = 2,
        IsCheckSum=3
    }

    public class ResultMaster
    {
        private const string DBName = "Instruct.ndb";

        public ResultMaster()
        {
            ReturnResults=new List<ReturnResult>();
        }
        public int MasterId { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        public string Header { get; set; }

        public int CheckSumFristByte { get; set; }

        public int CheckSumTotalByte { get; set; }

        public List<ReturnResult> ReturnResults { get; set; }

        public string SourceInstruct { get; set; }

        public override string ToString()
        {
            string sReturn="";// = ReturnResults.Aggregate("", (current, returnResult) => current + (" " + returnResult.ToString()));
            foreach (var returnResult in ReturnResults)
            {
                returnResult.SourceInstruct = SourceInstruct;
                sReturn += " " + returnResult.ToString();
            }
            sReturn += " " + sReturn.Trim().CalcationCRC();
            sReturn = Header + " " + sReturn.Trim();
            return sReturn.Trim();
        }

        public static void InitData()
        {
            var lstMasters=new List<ResultMaster>();
            //EE 55 08 AA 0D 00 12 01 02 0A 00 D6
            //EE 55 08 AA 0D 00 12 01 02 01 00 CD
            //EE 55 08 AA 0D 00 12 13 02 00 00 DE
            var aMaster = new ResultMaster { MasterId = 1, Description = "应答帧", Header = "EE 55 08" };
            var aReturnResult = new ReturnResult
            {
                ObjectId = 1,
                MasterId = 1,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue=6
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 2,
                MasterId = 1,
                SubId = 2,
                Description = "应答标识",
                ReturnType = DataType.IsFix,
                Value = "0A 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 2, Description = "打开药盒", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 3,
                MasterId = 2,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 6
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 4,
                MasterId = 2,
                SubId = 2,
                Description = "药盒打开标识",
                ReturnType = DataType.IsFix,
                Value = "01 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 3, Description = "关闭药盒", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 5,
                MasterId = 3,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 6
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 6,
                MasterId = 3,
                SubId = 2,
                Description = "药盒关闭标识",
                ReturnType = DataType.IsFix,
                Value = "02 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 4, Description = "返回指令取药数量", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 7,
                MasterId = 4,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 6
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 8,
                MasterId = 4,
                SubId = 2,
                Description = "取药标识",
                ReturnType = DataType.IsFix,
                Value = "00 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 5, Description = "返回随机取药数量", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 9,
                MasterId = 5,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 4
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 10,
                MasterId = 5,
                SubId = 2,
                Description = "随机取药数量",
                ReturnType = DataType.IsRandom,
                MinValue = 1,
                MaxValue = 72
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 11,
                MasterId = 5,
                SubId = 3,
                Description = "药盒位置",
                ReturnType = DataType.IsSource,
                MinValue = 8,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 12,
                MasterId = 5,
                SubId = 4,
                Description = "返回指令取药数量",
                ReturnType = DataType.IsFix,
                Value = "00 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            //using (var odb2 = OdbFactory.Open(DBName))
            //{
            //    odb2.Store(lstMasters);
            //}
            aMaster.SourceInstruct = "EE 55 08 AA 0D 00 12 01 02 0A 00 D6";
            var a=aMaster.ToString();
        }

    }
    public class ReturnResult
    {

        /// <summary>
        /// ID
        /// </summary>
        public int ObjectId { get; set; }
        /// <summary>
        /// 主表ID
        /// </summary>
        public int MasterId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int SubId { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 生成类型 0-原指令取值 1-随机数 2-固定值
        /// </summary>
        public DataType ReturnType { get; set; }
        /// <summary>
        /// 生成类型 0-原指令取值时为起始字节 1-随机数时为最小随机数
        /// </summary>
        public int MinValue { get; set; }
        /// <summary>
        /// 生成类型 0-原指令取值时为取字节数 1-随机数时为最大随机数
        /// </summary>
        public int MaxValue { get; set; }
        /// <summary>
        /// 生成类型 2-固定值时的取值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 原指令
        /// </summary>
        public string SourceInstruct { get; set; }

        public Int32 GetRandom(Int32 Start, Int32 End)
        {
            var randObj = new Random();
            return randObj.Next(Start, End);
        }
        public override string ToString()
        {
            string sReturn = "";
            try
            {
                switch (ReturnType)
                {
                    case DataType.IsRandom:
                        sReturn = Convert.ToString(GetRandom(MinValue, MaxValue));
                        break;
                    case DataType.IsSource:
                        var source = SourceInstruct.Split(' ');

                        for (int i = MinValue; i < MinValue + MaxValue; i++)
                        {
                            sReturn += " " + source[i];
                        }
                        //sReturn = [SourcePosition];
                        break;
                    case DataType.IsCheckSum:
                        sReturn = "CR" + MinValue + MaxValue;
                        break;
                    default:
                        sReturn = Value;
                        break;
                }
            }
            catch (Exception ex)
            {

                throw (new Exception(ex.Message));
            }
            return sReturn.Trim();
        }

    }
}
