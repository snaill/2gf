﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebook._2Gf
{
    enum ZszqOption
    {
        // Unknown
        Unknown,
        // 证券买入(xxx)
        ZQMR,
        // 证券卖出(xxx)
        ZQMC,
        // 红股入帐(xxx)
        HGRZ,
        // 股息入帐(xxx)
        GXRZ,
        // 银行转存
        YHZC,
        // 批量利息归本
        PLLXGB
    }

    class ZszqRecord
    {
        // 币种 
        public string BZ;
        // 证券名称
        public string ZQMC;
        // 成交日期
        public DateTime CJRQ;
        // 成交价格
        public double CJJG;
        // 成交数量 
        public double CJSL;
        // 发生金额
        public double FSJE;
        // 资金余额
        public double ZJYE;
        // 合同编号
        public string HTBH;
        // 业务名称
        public ZszqOption YWMC;
        // 手续费
        public double SXF;
        // 印花税
        public double YHS;
        // 过户费
        public double GHF;
        // 结算费
        public double JSF;
        // 证券代码
        public string ZQDM;
        // 股东代码
        public string GDDM;
        // 源数据行
        public string Source;

        public static ZszqOption ParseOption( string str )
        {
            if ( str.IndexOf('(') >= 0 )
            {
                string strOp = str.Substring(0, 4);
                if ( strOp == "证券买入" )
                    return ZszqOption.ZQMR;
                else if ( strOp == "证券卖出" )
                    return ZszqOption.ZQMC;
                else if ( strOp == "红股入帐" )
                    return ZszqOption.HGRZ;
                else if ( strOp == "股息入帐" )
                    return ZszqOption.GXRZ;
            }
            else
            {
                if ( str == "银行转存" )
                    return ZszqOption.YHZC;
                else if ( str == "批量利息归本" )
                    return ZszqOption.PLLXGB;
            }

            return ZszqOption.Unknown;
        }

        public static ZszqRecord Parse(string str )
        {
            //
            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            string sTemp = "";
            int nSpace = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ')
                    sTemp += str[i];
                else
                {
                    nSpace++;
                    if (sTemp.Length > 0)
                    {
                        if (sTemp == "---" && arr.Count < 14)
                            sTemp = "0.00";
                        arr.Add(sTemp);
                        sTemp = "";
                        nSpace = 0;
                    }
                    if (nSpace >= 18)
                    {
                        arr.Add("");
                        nSpace = 0;
                    }
                }
            }

            // 
            ZszqRecord rec = new ZszqRecord();
            // 币种 
            rec.BZ = arr[0].ToString();
            // 证券名称
            rec.ZQMC = arr[1].ToString(); 
            // 成交日期
            int year = int.Parse( arr[2].ToString().Substring( 0, 4 ) );
            int month = int.Parse( arr[2].ToString().Substring( 4, 2 ) );
            int day = int.Parse( arr[2].ToString().Substring( 6, 2 ) );
            rec.CJRQ = new DateTime( year, month, day );
            // 成交价格
            rec.CJJG = double.Parse(arr[3].ToString());
            // 成交数量 
            rec.CJSL = double.Parse(arr[4].ToString());
            // 发生金额
            rec.FSJE = double.Parse(arr[5].ToString());
            // 资金余额
            rec.ZJYE = double.Parse(arr[6].ToString());
            // 合同编号
            rec.HTBH = arr[7].ToString(); 
            // 业务名称
            rec.YWMC = ParseOption( arr[8].ToString() ); 
            // 手续费
            rec.SXF = double.Parse(arr[9].ToString());
            // 印花税
            rec.YHS = double.Parse(arr[10].ToString());
            // 过户费
            rec.GHF = double.Parse(arr[11].ToString());
            // 结算费
            rec.JSF = double.Parse(arr[12].ToString());
            // 证券代码
            rec.ZQDM = arr[13].ToString(); 
            // 股东代码
            rec.GDDM = arr[14].ToString();
            // 源数据行
            rec.Source = str;

            return rec;
        }
    }

    class ZszqTxt
    {
        public System.Collections.ArrayList Records = new System.Collections.ArrayList();

        public void FromTxt(string fn)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(fn, System.Text.Encoding.Default);
            sr.ReadLine();  // ------------
            sr.ReadLine();  // Space
            // 币种, 证券名称,成交日期,成交价格,成交数量,发生金额,资金余额,合同编号,业务名称,手续费,印花税,过户费,结算费,证券代码,股东代码
            sr.ReadLine();

            //
            string strLine = sr.ReadLine();
            while (strLine != null && strLine != "")
            {
                ZszqRecord rec = ZszqRecord.Parse(strLine);
                Records.Add(rec);
                strLine = sr.ReadLine();
            }
        }
    }
}
