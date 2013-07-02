using System;

namespace xZAPP.Core
{
    public sealed class ApplicationData
    {
        private static volatile ApplicationData instance;
        private static object sync = new Object();

        // ApplicationData properties
        public string Token { get; set;}
        public long NumberOfClients { get; set;}

        public ApplicationData()
        {
        }

        public static ApplicationData GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null) 
                            instance = new ApplicationData();
                    }
                }

                return instance;
            }
        }
       
    }
}