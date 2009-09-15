using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebook._2Gf
{
    public delegate void ErrorRecordEventHandler(Object sender, string data );  

    public interface Converter
    {
        event ErrorRecordEventHandler ErrorRecordEvent;

        void Convert(string txtFile, string csvFile);
    }
}
