using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISC_API.Models;
using ISC_API.Models.Response;

namespace ISC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StudentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetStudents()
        {
            return new BaseResponse
            {
                Message = "Get list success",
                Data = await _context.Students.Include(item => item.University)
                    .Include(item => item.Major)
                    .Select(item => new StudentInfo
                    {
                        ID = item.STUDENTID,
                        LASTNAME = item.LASTNAME,
                        FIRSTNAME = item.FIRSTNAME,
                        GENDER = item.GENDER,
                        EMAIL = item.EMAIL,
                        DOB = item.DOB,
                        IDENTITYNUMBER = item.IDENTITYNUMBER,
                        ADDRESS = item.ADDRESS,
                        CERTIFICATION = item.CERTIFICATION,
                        DATEREADYTOWORK = item.DATEREADYTOWORK,
                        DEPOSITS = item.DEPOSITS,
                        University = item.University,
                        Major = item.Major
                    }).ToListAsync()
            };
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Not Found"
                };
            }

            return new BaseResponse
            {
                Message = "Get success",
                Data = student
            };
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> PutStudent(int id, Student student)
        {
            var updatedStudent = await _context.Students.FindAsync(id);
            if (updatedStudent == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Not Found"
                };
            }

            updatedStudent.LASTNAME = student.LASTNAME;
            updatedStudent.FIRSTNAME = student.FIRSTNAME;
            updatedStudent.GENDER = student.GENDER;
            updatedStudent.EMAIL = student.EMAIL;
            updatedStudent.DOB = student.DOB;
            updatedStudent.IDENTITYNUMBER = student.IDENTITYNUMBER;
            updatedStudent.ADDRESS = student.ADDRESS;
            updatedStudent.MAJORID = student.MAJORID;
            updatedStudent.UNIVERSITYID = student.UNIVERSITYID;
            updatedStudent.DATEREADYTOWORK = student.DATEREADYTOWORK;
            updatedStudent.CERTIFICATION = student.CERTIFICATION;
            updatedStudent.DEPOSITS = student.DEPOSITS;
            _context.Students.Update(updatedStudent);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Update success",
                Data = student
            };
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostStudent(Student student)
        {
            try
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Added success",
                    Data = student
                };
            }
            catch
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Adding fail"
                };
            }
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Delete fail"
                };
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Delete success"
            };
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.STUDENTID == id);
        }
    }
}
