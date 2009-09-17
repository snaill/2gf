using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebook._2Gf
{
    class ZszqTxt2GfCsv : Converter
    {
        public event ErrorRecordEventHandler ErrorRecordEvent;

        public void Convert(string txtFile, string csvFile)
        {
            // 币种, 证券名称,成交日期,成交价格,成交数量,发生金额,资金余额,合同编号,业务名称,手续费,印花税,过户费,结算费,证券代码,股东代码
            ZszqTxt txt = new ZszqTxt();
            txt.FromTxt(txtFile);

            GfCsv csv = new GfCsv();
            for (int i = 0; i < txt.Records.Count; i++)
            {
                ZszqRecord rec = (ZszqRecord)txt.Records[i];
                string ZQDM = rec.GDDM[0] == 'A' ? rec.ZQDM : "SHE:" + rec.ZQDM;

                switch (rec.YWMC)
                {
                    case ZszqOption.ZQMR:
                        csv.Add(ZQDM, rec.ZQMC, GfType.Buy, rec.CJRQ, rec.CJSL, rec.CJJG, rec.SXF + rec.YHS + rec.GHF + rec.JSF, "");
                        break;
                    case ZszqOption.ZQMC:
                        csv.Add(ZQDM, rec.ZQMC, GfType.Sell, rec.CJRQ, rec.CJSL, rec.CJJG, rec.SXF + rec.YHS + rec.GHF + rec.JSF, "");
                        break;
                    case ZszqOption.YHZC:
                    case ZszqOption.GXRZ:
                    case ZszqOption.PLLXGB:
                        //csv.Add(GfType.DepositCash, rec.CJRQ, rec.FSJE, "");
                        //break;
                    case ZszqOption.HGRZ:
                    default:
                        if (ErrorRecordEvent != null)
                            ErrorRecordEvent(this, rec.Source);
                        break;

                }
            }
            csv.ToCsv(csvFile);
        }
    }
}
