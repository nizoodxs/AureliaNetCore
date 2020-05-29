using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicationProcess.May2020.Data
{
    public class AppDbConfig
    {
        private const string dBName = "HahnApplicationDB";
        
        public string DBName 
        {
            get
            {
                return dBName;
            }
        }
    }
}
