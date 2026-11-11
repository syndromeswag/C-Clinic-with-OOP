using System;
using System.Collections.Generic;

class Doctor
{
    private string id;

    public string ID
    {
        get
        {
            return id;
        }
        set
        {
            if (value == null || value == "")
            {
                throw new ArgumentException("Айди доктора не может быть пустым");
            }
            else
            {
                id = value;
            }
        }
    }

    private string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            if (value == null || value == "")
            {
                throw new ArgumentException("Имя доктора не может быть пустым");
            }
            else
            {
                name = value;
            }
        }
    }

    private string specialty;
    public string Specialty
    {
        get
        {
            return specialty;
        }
        set
        {
            if (value == null || value == "")
            {
                throw new ArgumentException("Специальность доктора не может быть пустой");
            }
            else
            {
                specialty = value;
            }
        }
    }
    public Doctor(string id, string name, string specialty)
    {
        ID = id;
        Name = name;
        Specialty = specialty;
    }
}

class Patient
{
    private string id;
    public string ID
    {
        get
        {
            return id;
        }
        set
        {
            if (value == null || value == "")
            {
                throw new ArgumentException("Айди пациента не может быть пустым");
            }
            else
            {
                id = value;
            }
        }
    }
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            if (value == null || value == "")
            {
                throw new ArgumentException("Имя пациента не может быть пустым");
            }
            else
            {
                name = value;
            }
        }
    }
    private int age;
    public int Age
    {
        get
        {
            return age;
        }
        set
        {
            if (value < 0 || value > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(Age), "Возраст пациента в рамках от 0 до 150");
            }
            else
            {
                age = value;
            }
        }
    }

    public Patient(string id, string name, int age)
    {
        ID = id;
        Name = name;
        Age = age;
    }
}

class Medicine
{
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            if (value == null || value == "")
            {
                throw new ArgumentException("Название не может быть пустым");
            }
            else
            {
                name = value;
            }
        }
    }
    private int price;
    public int Price
    {
        get
        {
            return price;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Price), "Цена не может быть меньше нуля");
            }
            else
            {
                price = value;
            }
        }
    }
    private int stock;
    public int Stock
    {
        get
        {
            return stock;
        }
        private set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Stock), "Количество не может быть нулем или меньше нуля");
            }
            else
            {
                stock = value;
            }
        }
    }

    public Medicine(string name, int price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Количество должно быть больше 0");
        }
        else if (quantity > stock)
        {
            throw new InvalidOperationException("Недостаточное количестов на складе");
        }
        else
        {
            stock -= quantity;
        }
    }
}

class Prescription
{
    public Medicine Medicine { get; set; }

    private int quantity;

    public int Quantity
    {
        get
        {
            return quantity;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Количество выписанного лекарства должно быть больше 0");
            }
            else
            {
                quantity = value;
            }
        }
    }

    public Prescription(Medicine medicine, int quantity)
    {
        Medicine = medicine;
        Quantity = quantity;
    }

    public int Total()
    {
        if (Medicine == null)
        {
            return 0;
        }
        return Medicine.Price * Quantity;
    }
}

class Visit
{
    public int Number { get; set; }
    public string DoctorId { get; set; }

    public string PatientId { get; set; }

    private string diagnosis;

    public string Diagnosis
    {
        get
        {
            return diagnosis;
        }
        set
        {
            if (value == "" || value == null)
            {
                throw new ArgumentException("Диагноз не может быть пустым");
            }
            else
            {
                diagnosis = value;
            }
        }
    }

    public List<Prescription> Prescriptions;

    public Visit(int number, string doctorId, string patientId, string diagnosis)
    {
        Number = number;
        DoctorId = doctorId;
        PatientId = patientId;
        Diagnosis = diagnosis;
        Prescriptions = new List<Prescription>();
    }

    public int TotalCost()
    {
        if (Prescriptions.Count == 0)
        {
            return 0;
        }
        int total = 0;

        foreach (Prescription p in Prescriptions)
        {
            total += p.Total();
        }
        return total;
    }

    public void AddPrescription(Prescription p)
    {
        Prescriptions.Add(p);
    }
}

class Clinic
{
    public string Name { get; set; }

    public Dictionary<string, Doctor> Doctors = new Dictionary<string, Doctor>();
    public Dictionary<string, Patient> Patients = new Dictionary<string, Patient>();

    public Dictionary<string, Medicine> Medicines = new Dictionary<string, Medicine>();

    public List<Visit> Visits = new List<Visit>();

    int NextVisitNumber = 1;

    public Clinic(string name)
    {
        Name = name;
    }

    public void AddDoctor(Doctor doctor)
    {
        foreach (var pair in Doctors)
        {
            if (doctor.ID == pair.Key)
            {
                throw new InvalidOperationException("Врач с таким айди уже существует");
            }
        }
        Doctors.Add(doctor.ID, doctor);
    }

