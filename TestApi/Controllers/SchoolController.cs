using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestWebAPI.Models;

namespace TestWebAPI.Controllers
{
    public class SchoolController : ApiController
    {
        private TestWebAPI.iseeSCHOOLEntities ctx = new TestWebAPI.iseeSCHOOLEntities();
        public IHttpActionResult getAllStudents()
        {
            //Creating and initializing viewmodel object
            List<TestWebAPI.STUDENT_MST> students = ctx.STUDENT_MST.ToList();
            //Calling view & passing viewmodel object to view
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }
        public IHttpActionResult GetStudentById(string id)
        {
            TestWebAPI.STUDENT_MST student = null;

            student = ctx.STUDENT_MST.Where(c=> c.SM_ID==id).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
        
        
        [HttpPost]
        public IHttpActionResult UpdateStudentMobile(UpdateStudentMob upd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TestWebAPI.STUDENT_MST student = null;

            student = ctx.STUDENT_MST.Where(c => c.SM_ID == upd.stud_id).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            student.SM_MOBILE = upd.stud_mobile;
            ctx.SaveChanges();
            return Ok("success");
        }

        //[Route("api/School/UpdStudentMobile_EntryDate")]
        //[ActionName("UpdStudentMobile_EntryDate")]
        [HttpPost]        
        public IHttpActionResult UpdStudentMobile_EntryDate(string id, string mob, bool updateEntryDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TestWebAPI.STUDENT_MST student = null;

            student = ctx.STUDENT_MST.Where(c => c.SM_ID == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            student.SM_MOBILE = mob;
            if (updateEntryDate)
            {
                student.SM_ENTRYDATE = DateTime.Now;
            }
            ctx.SaveChanges();
            return Ok("success");
        }


        [HttpPost]
        public IHttpActionResult UpdStudentMobile_EntryDate1(bool updateEntryDate, UpdateStudentMob stud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TestWebAPI.STUDENT_MST student = null;

            student = ctx.STUDENT_MST.Where(c => c.SM_ID == stud.stud_id).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            student.SM_MOBILE = stud.stud_mobile;
            if (updateEntryDate)
            {
                student.SM_ENTRYDATE = DateTime.Now;
            }
            ctx.SaveChanges();
            return Ok("success");
        }

        [Route("api/UpdStudentMobile")]
        [ActionName("UpdStudentMobile")]
        [HttpPost]
        public IHttpActionResult UpdStudMobile(UpdateStudentMob upd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TestWebAPI.STUDENT_MST student = null;

            student = ctx.STUDENT_MST.Where(c => c.SM_ID == upd.stud_id).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            student.SM_MOBILE = upd.stud_mobile;
            student.SM_ENTRYDATE = DateTime.Now;
            ctx.SaveChanges();
            return Ok("success");
        }

    }
}
