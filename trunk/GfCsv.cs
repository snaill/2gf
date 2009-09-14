using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebook._2Gf
{
    class GfRecord
    {
        // 股票代码
        public string Symbol;
        // 名称
        public string Name;
        // 类型
        public string Type;
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

        public void Add(string Symbol, string Name, string Type, string Date, string Shares, string Price, string Commission, string Notes)
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

        public void Add(string Type, string Date, string Value, string Notes)
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
                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}", 
                    rec.Symbol, rec.Name, rec.Type, rec.Date, rec.Shares, rec.Price, rec.CashValue, rec.Commission, rec.Notes);
            }
            sw.Close();
        }

    }
}
