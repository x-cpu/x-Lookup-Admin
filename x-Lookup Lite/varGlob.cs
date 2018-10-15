using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x_Lookup_Lite
{
    class varGlob
    {
        public static string aValue { get; set; }
        public static string operID { get; set; }
        public static string machineName { get; set; }
        public static string IPaddress { get; set; }
        public static int counter { get; set; }

        public static int couplerMTV { get; set; }
        public static int couplerMTVRMC { get; set; }
        public static int ncc { get; set; }
        public static int doma { get; set; }
        public static int sms { get; set; }
        public static int docidFP { get; set; }
        public static int docidMTV { get; set; }
        public static int mtv { get; set; }
        public static int docidTotal { get; set; }
        public static int mtvMonthly { get; set; }


        public static double totalspace { get; set; }
        public static double freespace { get; set; }
        public static double freepercent { get; set; }

        public static int CMPtoBEZIP { get; set; }
        public static int CMPwaitingForPP { get; set; }
        public static int CMPpendingSMS_ACK { get; set; }
        public static int CMPpendingSMS_ZIP { get; set; }
        public static int CMPpendingDOMA_ZIP { get; set; }

        public static int mtvRMC { get; set; }
        public static int mtvMonthlyRMC { get; set; }

        public static int docidLON { get; set; }
        public static int docidFPRMC { get; set; }
        public static int docidMTVRMC { get; set; }
        public static int docidLONRMC { get; set; }
        public static int docidTotalRMC { get; set; }
    }
}
