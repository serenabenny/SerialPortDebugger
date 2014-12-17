using System;
using System.Collections.Generic;
using System.Linq;
using LeafSoft.Units;
using NDatabase;

namespace LeafSoft.Model
{
    public enum DataType
    {
        IsSource = 0,
        IsRandom = 1,
        IsFix = 2,
        IsCheckSum = 3,
        IsAddOne = 4,
        IsLength = 5,
        IsRecordData = 6
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

        public string RecordData { get; set; }
        public override string ToString()
        {
            string sReturn = "";
            try
            {
                switch (ReturnType)
                {
                    case DataType.IsRandom:
                        sReturn = RNG.Next(MinValue, MaxValue).ToString("X2");
                        break;
                    case DataType.IsSource:
                        var source = SourceInstruct.Split(' ');
                        int nLength = 0;
                        if (MaxValue > 0)
                        {
                            nLength = MinValue + MaxValue;
                        }
                        else
                        {
                            nLength = source.Count();
                        }
                        for (int i = MinValue; i < nLength; i++)
                        {
                            sReturn += " " + source[i];
                        }
                        //sReturn = [SourcePosition];
                        break;
                    case DataType.IsRecordData:
                        var source2 = RecordData.Split(' ');
                        int nLength2 = 0;
                        if (MaxValue > 0)
                        {
                            nLength2 = MinValue + MaxValue;
                        }
                        else
                        {
                            nLength2 = source2.Count();
                        }
                        for (int i = MinValue; i < nLength2; i++)
                        {
                            sReturn += " " + source2[i];
                        }
                        break;
                    case DataType.IsCheckSum:
                        sReturn = "CR";
                        break;
                    case DataType.IsAddOne:
                        var source1 = SourceInstruct.Split(' ');
                        int sValue = Convert.ToByte(source1[MinValue], 16) + 1;
                        sReturn = sValue.ToString("X2");
                        break;
                    case DataType.IsLength:
                        sReturn = "LN";
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
    public class ResultMaster
    {
        private const string DBName = "Instruct.ndb";

        public ResultMaster()
        {
            ReturnResults = new List<ReturnResult>();
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        public int MasterId { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 指令头
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 计算校验码首字节地址
        /// </summary>
        public int CheckSumFristByte { get; set; }
        /// <summary>
        /// 计算校验码字节数，当值设为0时，意思为首字节地址至指令尾进行计算
        /// </summary>
        public int CheckSumTotalByte { get; set; }
        /// <summary>
        /// 是否取消已经记录的数据
        /// </summary>
        public bool IsCancelRecordData { get; set; }
        /// <summary>
        /// 是否记录生成的数据
        /// </summary>
        public bool IsRecordData { get; set; }
        /// <summary>
        /// 记录的数据
        /// </summary>
        public string RecordData { get; set; }
        /// <summary>
        /// 记录数据首地址
        /// </summary>
        public int RecordDataFristData { get; set; }
        /// <summary>
        /// 记录数据的长度
        /// </summary>
        public int RecordDataTotoalByte { get; set; }
        /// <summary>
        /// 指令集
        /// </summary>
        public List<ReturnResult> ReturnResults { get; set; }
        /// <summary>
        /// 源指令
        /// </summary>
        public string SourceInstruct { get; set; }

        public override string ToString()
        {
            string sReturn = "";// = ReturnResults.Aggregate("", (current, returnResult) => current + (" " + returnResult.ToString()));
            foreach (var returnResult in ReturnResults)
            {
                returnResult.SourceInstruct = SourceInstruct;
                returnResult.RecordData = RecordData;
                sReturn += " " + returnResult.ToString();
            }
            sReturn = Header + " " + sReturn.Trim();
            var source = sReturn.Split(' ');

            if (sReturn.IndexOf("LN") > 0)
            {
                sReturn = sReturn.Replace("LN", (source.Count() - 7).ToString("X2"));
            }
            if (sReturn.IndexOf("CR") > 0)
            {

                string calcsum = "";
                if (source.Count() > CheckSumFristByte)
                {
                    var checksum = "";
                    int totalbyte = 0;
                    if (CheckSumTotalByte == 0)
                    {
                        totalbyte = source.Count() - CheckSumFristByte;
                    }
                    else
                    {
                        totalbyte = CheckSumTotalByte;
                    }
                    for (int i = CheckSumFristByte - 1; i < CheckSumFristByte + totalbyte - 1; i++)
                    {
                        checksum += " " + source[i];
                    }
                    calcsum = checksum.Trim().CalcationCRC();
                }
                else
                {
                    calcsum = "00";
                }
                sReturn = sReturn.Replace("CR", calcsum);
            }
            //else
            //{
            //    sReturn += " " + sReturn.Trim().CalcationCRC();
            //}


            return sReturn.Trim();
        }

        public static void InitData()
        {
            var lstMasters = new List<ResultMaster>();
            var aMaster = new ResultMaster();
            var aReturnResult = new ReturnResult();
            #region 盒剂

            #region 盒剂-应答帧
            aMaster = new ResultMaster
            {
                MasterId = 10001,
                CheckSumFristByte = 8,
                CheckSumTotalByte = 0,
                Description = "盒剂-应答帧",
                Header = "FA F5"
            };
            aReturnResult = new ReturnResult
            {
                ObjectId = 1000101,
                MasterId = 10001,
                SubId = 1,
                Description = "流水号",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 2
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000102,
                MasterId = 10001,
                SubId = 2,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000103,
                MasterId = 10001,
                SubId = 3,
                Description = "应答标识",
                ReturnType = DataType.IsFix,
                Value = "0A 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            lstMasters.Add(aMaster);

            #endregion

            #region 盒剂-启动
            aMaster = new ResultMaster
            {
                MasterId = 10002,
                CheckSumFristByte = 8,
                CheckSumTotalByte = 0,
                Description = "盒剂-启动",
                Header = "FA F5"
            };
            aReturnResult = new ReturnResult
            {
                ObjectId = 1000201,
                MasterId = 10002,
                SubId = 1,
                Description = "启动",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 5
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);
            #endregion

            #region 盒剂-进篮子
            aMaster = new ResultMaster
            {
                MasterId = 10003,
                CheckSumFristByte = 8,
                CheckSumTotalByte = 0,
                Description = "盒剂-进篮子",
                IsRecordData = true,
                RecordDataFristData = 9,
                RecordDataTotoalByte = 4,
                Header = "FA F5"
            };
            aReturnResult = new ReturnResult
            {
                ObjectId = 1000301,
                MasterId = 10003,
                SubId = 1,
                Description = "流水号",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000302,
                MasterId = 10003,
                SubId = 2,
                Description = "命令类型",
                ReturnType = DataType.IsAddOne,
                MinValue = 4
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000303,
                MasterId = 10003,
                SubId = 3,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000304,
                MasterId = 10003,
                SubId = 4,
                Description = "状态位",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000305,
                MasterId = 10003,
                SubId = 5,
                Description = "长度位",
                ReturnType = DataType.IsLength
            };

            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000306,
                MasterId = 10003,
                SubId = 6,
                Description = "窗口号",
                ReturnType = DataType.IsSource,
                MinValue = 8,
                MaxValue = 2
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000307,
                MasterId = 10003,
                SubId = 7,
                Description = "卡号1",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000308,
                MasterId = 10003,
                SubId = 8,
                Description = "卡号2",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000309,
                MasterId = 10003,
                SubId = 9,
                Description = "卡号3",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000310,
                MasterId = 10003,
                SubId = 10,
                Description = "卡号4",
                ReturnType = DataType.IsRandom,
                MinValue = 0,
                MaxValue = 30
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000311,
                MasterId = 10003,
                SubId = 11,
                Description = "错误码",
                ReturnType = DataType.IsFix,
                Value = "00 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            lstMasters.Add(aMaster);
            #endregion

            #region 盒剂-发药结果
            aMaster = new ResultMaster
            {
                MasterId = 10004,
                CheckSumFristByte = 8,
                CheckSumTotalByte = 0,
                Description = "盒剂-发药结果",
                Header = "FA F5"
            };
            aReturnResult = new ReturnResult
            {
                ObjectId = 1000401,
                MasterId = 10004,
                SubId = 1,
                Description = "流水号",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000402,
                MasterId = 10004,
                SubId = 2,
                Description = "命令类型",
                ReturnType = DataType.IsAddOne,
                MinValue = 4
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000403,
                MasterId = 10004,
                SubId = 3,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000404,
                MasterId = 10004,
                SubId = 4,
                Description = "状态位",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000405,
                MasterId = 10004,
                SubId = 5,
                Description = "长度位",
                ReturnType = DataType.IsLength
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000406,
                MasterId = 10004,
                SubId = 6,
                Description = "返回发药结果",
                ReturnType = DataType.IsSource,
                MinValue = 8,
                MaxValue = 0

            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);
            #endregion

            #region 盒剂-送篮子
            aMaster = new ResultMaster
            {
                MasterId = 10005,
                CheckSumFristByte = 8,
                CheckSumTotalByte = 0,
                Description = "盒剂-送篮子",
                Header = "FA F5"
            };
            aReturnResult = new ReturnResult
            {
                ObjectId = 1000501,
                MasterId = 10005,
                SubId = 1,
                Description = "流水号",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000502,
                MasterId = 10005,
                SubId = 2,
                Description = "命令类型",
                ReturnType = DataType.IsAddOne,
                MinValue = 4
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000503,
                MasterId = 10005,
                SubId = 3,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000504,
                MasterId = 10005,
                SubId = 4,
                Description = "状态位",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000505,
                MasterId = 10005,
                SubId = 5,
                Description = "长度位",
                ReturnType = DataType.IsLength
            };

            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000506,
                MasterId = 10005,
                SubId = 6,
                Description = "窗口号",
                ReturnType = DataType.IsSource,
                MinValue = 8,
                MaxValue = 2
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000507,
                MasterId = 10005,
                SubId = 7,
                Description = "卡号1",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000508,
                MasterId = 10005,
                SubId = 8,
                Description = "卡号2",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000509,
                MasterId = 10005,
                SubId = 9,
                Description = "卡号3",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000510,
                MasterId = 10005,
                SubId = 10,
                Description = "卡号4",
                ReturnType = DataType.IsRecordData,
                MinValue = 4,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000511,
                MasterId = 10005,
                SubId = 11,
                Description = "错误码",
                ReturnType = DataType.IsFix,
                Value = "00 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            lstMasters.Add(aMaster);
            #endregion

            #region 盒剂-上缓存架
            aMaster = new ResultMaster
            {
                MasterId = 10006,
                CheckSumFristByte = 8,
                CheckSumTotalByte = 0,
                Description = "盒剂-上缓存架",
                IsCancelRecordData = true,
                Header = "FA F5"
            };
            aReturnResult = new ReturnResult
            {
                ObjectId = 1000601,
                MasterId = 10006,
                SubId = 1,
                Description = "流水号",
                ReturnType = DataType.IsSource,
                MinValue = 3,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000602,
                MasterId = 10006,
                SubId = 2,
                Description = "命令类型",
                ReturnType = DataType.IsFix,
                Value = "24"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000603,
                MasterId = 10006,
                SubId = 3,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000604,
                MasterId = 10006,
                SubId = 4,
                Description = "状态位",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000605,
                MasterId = 10006,
                SubId = 5,
                Description = "长度位",
                ReturnType = DataType.IsLength
            };

            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000606,
                MasterId = 10006,
                SubId = 6,
                Description = "窗口号",
                ReturnType = DataType.IsSource,
                MinValue = 8,
                MaxValue = 2
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000607,
                MasterId = 10006,
                SubId = 7,
                Description = "卡号1",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000608,
                MasterId = 10006,
                SubId = 8,
                Description = "卡号2",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000609,
                MasterId = 10006,
                SubId = 9,
                Description = "卡号3",
                ReturnType = DataType.IsFix,
                Value = "00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000610,
                MasterId = 10006,
                SubId = 10,
                Description = "卡号4",
                ReturnType = DataType.IsRecordData,
                MinValue = 4,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 1000611,
                MasterId = 10006,
                SubId = 11,
                Description = "错误码",
                ReturnType = DataType.IsFix,
                Value = "00 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            lstMasters.Add(aMaster);
            #endregion
            #endregion

            //EE 55 08 AA 0D 00 12 01 02 0A 00 D6
            //EE 55 08 AA 0D 00 12 01 02 01 00 CD
            //EE 55 08 AA 0D 00 12 13 02 00 00 DE
            #region 管控

            aMaster = new ResultMaster { MasterId = 1, CheckSumFristByte = 4, CheckSumTotalByte = 8, Description = "应答帧", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 1,
                MasterId = 1,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 4,
                MaxValue = 6
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
            aReturnResult = new ReturnResult
            {
                ObjectId = 3,
                MasterId = 1,
                SubId = 3,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 2, CheckSumFristByte = 4, CheckSumTotalByte = 8, Description = "打开药盒", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 4,
                MasterId = 2,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 4,
                MaxValue = 6
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 5,
                MasterId = 2,
                SubId = 2,
                Description = "药盒打开标识",
                ReturnType = DataType.IsFix,
                Value = "01 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 6,
                MasterId = 2,
                SubId = 3,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 3, CheckSumFristByte = 4, CheckSumTotalByte = 8, Description = "关闭药盒", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 7,
                MasterId = 3,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 4,
                MaxValue = 6
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 8,
                MasterId = 3,
                SubId = 2,
                Description = "药盒关闭标识",
                ReturnType = DataType.IsFix,
                Value = "02 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 9,
                MasterId = 3,
                SubId = 3,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 4, CheckSumFristByte = 4, CheckSumTotalByte = 8, Description = "指令数量", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 10,
                MasterId = 4,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 4,
                MaxValue = 6
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 11,
                MasterId = 4,
                SubId = 2,
                Description = "取药标识",
                ReturnType = DataType.IsFix,
                Value = "00 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 12,
                MasterId = 4,
                SubId = 3,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 5, CheckSumFristByte = 4, CheckSumTotalByte = 8, Description = "随机数量", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 13,
                MasterId = 5,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 4,
                MaxValue = 4
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 14,
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
                ObjectId = 15,
                MasterId = 5,
                SubId = 3,
                Description = "药盒位置",
                ReturnType = DataType.IsSource,
                MinValue = 9,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 16,
                MasterId = 5,
                SubId = 4,
                Description = "返回指令取药数量",
                ReturnType = DataType.IsFix,
                Value = "00 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 17,
                MasterId = 5,
                SubId = 5,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 6, CheckSumFristByte = 4, CheckSumTotalByte = 8, Description = "电子标签00", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 18,
                MasterId = 6,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 4,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 19,
                MasterId = 6,
                SubId = 2,
                Description = "药盒位置",
                ReturnType = DataType.IsSource,
                MinValue = 9,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 20,
                MasterId = 6,
                SubId = 3,
                Description = "指令标识",
                ReturnType = DataType.IsSource,
                MinValue = 6,
                MaxValue = 2
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 21,
                MasterId = 6,
                SubId = 4,
                Description = "电子标签字节数",
                ReturnType = DataType.IsFix,
                Value = "08"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 22,
                MasterId = 6,
                SubId = 5,
                Description = "药盒位置",
                ReturnType = DataType.IsSource,
                MinValue = 9,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 23,
                MasterId = 5,
                SubId = 6,
                Description = "指令标识",
                ReturnType = DataType.IsFix,
                Value = "00 00"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 24,
                MasterId = 5,
                SubId = 7,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 7, CheckSumFristByte = 4, CheckSumTotalByte = 8, Description = "电子标签01", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 25,
                MasterId = 7,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 4,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 26,
                MasterId = 7,
                SubId = 2,
                Description = "药盒位置",
                ReturnType = DataType.IsSource,
                MinValue = 9,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 27,
                MasterId = 7,
                SubId = 3,
                Description = "电子标签第一节",
                ReturnType = DataType.IsFix,
                Value = "01"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 28,
                MasterId = 7,
                SubId = 4,
                Description = "指令标识",
                ReturnType = DataType.IsSource,
                MinValue = 7,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 29,
                MasterId = 7,
                SubId = 5,
                Description = "电子标签1",
                ReturnType = DataType.IsRandom,
                MinValue = 1,
                MaxValue = 255
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 30,
                MasterId = 7,
                SubId = 6,
                Description = "电子标签2",
                ReturnType = DataType.IsRandom,
                MinValue = 1,
                MaxValue = 255
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 31,
                MasterId = 7,
                SubId = 7,
                Description = "电子标签3",
                ReturnType = DataType.IsRandom,
                MinValue = 1,
                MaxValue = 255
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 32,
                MasterId = 7,
                SubId = 8,
                Description = "电子标签4",
                ReturnType = DataType.IsRandom,
                MinValue = 1,
                MaxValue = 255
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 33,
                MasterId = 7,
                SubId = 9,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);

            aMaster = new ResultMaster { MasterId = 8, CheckSumFristByte = 4, CheckSumTotalByte = 8, Description = "电子标签02", Header = "EE 55 08" };
            aReturnResult = new ReturnResult
            {
                ObjectId = 44,
                MasterId = 8,
                SubId = 1,
                Description = "帧ID",
                ReturnType = DataType.IsSource,
                MinValue = 4,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 45,
                MasterId = 8,
                SubId = 2,
                Description = "药盒位置",
                ReturnType = DataType.IsSource,
                MinValue = 9,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 46,
                MasterId = 8,
                SubId = 3,
                Description = "电子标签第二节",
                ReturnType = DataType.IsFix,
                Value = "02"
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 47,
                MasterId = 8,
                SubId = 4,
                Description = "指令标识",
                ReturnType = DataType.IsSource,
                MinValue = 7,
                MaxValue = 1
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 48,
                MasterId = 8,
                SubId = 5,
                Description = "电子标签1",
                ReturnType = DataType.IsRandom,
                MinValue = 1,
                MaxValue = 255
            };
            aMaster.ReturnResults.Add(aReturnResult);
            aReturnResult = new ReturnResult
            {
                ObjectId = 49,
                MasterId = 8,
                SubId = 6,
                Description = "电子标签2",
                ReturnType = DataType.IsRandom,
                MinValue = 1,
                MaxValue = 255
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 50,
                MasterId = 8,
                SubId = 7,
                Description = "电子标签3",
                ReturnType = DataType.IsRandom,
                MinValue = 1,
                MaxValue = 255
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 51,
                MasterId = 8,
                SubId = 8,
                Description = "电子标签4",
                ReturnType = DataType.IsRandom,
                MinValue = 1,
                MaxValue = 255
            };
            aMaster.ReturnResults.Add(aReturnResult);

            aReturnResult = new ReturnResult
            {
                ObjectId = 52,
                MasterId = 8,
                SubId = 9,
                Description = "校验码",
                ReturnType = DataType.IsCheckSum
            };
            aMaster.ReturnResults.Add(aReturnResult);
            lstMasters.Add(aMaster);
            #endregion
            using (var odb2 = OdbFactory.Open(DBName))
            {
                odb2.Store(lstMasters);
            }
            //aMaster.SourceInstruct = "EE 55 08 AA 0D 00 12 01 02 0A 00 D6";
            //var a=aMaster.ToString();
        }

    }

}
