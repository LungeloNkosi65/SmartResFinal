using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRes_Official2019Data;

namespace SmartRes_Official2019Data
{
    public class Logic
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public String getResForNotification(string userName)
        {
            var res = (from r in db.UniversityEmployees
                       where r.Email == userName
                       select r.UnivbersityId).FirstOrDefault();
            var resname = (from R in db.Residences
                           where R.UnivbersityId == res
                           select R.ResName).FirstOrDefault();
            return resname;
        }
        public int getUniversityId(string userName)
        {
            var res = (from r in db.UniversityEmployees
                       where r.Email == userName
                       select r.UnivbersityId).FirstOrDefault();
            return res;
        }
        public bool exists(string id)
        {
            bool exist = false;
            foreach (var e in db.TemporaryBookings)
            {
                if (e.StudentNumber.Equals(id))
                {
                    exist = true;
                }
            }

            return exist;
        }

        public bool CheckStudentNumber(string StudNo)
        {
            bool exist = false;
            foreach (var e in db.Students)
            {
                if (e.StudentNumber.Equals(StudNo))
                {
                    exist = true;
                }
            }

            return exist;
        }
        public bool StudentNoExists(string StudNo)
        {
            bool exist = false;
            foreach (var e in db.Students)
            {
                if (e.Email.Equals(StudNo))
                {
                    exist = true;
                }
            }

            return exist;
        }

        //Get Students Id Number From Temporary Booking if Student Number Matches


        public bool CheckWeekDays(string wday)
        {
            int counter = 0;
            bool result = false;
            var week = db.WeekDays.ToList();
            foreach (var item in week)
            {
                counter++;


                if (item.CurrDayofWeek == wday && counter <= 7)
                {
                    result = true;

                }


            }

            return result;
        }


        public double getId(string id)
        {
            double Idnum = 0;
            foreach (var e in db.TemporaryBookings)
            {
                if (e.StudentNumber == id)
                {
                    Idnum = e.IDNumber;
                }

            }
            return Idnum;
        }
        public string getStudentGender(string id)
        {
            string Gen = " ";
            foreach (var e in db.TemporaryBookings)
            {
                if (e.StudentNumber == id)
                {
                    Gen = e.Gender;
                }

            }
            return Gen;
        }

        public bool CorectId(double id)
        {
            bool exist = false;
            foreach (var e in db.TemporaryBookings)
            {
                if (e.IDNumber.Equals(id))
                {
                    exist = true;
                }
            }

            return exist;
        }

        public bool RoomNoExists(string RoomNumber, int resId)
        {
            bool exist = false;
            foreach (var e in db.Rooms)
            {
                if (e.RoomNumber.Equals(RoomNumber) && e.ResId == resId)
                {
                    exist = true;
                }
            }

            return exist;
        }

        public int getStudentRes(double studentid)
        {
            TemporaryBooking bk = db.TemporaryBookings.ToList().Find(x => x.IDNumber == studentid);
            int redid = bk.ResId;

            return db.Residences.Find(redid).ResId;
        }

        public int getStudentRoom(string RoomNo)
        {
            Room bk = db.Rooms.ToList().Find(x => x.RoomNumber == RoomNo);
            int redid = bk.ResId;

            return db.Residences.Find(redid).ResId;
        }

        public string getGender(string roomno)
        {//
            var chkin = db.StudentCheckIns.ToList().Find(x => x.RooNumber == roomno);
            if (chkin == null)
            {
                return "";
            }
            var gender = db.Students.ToList().Find(x => x.Email == chkin.StudId).Gender;
            //
            return gender;
        }

        public bool canCheckIn(string username, string roomno)
        {
            var student = db.Students.ToList().Find(x => x.Email == username);
            bool allowed = false;
            if (student.Gender == getGender(roomno) || getGender(roomno) == "")
            {
                allowed = true;
            }

            return allowed;
        }

        public bool CheckGender(int ResId, string Gender)
        {
            bool resukt = false;
            //var student = db.Students.ToList();
            var res = db.Residences.Where(x => x.ResId == ResId).FirstOrDefault();

            if (res.Gender_Allocation == Gender || res.Gender_Allocation=="Both")
            {
                resukt = true;
            }

            return resukt;
        }

        public string getResGender(int id)
        {
            var res = db.Residences.ToList();
            string gender = "";
            foreach (var item in res)
            {
                if (item.ResId == id)
                {
                    gender = item.Gender_Allocation;
                }
            }
            return gender;
        }


        //GetRoom Number For Maintenance
        public string GetRoomNumber(string email)
        {
            MaintenanceRequest mainReq = new MaintenanceRequest();
            Student stud = new Student();

            var Rnum = (from S in db.StudentCheckIns
                        where S.Student.Email == email
                        select S.RooNumber).FirstOrDefault();
            return Rnum;
        }
        public string GetResForMaintenance(string email)
        {
            MaintenanceRequest mainReq = new MaintenanceRequest();
            Student stud = new Student();

            var Rnum = (from S in db.StudentCheckIns
                        where S.Student.Email == email
                        select S.ResName).FirstOrDefault();
            return Rnum;
        }

        public bool ChechIdForCheckIn(double Id)
        {
            bool result = false;
            foreach (var item in db.TemporaryBookings)
            {
                if (item.IDNumber == Id)
                {
                    result = true;
                }
            }
            return result;

        }



        public bool checkForCheckIn( string email)
        {
            var getId = (from s in db.Students
                         where s.Email == email
                         select s.StudId).FirstOrDefault();
            bool result = false;
            foreach (var e in db.StudentCheckIns)
            {
                if (e.StudId == getId)
                {
                    result = true;
                }

            }
            return result;
        }

        public string getResName(string id)
        {
            var Temp = db.TemporaryBookings.ToList();
            string name = " ";
            foreach (var item in Temp)
            {
                if (item.StudentNumber == id)
                {
                    name = item.Residence.ResName;
                }
            }
            return name;

        }


        public string getResNameWith(string id)
        {
            var Temp = db.Students.ToList();
            string name = " ";
            foreach (var item in Temp)
            {
                if (item.Email == id)
                {
                    name = item.ResName;
                }
            }
            return name;

        }


        //GEtStudent Number With OTP
        public string GetStudentNumber(string Otp)
        {
            var tempBooking = db.TemporaryBookings.Where(x => x.OTPCode == Otp).FirstOrDefault();
            return tempBooking.StudentNumber;
        }

        //GEtSTudent Email from Dummy

        public string GetEail(string stuNum)
        {
            var dummy = db.Students_Dummies.Where(x => x.StudentNumber == stuNum).FirstOrDefault();
            return dummy.StudentEmail;
        }



    }
}