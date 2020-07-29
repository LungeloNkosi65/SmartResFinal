using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRes_Official2019Data;

using Microsoft.AspNet.Identity;
using System.Data.Entity;
using SmartRes_Official2019Data.ViewModels;
namespace SmartRes_Official2019Logic
{
  public  class Students_Dummy_Services
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public List<Students_Dummy> LIstStudents_Dummy()
        {

            return db.Students_Dummies.ToList();
        }
        public bool RemoveStudents_Dummy(Students_Dummy students_Dummy)
        {
            try
            {
                db.Students_Dummies.Remove(students_Dummy);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public Students_Dummy GetStudents_Dummy(string Id)
        {
            return db.Students_Dummies.Find(Id);
        }


        public bool UpdateStudents_Dummy(Students_Dummy students_Dummy)
        {
            try
            {
                db.Entry(students_Dummy).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }



        public bool AddStudents_Dummy(Students_Dummy students_Dummy)
        {
            try
            {

                db.Students_Dummies.Add(students_Dummy);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
    }
}
