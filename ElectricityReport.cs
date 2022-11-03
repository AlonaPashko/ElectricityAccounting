using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityAccounting
{
    internal class ElectricityReport
    {
        private int appartments;
        private int quarterNo;
        private DateTime[] dateTimes;
        private List<ElectricityBill> bills;

        public ElectricityReport()
        {
            Appartments = 1;
            QuarterNo = 1;
            dateTimes = new DateTime[] { new DateTime(2022, 1, 1), new DateTime(2022, 2, 1), new DateTime(2022, 3, 1) };
            bills = new List<ElectricityBill> { new ElectricityBill() };
        }
        public ElectricityReport(int appartments, int quaterNo)
        {
            Appartments = appartments;
            QuarterNo = quaterNo;
            dateTimes = SetDateTimes();
            bills = new List<ElectricityBill> { new ElectricityBill(1, "Pashko", 3250, 3340, 3520),
                new ElectricityBill(2, "Cherniak", 2390, 2489, 2532) };
        }
        public ElectricityReport(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }
            else
            {
                StreamReader reader = new StreamReader(path);
                string line1 = "" + reader.ReadLine();
                string line2 = "" + reader.ReadToEnd();
                reader.Close();

                string[] array1 = line1.Split(' ');
                string[] array2 = line2.Split('\n');

                Appartments = int.Parse(array1[0]);
                QuarterNo = int.Parse(array1[1]);
                dateTimes = SetDateTimes();

                bills = SetBills(array2);
            }
        }

        public int Appartments
        {
            get { return appartments; }
            set
            {
                if (value <= 0)
                {
                    appartments = 1;
                }
                else
                {
                    appartments = value;
                }
            }
        }
        public int QuarterNo
        {
            get { return quarterNo; }
            set
            {
                if (value <= 0 || value > 4)
                {
                    quarterNo = 1;
                }
                else
                {
                    quarterNo = value;
                }
            }
        }

        private DateTime[] SetDateTimes()
        {
            switch (quarterNo)
            {
                case 1:
                    dateTimes = new DateTime[] { new DateTime(2022, 1, 1), new DateTime(2022, 2, 1), new DateTime(2022, 3, 1) };
                    break;
                case 2:
                    dateTimes = new DateTime[] { new DateTime(2022, 4, 1), new DateTime(2022, 5, 1), new DateTime(2022, 6, 1) };
                    break;
                case 3:
                    dateTimes = new DateTime[] { new DateTime(2022, 7, 1), new DateTime(2022, 8, 1), new DateTime(2022, 9, 1) };
                    break;
                case 4:
                    dateTimes = new DateTime[] { new DateTime(2022, 10, 1), new DateTime(2022, 11, 1), new DateTime(2022, 12, 1) };
                    break;
            }
            return dateTimes;
        }

        public void ChangeCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
        }

        private List<ElectricityBill> SetBills(string[] array)
        {
            List<ElectricityBill> bills = new List<ElectricityBill>();
            for (int i = 0; i < array.Length; i++)
            {
                ElectricityBill bill = new ElectricityBill();
                bill.Parse(array[i]);
                bills.Add(bill);
            }
            return bills;
        }

        public string PrintHeader()
        {
            return "----------------------------------------------------------\n" +
                "| No | Owners: " + appartments + " | QuarterNo: " + quarterNo + " | To pay |\n" +
                "---------------------------------------------------------- \n" +
                "\t" + PrintDates() + "\t\t\n" +
                "---------------------------------------------------------- \n";
        }
        private string PrintBills()
        {
            string line = "";
            for (int i = 0; i < bills.Count; i++)
            {
                line += bills[i] + "\n";
            }
            return line;
        }
        private string PrintDates()
        {
            string line = "";
            for (int i = 0; i < dateTimes.Length; i++)
            {
                ChangeCulture();
                line += dateTimes[i].ToString("MMMM, dd") + " | ";
            }
            return line;
        }

        public override string ToString()
        {
            return PrintHeader() + '\n' + PrintBills();
        }

        private string[] MakeArrayToWrite()
        {
            string header = PrintHeader();
            string bills = PrintBills();
            string result = header + bills;

            string[] strArray = result.Split('\n');
            return strArray;
        }

        public void WriteToFile(string path)
        {
            StreamWriter writer = new StreamWriter(path);
            string[] array = MakeArrayToWrite();
            for (int i = 0; i < array.Length; i++)
            {
                writer.WriteLine(array[i]);
            }
            writer.Close();
        }
        public ElectricityBill PrintOneBill(string surname)
        {
            foreach (ElectricityBill bill in bills)
            {
                if (bill.Surname == surname)
                {
                    return bill;
                }
            }
            return new ElectricityBill();
        }
        public ElectricityBill PrintOneBill(int appNo)
        {
            foreach (ElectricityBill bill in bills)
            {
                if (bill.AppNo == appNo)
                {
                    return bill;
                }
            }
            return new ElectricityBill();
        }
        public string LargestDebt()
        {
            double result = 0;
            string debtor = "";

            foreach (ElectricityBill bill in bills)
            {
                if (bill.CountPayAmount() > result)
                {
                    result = bill.CountPayAmount();
                    debtor = bill.Surname;
                }
            }
            return debtor;
        }
        public string ZeroDebt()
        {
            double result = 0;

            foreach (ElectricityBill bill in bills)
            {
                if (bill.CountPayAmount() == result)
                {
                    result = bill.CountPayAmount();
                    return bill.AppNo.ToString();
                }
            }
            return "none";
        }
        public int DaysSinceLastMeterReading()
        {
            DateTime currentDate = DateTime.Now;
            DateTime lastDayMeterRead = dateTimes[2];

            TimeSpan result = currentDate.Subtract(lastDayMeterRead);
            int days = (int)result.TotalDays;
            return days;
        }

        public static ElectricityReport operator +(ElectricityReport report1, ElectricityReport report2)
        {
            ElectricityReport resReport = new ElectricityReport();
            resReport.bills = report1.bills;
            resReport.bills.AddRange(report2.bills);

            return resReport;
        }
        public static ElectricityReport operator -(ElectricityReport report1, ElectricityReport report2)
        {
            ElectricityReport resReport = report1;

            for (int i = 0; i < report2.bills.Count; i++)
            {
                for (int j = 0; j < resReport.bills.Count; j++)
                {
                    if (IsEqualBills(resReport.bills[j], report2.bills[i]))
                    {
                        resReport.bills.Remove(resReport.bills[j]);
                    }
                }
            }

            return resReport;
        }

        public static bool IsEqualBills(ElectricityBill bill1, ElectricityBill bill2)
        {
            if (bill1.AppNo == bill2.AppNo && bill1.Surname == bill2.Surname)
            {
                return true;
            }
            return false;
        }


    }
}
