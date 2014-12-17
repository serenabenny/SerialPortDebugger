using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
/*---------------作者：Maximus Ye----------------------*/
/*---------------时间：2013年8月16日---------------*/
/*---------------邮箱：yq@yyzq.net---------*/
/*---------------QQ：275623749---------*/
/*本软件也耗费了我不少的时间和精力，希望各位同行们尊重个人劳动成果，
 * 如果在此版本的基础上修改发布新的版本，请包含原作者信息（包括代码和UI界面相关信息)，为中国的
 * 开源事业做出一点贡献。*/
using LeafSoft.Model;
using LeafSoft.Units;
using NDatabase;

namespace LeafSoft.PartPanel
{
    public partial class ComPanel : BasePanel
    {
        private const string DBName = "Instruct.ndb";
        public ComPanel()
        {
            InitializeComponent();
            int oObj;
            using (var odb2 = OdbFactory.Open(DBName))
            {
                oObj = odb2.QueryAndExecute<ResultMaster>().Count;
            }
            if (oObj == 0)
            {
                ResultMaster.InitData();
            }

        }

        private bool DataSender_EventDataSend(byte[] data)
        {
            return Configer.SendData(data);
        }

        private byte[] GetInstruct(string InstrName, string sourceinstruct)
        {
            using (var odb2 = OdbFactory.Open(DBName))
            {
                var oObj =
                        odb2.QueryAndExecute<ResultMaster>().First(p => p.Description.Contains(InstrName));
                oObj.SourceInstruct = sourceinstruct;
                byte[] sendinstruct = oObj.ToString().StrToToHexByte();

                //var delete = lstCMD[dgCMD.SelectedRows[0].Index];
                //odb2.GetObjectFromId(delete);
                //odb2.Delete(oObj);
                return sendinstruct;
            }
        }
        private void Configer_DataReceived(object sender, byte[] data)
        {
            if (txtCmd.Visible == true)
            {
                if (data.Length > 1)
                {
                    txtCmd.BeginInvoke(new MethodInvoker(delegate
                    {
                        txtCmd.AppendText(new UTF8Encoding().GetString(data).Replace("\r", "\r\n"));
                        txtCmd.SelectionStart = txtCmd.Text.Length;
                    }));
                }
            }
            else
            {
                if (data.Length > 1)
                {
                    DataReceiver.AddData(data);
                }
            }

            if (DataSender.AutoResult && data.Length > 6)
            {
                string aSendResult = data.ByteToHexStr();
                List<string> strBuilder = new List<string>();

                if (data[0]==0xfa && data[1]==0xf5)
                {
                    switch (data[3])
                    {
                        case 0x27://启动
                            strBuilder.Add("盒剂-启动");
                            break;
                        case 0x20://进篮子
                            strBuilder.Add("盒剂-应答帧");
                            strBuilder.Add("盒剂-进篮子");
                            break;
                        case 0x12://发药
                            strBuilder.Add("盒剂-应答帧");
                            strBuilder.Add("盒剂-发药结果");
                            break;
                        case 0x22://送篮子
                            strBuilder.Add("盒剂-应答帧");
                            strBuilder.Add("盒剂-送篮子");
                            break;
                        default:
                            strBuilder.Add("盒剂-应答帧");
                            break;
                    }
                }
                else
                {
                    switch (data[6])
                    {
                        case 0x21://抽屉取药
                        case 0x23://抽屉加药
                            strBuilder.Add("应答帧");
                            strBuilder.Add("打开药盒");
                            strBuilder.Add("关闭药盒");
                            strBuilder.Add("随机数量");
                            break;
                        case 0x22://抽屉盘点
                        case 0x12://智能药盒盘点
                            strBuilder.Add("应答帧");
                            strBuilder.Add("随机数量");
                            break;
                        case 0x14://电子标签
                            strBuilder.Add("应答帧");
                            strBuilder.Add("电子标签00");
                            strBuilder.Add("电子标签01");
                            strBuilder.Add("电子标签02");
                            break;
                        default:
                            strBuilder.Add("应答帧");
                            strBuilder.Add("打开药盒");
                            strBuilder.Add("关闭药盒");
                            strBuilder.Add("指令数量");
                            break;
                    }
                }
                

                foreach (var instruct in strBuilder)
                {
                    byte[] returnresult = GetInstruct(instruct, aSendResult);
                    Configer.SendData(returnresult);
                    DataReceiver.AddData(returnresult, true);
                    Thread.Sleep(1000);
                }
                //aMaster.SourceInstruct = "EE 55 08 AA 0D 00 12 01 02 0A 00 D6";
                //var a=aMaster.ToString();


                ////应答帧
                //aSendResult[aSendResult.Length - 3] = 0xA1;
                //Configer.SendData(aSendResult);

                //Thread.Sleep(1000);
                ////打开药盒
                ////byte[] aSendResult = data;
                //aSendResult[aSendResult.Length - 3] = 0x01;
                //Configer.SendData(aSendResult);
                //Thread.Sleep(1000);
                ////关上药盒
                ////byte[] aSendResult = data;
                //aSendResult[aSendResult.Length - 3] = 0x02;
                //Configer.SendData(aSendResult);
                //Thread.Sleep(1000);
                ////返回数据
                ////byte[] aSendResult = data;
                //aSendResult[aSendResult.Length - 3] = 0x00;
                //Configer.SendData(aSendResult);
                //Thread.Sleep(1000);

            }
        }

        public override void ClearSelf()
        {
            Configer.ClearSelf();
        }

        private void btnSuper_Click(object sender, EventArgs e)
        {
            txtCmd.Visible = !txtCmd.Visible;
            txtCmd.Focus();
        }

        private bool txtCmd_DataSend(byte[] cmd)
        {
            return Configer.SendData(cmd);
        }

        private void MS_ClearCMD_Click(object sender, EventArgs e)
        {
            txtCmd.Clear();
        }
    }
}
