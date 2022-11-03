using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityAccounting
{
    internal class ElectricityBill
    {
        const double priceKWh = 0.18; //it is price per kWh in Poland, сurrency - $USA
        int appNo;
        string? surname;
        int[] meterReadings;

        public ElectricityBill()
        {
            appNo = 0;
            surname = " ";
            meterReadings = new int[] { 0, 0, 0 };
        }

        public ElectricityBill(int appNo, string surname, int month1Readings, int month2Readings, int month3Readings)
        {
            AppNo = appNo;
            Surname = surname;
            meterReadings = SetMeterReadings(month1Readings, month2Readings, month3Readings);
        }
        public ElectricityBill(int appNo, string surname, int[] meterReadings)
        {
            AppNo = appNo;
            Surname = surname;
            this.meterReadings = meterReadings;
        }
        public int AppNo
        {
            get { return appNo; }
            set
            {
                if (value < 0)
                {
                    appNo = 0;
                }
                else
                {
                    appNo = value;
                }
            }
        }
        public string Surname
        {
            get
            {
                if (surname != null)
                {
                    return surname;
                }
                else
                {
                    return " ";
                }

            }
            set
            {
                if (value.Length <= 1)
                {
                    surname = "Incorrect surname";
                }
                else
                {
                    surname = value;
                }
            }

        }

        private int[] SetMeterReadings(int month1Reading, int month2Reading, int month3Reading)
        {
            int[] meterReadings = new int[3];
            meterReadings[0] = month1Reading;
            meterReadings[1] = month2Reading;
            meterReadings[2] = month3Reading;
            return meterReadings;
        }
        public override string ToString()
        {
            return " | " + appNo.ToString() + " | " + surname + " | " + PrintMeterReadings()
                + CountPayAmount().ToString() + " | \n";
        }

        public string PrintMeterReadings()
        {
            string meterReadStr = "";
            for (int i = 0; i < meterReadings.Length; i++)
            {
                meterReadStr += meterReadings[i].ToString() + " | ";
            }
            return meterReadStr;
        }
        public double CountPayAmount()
        {
            return (double)((meterReadings[2] - meterReadings[0]) * priceKWh);
        }

        public ElectricityBill Parse(string line)
        {
            string[] bills = line.Split(' ');

            AppNo = int.Parse(bills[0]);
            Surname = bills[1];
            meterReadings = SetMeterReadings(int.Parse(bills[2]), int.Parse(bills[3]), int.Parse(bills[4]));
            return new ElectricityBill(AppNo, Surname, meterReadings);
        }

        public string ReadFromFile(string path)
        {
            string line = "";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }
            else
            {
                StreamReader reader = new StreamReader(path);
                line += reader.ReadLine();
                reader.Close();
            }
            return line;
        }

    }
}