    public void AddPatient(Patient patient)
    {
        foreach (var pair in Patients)
        {
            if (patient.ID == pair.Key)
            {
                throw new InvalidOperationException("Пациент с таким айди уже существует");
            }
        }
        Patients.Add(patient.ID, patient);
    }
    public void AddMedicine(Medicine medicine)
    {
        foreach (var pair in Medicines)
        {
            if (pair.Key == medicine.Name)
            {
                throw new InvalidOperationException("Лекарство с таким названием уже существует");
            }
        }
        Medicines.Add(medicine.Name, medicine);
    }
    public Doctor FindDoctor(string id)
    {
        foreach (var pair in Doctors)
        {
            if (id == pair.Key)
            {
                return pair.Value;
            }
        }
        return null;
    }

    public Patient FindPatient(string id)
    {
        foreach (var pair in Patients)
        {
            if (id == pair.Key)
            {
                return pair.Value;
            }
        }
        return null;
    }

    public Medicine FindMedicine(string name)
    {
        foreach (var pair in Medicines)
        {
            if (name == pair.Key)
            {
                return pair.Value;
            }
        }
        return null;
    }
    public Visit FindVisit(int number)
    {
        foreach (var v in Visits)
        {
            if (number == v.Number)
            {
                return v;
            }
        }
        return null;
    }

    public int RegisterVisit(string doctorId, string patientId, string diagnosis)
    {
        Doctor foundeddoctor = FindDoctor(doctorId);
        Patient foundedpatient = FindPatient(patientId);

        if (foundeddoctor == null)
        {
            throw new InvalidOperationException("Доктор не найден");
        }
        else if (foundedpatient == null)
        {
            throw new InvalidOperationException("Пациент не найден");
        }
        else
        {
            Visit visit = new Visit(NextVisitNumber, doctorId, patientId, diagnosis);
            NextVisitNumber++;
            Visits.Add(visit);
            return visit.Number;
        }
    }

    public void Prescribe(int visitnumber, string medicineName, int quantity)
    {
        Visit foundedvisit = FindVisit(visitnumber);
        Medicine foundedmedicine = FindMedicine(medicineName);
        if (foundedvisit == null)
        {
            throw new InvalidOperationException("Визит не найден");
        }
        else if (foundedmedicine == null)
        {
            throw new InvalidOperationException("Лекарство не найдено");
        }
        else
        {
            foundedmedicine.ReduceStock(quantity);
            Prescription prescription = new Prescription(foundedmedicine, quantity);
            foundedvisit.AddPrescription(prescription);
        }
    }

}

