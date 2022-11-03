
using ElectricityAccounting;

ElectricityReport elReport1 = new ElectricityReport("ElectrycityAccounting1.txt");
elReport1.WriteToFile("ElectrycityReport1.txt");

Console.WriteLine(elReport1.PrintOneBill("Bonn"));
Console.WriteLine(elReport1.PrintOneBill(1));
Console.WriteLine(elReport1.PrintOneBill("Non-existed surname"));

Console.WriteLine(elReport1.LargestDebt() + " has a largest debt.");
Console.WriteLine();

Console.WriteLine("Apartment in which no electricity was used - " + elReport1.ZeroDebt());
Console.WriteLine();

Console.WriteLine("Days since last meter reading: " + elReport1.DaysSinceLastMeterReading());

ElectricityReport elReport2 = new ElectricityReport("ElectrycityAccounting2.txt");
elReport2.WriteToFile("ElectrycityReport2.txt");
Console.WriteLine(elReport1);
Console.WriteLine(elReport2);

ElectricityReport elReport4 = elReport2 - elReport1; //but doesn`t work correctly together
Console.WriteLine(elReport4);

ElectricityReport elReport3 = elReport1 + elReport2;
Console.WriteLine(elReport3);
