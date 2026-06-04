using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
static void Main()
    {
    var hospital = new Hospital();
    hospital.Specializations.AddRange(new[] {
    new Specialization { Id = 1, Name = "Хирург" },
    new Specialization { Id = 2, Name = "Терапевт" },
    new Specialization { Id = 3, Name = "Окулист" }
    });
hospital.Diagnoses.AddRange(new[] {
new Diagnosis { Id = 1, Name = "Грипп" },
new Diagnosis { Id = 2, Name = "Перелом" },
new Diagnosis { Id = 3, Name = "Близорукость" }
});

hospital.Doctors.AddRange(new[] {
new Doctor { Id = 1, FullName = "Костоломов В. В.", SpecializationId = 2, ExperienceYears = 20 },
new Doctor { Id = 2, FullName = "Выгноухов Н. Н.", SpecializationId = 1, ExperienceYears = 15 },
new Doctor { Id = 3, FullName = "Зубоскалов К. К.", SpecializationId = 2, ExperienceYears = 8 }
});

hospital.Patients.AddRange(new[] {
new Patient { Id = 10, FullName = "Иванов И. И.", Address = "Москва", BirthYear = 1990 },
new Patient { Id = 11, FullName = "Петров П. П.", Address = "Вологда", BirthYear = 1985 },
new Patient { Id = 12, FullName = "Сидорова А. А.", Address = "Омск", BirthYear = 1995 }
});

hospital.Visits.AddRange(new[] {
new Visit { PatientId = 10, DoctorId = 1, VisitDate = new DateTime(2026, 4, 7), DiagnosisId = 1, HasSickLeave = true },
new Visit { PatientId = 11, DoctorId = 1, VisitDate = new DateTime(2026, 5, 6), DiagnosisId = 1, HasSickLeave = false },
new Visit { PatientId = 12, DoctorId = 2, VisitDate = new DateTime(2026, 4, 5), DiagnosisId = 2, HasSickLeave = true },
new Visit { PatientId = 10, DoctorId = 3, VisitDate = new DateTime(2026, 5, 13), DiagnosisId = 1, HasSickLeave = false }
});

Console.WriteLine("Пациенты по врачам");
hospital.PrintPatientsByDoctor();

Console.WriteLine("История посещений пациента");
hospital.PrintVisitsByPatient();

Console.WriteLine("Группировка по диагнозу");
hospital.PrintPatientsByDiagnosis();

Console.WriteLine("Больничные листы");
hospital.PrintPatientsWithSickLeave();

Console.WriteLine("Врачи по специализациям");
hospital.PrintDoctorsBySpecialization();
}
}
public class Patient
{
public int Id { get; set; }
public string FullName { get; set; }
public string Address { get; set; }
public int BirthYear { get; set; }
}

public class Diagnosis
{
public int Id { get; set; }
public string Name { get; set; }
}

public class Doctor
{
public int Id { get; set; }
public string FullName { get; set; }
public int SpecializationId { get; set; }
public int ExperienceYears { get; set; }
}

public class Specialization
{
public int Id { get; set; }
public string Name { get; set; }
}

public class Visit
{
public int PatientId { get; set; }
public int DoctorId { get; set; }
public DateTime VisitDate { get; set; }
public int DiagnosisId { get; set; }
public bool HasSickLeave { get; set; }
}

public class Hospital
{
public List<Specialization> Specializations = new List<Specialization>();
public List<Patient> Patients = new List<Patient>();
public List<Diagnosis> Diagnoses = new List<Diagnosis>();
public List<Doctor> Doctors = new List<Doctor>();
public List<Visit> Visits = new List<Visit>();

public void PrintPatientsByDoctor()
{
    var query = Doctors
    .Join(Visits, d => d.Id, v => v.DoctorId, (d, v) => new { d.FullName, v.PatientId, v.VisitDate })
    .Join(Patients, res => res.PatientId, p => p.Id, (res, p) => new { DoctorName = res.FullName, PatientName = p.FullName, res.VisitDate })
    .GroupBy(x => x.DoctorName); // Группируем по врачу
    foreach (var group in query)
    {
        Console.WriteLine($"Врач: {group.Key}");
        // Сортируем внутри группы по дате
        foreach (var item in group.OrderBy(x => x.VisitDate))
            {
            Console.WriteLine($" - {item.VisitDate.ToShortDateString()}: {item.PatientName}");
            }
    }
}

public void PrintVisitsByPatient()
{
    var query = Patients
    .Join(Visits, p => p.Id, v => v.PatientId, (p, v) => new { p.FullName, v.VisitDate })
    .GroupBy(x => x.FullName);

    foreach (var group in query)
    {
        Console.WriteLine($"Пациент: {group.Key}");
        foreach (var v in group.OrderBy(x => x.VisitDate))
        {
            Console.WriteLine($" Дата: {v.VisitDate.ToShortDateString()}");
        }
    }
}

public void PrintPatientsByDiagnosis()
{
    var query = Diagnoses
    .Join(Visits, d => d.Id, v => v.DiagnosisId, (d, v) => new { d.Name, v.PatientId })
    .Join(Patients, res => res.PatientId, p => p.Id, (res, p) => new { DiagName = res.Name, PatientName = p.FullName })
    .GroupBy(x => x.DiagName);

    foreach (var group in query)
    {
        Console.WriteLine($"Диагноз: {group.Key}");
        foreach (var p in group.Select(x => x.PatientName).Distinct())
            {
            Console.WriteLine($" - {p}");
            }
    }
}
public void PrintPatientsWithSickLeave()
{
    var query = Visits
    .Where(v => v.HasSickLeave)
    .Join(Patients, v => v.PatientId, p => p.Id, (v, p) => new { p.FullName, v.VisitDate })
    .OrderBy(res => res.VisitDate);

    Console.WriteLine("Пациенты с больничным листом:");
    foreach (var item in query)
    {
        Console.WriteLine($"{item.VisitDate.ToShortDateString()} - {item.FullName}");
    }
}
public void PrintDoctorsBySpecialization()
{
    var query = Specializations
    .Join(Doctors, s => s.Id, d => d.SpecializationId, (s, d) => new { SpecName = s.Name, d.FullName, d.ExperienceYears })
    .GroupBy(x => x.SpecName);

    foreach (var group in query)
        {
        Console.WriteLine($"Специализация: {group.Key}");
        foreach (var doc in group)
            {
            Console.WriteLine($" - {doc.FullName} (стаж: {doc.ExperienceYears} лет)");
            }   
        }
    }
}