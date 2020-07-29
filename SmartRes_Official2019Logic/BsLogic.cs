using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRes_Official2019Data;

using Microsoft.AspNet.Identity;
using System.Data.Entity;
using SmartRes_Official2019Data.ViewModels;

namespace SmartRes_Official2019Logic
{
    public class BsLogic
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<Notification> AllNotifications(string username)
        {
            //var username = User.Identity.GetUserName();
            return db.Notifications.Where(x=>x.Reciever==username).OrderBy(x=>x.NotDate).ToList();
        }

        public List<Check_In> LIstChk()
        {

            return db.Check_In.ToList();
        }


        public bool RemoveCheck_In(Check_In check_In)
        {
            try
            {
                db.Check_In.Remove(check_In);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public Check_In GetCheck_In(string Id)
        {
            return db.Check_In.Find(Id);
        }

        public List<Medical_Info> ListMedicInfo()
        {
            return db.Medical_Info.ToList();
        }
        public bool AddMedical_Info(Medical_Info medical_Info)
        {
            try
            {
                
                db.Medical_Info.Add(medical_Info);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
     

        public bool UpdateMedicInfo(Medical_Info medical_Info)
        {
            try
            {
                db.Entry(medical_Info).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }


        public bool RemovMedical_InfoF(Medical_Info medical_Info)
        {
            try
            {
                db.Medical_Info.Remove(medical_Info);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        //public Medical_Info GetMedical_Info(string Id)
        //{
        //    return db.Medical_Info(Id);
        //}


        public List<PDF> LIstPdf()
        {
            return db.PDFs.ToList();
        }
        public bool AddPDF(PDF pDF)
        {
            try
            {

                db.PDFs.Add(pDF);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool AddRes(MaintenanceReservation b)
        {
            try
            {

                db.MaintenanceReservations.Add(b);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }


        public bool UpdatePdf(PDF pDF)
        {
            try
            {
                db.Entry(pDF).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemovePDF(PDF pDF)
        {
            try
            {
                db.PDFs.Remove(pDF);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public PDF GetTPDF(int? Id)
        {
            return db.PDFs.Find(Id);
        }


        public List<ResAvailability> LIstResAvailability()
        {
            return db.ResAvailabilities.ToList();
        }

        public bool AddResAvailability(ResAvailability resAvailability)
        {
            try
            {

                db.ResAvailabilities.Add(resAvailability);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool UpdateResAvailability(ResAvailability resAvailability)
        {
            try
            {
                db.Entry(resAvailability).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveResAvailability(ResAvailability resAvailability)
        {
            try
            {
                db.ResAvailabilities.Remove(resAvailability);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public ResAvailability GetTResAvailability(int? Id)
        {
            return db.ResAvailabilities.Find(Id);
        }


        public List<Residence> LIstResidences()
        {
            return db.Residences.ToList();
        }

        public bool AddResidence(Residence residence)
        {
            try
            {

                db.Residences.Add(residence);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool UpdateResidence(Residence residence)
        {
            try
            {
                db.Entry(residence).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveResidence(Residence residence)
        {
            try
            {
                db.Residences.Remove(residence);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Residence GetTResidence(int? Id)
        {
            return db.Residences.Find(Id);
        }
        public List<Room> LIstRooms()
        {
            return db.Rooms.ToList();

        }

        public bool UpdateRoom(Room room)
        {
            try
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool AddRoom(Room room)
        {
            try
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }





        public bool AddEvents(Event ev)
        {
            try
            {
                db.Events.Add(ev);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }











        public bool RemoveRoom(Room room)
        {
            try
            {
                db.Rooms.Remove(room);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Room GetTRoom(int? Id)
        {
            return db.Rooms.Find(Id);
        }

        public List<RoomType> LIstRoomTypes()
        {
            return db.RoomTypes.ToList();

        }
        public bool AddRoomType(RoomType roomType)
        {
            try
            {
                db.RoomTypes.Add(roomType);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool UpdateRoomTypes(RoomType roomTypes)
        {
            try
            {
                db.Entry(roomTypes).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveRoomType(RoomType roomType)
        {
            try
            {
                db.RoomTypes.Remove(roomType);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public RoomType GetTRoomType(int? Id)
        {
            return db.RoomTypes.Find(Id);
        }

        public List<SAN_Employee> LIstSAN_Employee()
        {
            return db.SAN_Employee.ToList();

        }
        public bool AddSAN_Employee(SAN_Employee sAN_Employee)
        {
            try
            {
                db.SAN_Employee.Add(sAN_Employee);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool UpdateSAN_Employee(SAN_Employee sAN_Employee)
        {
            try
            {
                db.Entry(sAN_Employee).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveSAN_Employee(SAN_Employee sAN_Employee)
        {
            try
            {
                db.SAN_Employee.Remove(sAN_Employee);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public SAN_Employee GetTSAN_Employee(string Id)
        {
            return db.SAN_Employee.Find(Id);
        }
        public List<Student> LIstStudent()
        {
            return db.Students.ToList();

        }
        public bool AddStudent(Student student)
        {
            try
            {
                db.Students.Add(student);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool UpdateStudent(Student student)
        {
            try
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveStudent(Student student)
        {
            try
            {
                db.Students.Remove(student);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Student GetTStudent(string Id)
        {
            return db.Students.Find(Id);
        }

        public List<Student_Otp> ListStudent_Otp()
        {
            return db.Student_Otp.ToList();

        }
        public bool AddStudent_Otp(Student_Otp student_Otp)
        {
            try
            {
                db.Student_Otp.Add(student_Otp);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool UpdateStudent_Otp(Student_Otp student_Otp)
        {
            try
            {
                db.Entry(student_Otp).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveStudent_Otp(Student_Otp student_Otp)
        {
            try
            {
                db.Student_Otp.Remove(student_Otp);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Student_Otp GetTStudent_Otp(string Id)
        {
            return db.Student_Otp.Find(Id);
        }
        public List<StudentCheckIn> ListStudentCheckIn()
        {
            return db.StudentCheckIns.ToList();

        }

        public bool AddStudentCheckIn(StudentCheckIn studentCheckIn)
        {
            try
            {
                db.StudentCheckIns.Add(studentCheckIn);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool UpdateStudentCheckIn(StudentCheckIn studentCheckIn)
        {
            try
            {
                db.Entry(studentCheckIn).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveStudentCheckIn(StudentCheckIn studentCheckIn)
        {
            try
            {
                db.StudentCheckIns.Remove(studentCheckIn);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public StudentCheckIn GetStudentCheckIn(string Id)
        {
            return db.StudentCheckIns.Find(Id);
        }
        public List<TemporaryBooking> ListTemporaryBooking()
        {
            return db.TemporaryBookings.ToList();

        }
        public bool AddTemporaryBooking(TemporaryBooking temporaryBooking)
        {
            try
            {
                db.TemporaryBookings.Add(temporaryBooking);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool UpdateTemporaryBooking(TemporaryBooking temporaryBooking)
        {
            try
            {
                db.Entry(temporaryBooking).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveTemporaryBooking(TemporaryBooking temporaryBooking)
        {
            try
            {
                db.TemporaryBookings.Remove(temporaryBooking);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public TemporaryBooking GetTemporaryBooking(int? Id)
        {
            return db.TemporaryBookings.Find(Id);
        }
        public List<University> ListUniversity()
        {
            return db.Universities.ToList();

        }
        public bool AddUniversity(University university)
        {
            try
            {
                db.Universities.Add(university);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool UpdateUniversity(University university)
        {
            try
            {
                db.Entry(university).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool RemoveUniversity(University university)
        {
            try
            {
                db.Universities.Remove(university);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public University GetTUniversity(int? Id)
        {
            return db.Universities.Find(Id);
        }

        public List<UniversityEmployee> ListUniversityEmployee()
        {
            return db.UniversityEmployees.ToList();

        }
        public bool AddUniversityEmployee(UniversityEmployee universityEmployee)
        {
            try
            {
                db.UniversityEmployees.Add(universityEmployee);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool UpdateUniversityEmployee(UniversityEmployee universityEmployee)
        {
            try
            {
                db.Entry(universityEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveUniversityEmployee(UniversityEmployee universityEmployee)
        {
            try
            {
                db.UniversityEmployees.Remove(universityEmployee);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public UniversityEmployee GetTUniversityEmployee(string Id)
        {
            return db.UniversityEmployees.Find(Id);
        }
        public List<RoomStudent> ListRoomStudent()
        {
            return db.RoomStudents.ToList();

        }
        public bool AddRoomStudent(RoomStudent roomStudent)
        {
            try
            {
                db.RoomStudents.Add(roomStudent);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool UpdateRoomStudent(RoomStudent roomStudent)
        {
            try
            {
                db.Entry(roomStudent).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveRoomStudent(RoomStudent roomStudent)
        {
            try
            {
                db.RoomStudents.Remove(roomStudent);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public RoomStudent GetTRoomStudent(int? Id)
        {
            return db.RoomStudents.Find(Id);
        }
        //Edit
        public bool UpdateCheckIn(Check_In check_In)
        {
            try
            {
                db.Entry(check_In).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }



        //Creates
        public bool AddCheckIn(Check_In check_In)
        {
            try
            {

                db.Check_In.Add(check_In);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public List<MaintenanceSuppliers> ListMaintenanceSupp()
        {
            return db.maintenanceSuppliers.ToList();

        }
        public MaintenanceSuppliers GetTMaintenanceSupp(int? Id)
        {
            return db.maintenanceSuppliers.Find(Id);
        }
        public bool AddMaintenanceSupp(MaintenanceSuppliers maintenanceSuppliers)
        {
            try
            {
                db.maintenanceSuppliers.Add(maintenanceSuppliers);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveMaintenanceSupp(MaintenanceSuppliers maintenanceSuppliers)
        {
            try
            {
                db.maintenanceSuppliers.Remove(maintenanceSuppliers);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public List<MaintenanceRequest> ListMaintenanceRequest()
        {
            return db.MaintenanceRequests.ToList();

        }
        public bool AddMaintenanceRequest(MaintenanceRequest maintenanceRequest)
        {
            try
            {
                db.MaintenanceRequests.Add(maintenanceRequest);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public bool UpdateMaintenanceRequest(MaintenanceRequest maintenanceRequest)
        {
            try
            {
                db.Entry(maintenanceRequest).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveMaintenanceRequest(MaintenanceRequest maintenanceRequest)
        {
            try
            {
                db.MaintenanceRequests.Remove(maintenanceRequest);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public MaintenanceRequest GetTMaintenanceRequest(int? Id)
        {
            return db.MaintenanceRequests.Find(Id);
        }

     

    
      
   

        public List< StudentProfile> StudentProfiles()
        {
            List<StudentProfile> stp = new List<StudentProfile>();

            var Record = (from stud in db.Students
                          join chk in db.StudentCheckIns on stud.StudId equals chk.StudId
                          select new
                          {
                              stud.StudentNumber,
                              stud.PhoneNumber,
                              stud.Name,
                              stud.Surname,
                              stud.IdNumber,
                              stud.Email,
                              chk.ResName,
                              chk.RooNumber

                          }).ToList();
            StudentProfile stupp = new StudentProfile();
            foreach (var item in Record)
            {
                stupp.StudentNumber = item.StudentNumber;
                stupp.PhoneNumber = item.PhoneNumber;
                stupp.IdNumber = item.IdNumber;
                stupp.ResName = item.ResName;
                stupp.Email = item.Email;
                stupp.InitialSurname = item.Surname.Substring(0, 1) + ". " + item.Surname;
                stp.Add(stupp);
            }
            return stp;
        }

        }
    }



    