class Program
{
    static void Main()
    {
        Clinic clinic = new Clinic("Больница");
        while (true)
        {
            try
            {
                Console.WriteLine("1. Добавить врача \n2. Добавить пациента \n3. Добавить лекарство \n4. Зарегистрировать приём \n5. Назначить лекарство \n6. Отчёты \n7. Поиск \n0. Выход");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    try
                    {
                        Console.WriteLine("Введите ID доктора");
                        string id = Console.ReadLine();
                        Console.WriteLine("Введите имя доктора");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите специальность доктора");
                        string specialty = Console.ReadLine();
                        clinic.AddDoctor(new Doctor(id, name, specialty));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (action == "2")
                {
                    try
                    {
                        Console.WriteLine("Введите ID пациента");
                        string id = Console.ReadLine();
                        Console.WriteLine("Введите имя пациента");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите возраст пациента");
                        int age = Convert.ToInt32(Console.ReadLine());
                        clinic.AddPatient(new Patient(id, name, age));
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Возраст слишком велик");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Возраст должен быть числом");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (action == "3")
                {
                    try
                    {
                        Console.WriteLine("Введите название лекарства");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите цену лекарства");
                        int price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите остаток лекарства на складе");
                        int stock = Convert.ToInt32(Console.ReadLine());
                        clinic.AddMedicine(new Medicine(name, price, stock));
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Сумма слишком велика");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Сумма должна быть числом");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (action == "4")
                {
                    try
                    {
                        Console.WriteLine("Введите ID доктора");
                        string doctorid = Console.ReadLine();
                        Console.WriteLine("Введите ID пациента");
                        string patientid = Console.ReadLine();
                        Console.WriteLine("Введите диагноз");
                        string diagnosis = Console.ReadLine();
                        Console.WriteLine($"Приём {clinic.RegisterVisit(doctorid, patientid, diagnosis)} создан");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (action == "5")
                {
                    try
                    {
                        Console.WriteLine("Введите номер визита");
                        int visitnumber = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите название лекарства");
                        string medicineName = Console.ReadLine();
                        Console.WriteLine("Введите количество лекарства");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        clinic.Prescribe(visitnumber, medicineName, quantity);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Сумма слишком велика");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Номер визита должен быть числом");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (action == "6")
                {
                    Console.WriteLine("Все врачи:");

                    foreach (var pair in clinic.Doctors)
                    {
                        Console.WriteLine("-----");
                        Console.WriteLine($"ID: {pair.Key}");
                        Console.WriteLine($"ФИО: {pair.Value.Name}");
                        Console.WriteLine($"Специальность: {pair.Value.Specialty}");
                        Console.WriteLine("-----");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Все пациенты:");

                    foreach (var pair in clinic.Patients)
                    {
                        Console.WriteLine("-----");
                        Console.WriteLine($"ID: {pair.Key}");
                        Console.WriteLine($"ФИО: {pair.Value.Name}");
                        Console.WriteLine($"Возраст: {pair.Value.Age}");
                        Console.WriteLine("-----");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Все лекарства:");

                    foreach (var pair in clinic.Medicines)
                    {
                        Console.WriteLine($"{pair.Key}: Цена: {pair.Value.Price} Остаток: {pair.Value.Stock}");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Уникальные специальности");

                    HashSet<string> UniqueSpeciality = new HashSet<string>();

                    foreach (var pair in clinic.Doctors)
                    {
                        UniqueSpeciality.Add(pair.Value.Specialty);
                    }

                    foreach (var s in UniqueSpeciality)
                    {
                        Console.WriteLine(s);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Топ 3 диагноза по частоте");

                    Dictionary<string, int> DiagnosisCount = new Dictionary<string, int>();

                    foreach (Visit v in clinic.Visits)
                    {
                        if (DiagnosisCount.TryGetValue(v.Diagnosis, out int value))
                        {
                            DiagnosisCount[v.Diagnosis]++;
                        }
                        else
                        {
                            DiagnosisCount[v.Diagnosis] = 1;
                        }
                    }
                    string winner = "";
                    int winnercount = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (DiagnosisCount.Count == 0)
                        {
                            Console.WriteLine("Словарь для топ 3 пуст");
                            break;
                        }
                        foreach (var pair in DiagnosisCount)
                        {
                            if (winnercount < pair.Value)
                            {
                                winner = pair.Key;
                                winnercount = pair.Value;
                            }
                        }
                        Console.WriteLine("Диагноз: " + winner + " Количество: " + winnercount);
                        DiagnosisCount.Remove(winner);
                        winner = "";
                        winnercount = 0;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Самое назначаемое лекарство по количеству");

                    Dictionary<string, int> MedicinesCount = new Dictionary<string, int>();

                    foreach (var v in clinic.Visits)
                    {
                        foreach (Prescription prescription in v.Prescriptions)
                        {
                            if (MedicinesCount.TryGetValue(prescription.Medicine.Name, out int value))
                            {
                                MedicinesCount[prescription.Medicine.Name] += prescription.Quantity;
                            }
                            else
                            {
                                MedicinesCount[prescription.Medicine.Name] = prescription.Quantity;
                            }
                        }
                    }
                    string winnermedicine = "";
                    int winnermedicinecount = 0;
                    foreach (var pair in MedicinesCount)
                    {
                        if (winnermedicinecount < pair.Value)
                        {
                            winnermedicine = pair.Key;
                            winnermedicinecount = pair.Value;
                        }
                    }
                    Console.WriteLine(winnermedicine);
                    Console.WriteLine();
                    Console.WriteLine("Лекарства на исходе:");

                    Console.WriteLine("Введите порог");
                    int porog = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    foreach (var pair in clinic.Medicines)
                    {
                        if (pair.Value.Stock < porog)
                        {
                            Console.WriteLine(pair.Key);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Лекарства, которые ни разу не назначались");

                    HashSet<string> PrescribedNames = new HashSet<string>();

                    foreach (var pair in MedicinesCount)
                    {
                        PrescribedNames.Add(pair.Key);
                    }

                    foreach (var pair in clinic.Medicines)
                    {
                        if (!PrescribedNames.Contains(pair.Value.Name))
                        {
                            Console.WriteLine(pair.Value.Name);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Пациенты с 2+ приёмами");

                    Dictionary<string, int> PatientsCount = new Dictionary<string, int>();

                    foreach (Visit v in clinic.Visits)
                    {
                        if (PatientsCount.TryGetValue(v.PatientId, out int value))
                        {
                            PatientsCount[v.PatientId]++;
                        }
                        else
                        {
                            PatientsCount[v.PatientId] = 1;
                        }
                    }

                    foreach (var pair in PatientsCount)
                    {
                        if (pair.Value > 1)
                        {
                            Patient foundedpatient = clinic.FindPatient(pair.Key);
                            Console.WriteLine(foundedpatient.Name);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Общая стоимость всех назначений");

                    int totalcost = 0;
                    if (clinic.Visits.Count == 0)
                    {
                        throw new DivideByZeroException("Список визитов пуст");

                    }

                    foreach (Visit v in clinic.Visits)
                    {
                        totalcost += v.TotalCost();
                    }
                    Console.WriteLine(totalcost);




                }
                if (action == "0")
                {
                    break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный формат ввода");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Слишком большое число");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("-----CLINIC-----");
            }
        }
    }
}
