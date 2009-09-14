using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebook._2Gf
{
    enum GfType
    {
        // 买入
        Buy,
        // 买出
        Sell,
        // 存入现金
        DepositCash,
        // 取出现金
        WithdrawCash
    }

    class GfRecord
    {
        // 股票代码
        public string Symbol;
        // 名称
        public string Name;
        // 类型
        public GfType Type;
        // 日期
        public string Date;
        // 股数
        public string Shares;
        // 价格
        public string Price;
        // 现金价值
        public string CashValue;
        // 佣金
        public string Commission;
        // 备注
        public string Notes;
    }

    class GfCsv
    {
        System.Collections.ArrayList arRecords = new System.Collections.ArrayList();

        public void Add(string Symbol, string Name, GfType Type, string Date, string Shares, string Price, string Commission, string Notes)
        {
            GfRecord rec = new GfRecord();
            rec.Symbol = Symbol;
            rec.Name = Name;
            rec.Type = Type;
            rec.Date = Date;
            rec.Shares = Shares;
            rec.Price = Price;
            rec.Commission = Commission;
            rec.Notes = Notes;
            Add(rec);

        }

        public void Add(GfType Type, string Date, string Value, string Notes)
        {
            GfRecord rec = new GfRecord();
            rec.Name = "Cash";
            rec.Type = Type;
            rec.Date = Date;
            rec.CashValue = Value;
            rec.Notes = Notes;
            Add(rec);
        }

        public void Add(GfRecord rec)
        {
            arRecords.Add(rec);
        }

        public void ToCsv(string fn)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fn);
            sw.WriteLine("Symbol,Name,Type,Date,Shares,Price,Cash value,Commission,Notes");
            for (int i = 0; i < arRecords.Count; i++)
            {
                GfRecord    rec = (GfRecord)arRecords[i];
                string strType = "Unknown";
                switch (rec.Type)
                {
                    case GfType.Buy: strType = "Buy"; break;
                    case GfType.Sell: strType = "Sell"; break;
                    case GfType.DepositCash: strType = "Deposit Cash"; break;
                    case GfType.WithdrawCash: strType = "Withdraw Cash"; break;
                }
                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                    rec.Symbol, rec.Name, strType, rec.Date, rec.Shares, rec.Price, rec.CashValue, rec.Commission, rec.Notes);
            }
            sw.Close();
        }

    }
}
