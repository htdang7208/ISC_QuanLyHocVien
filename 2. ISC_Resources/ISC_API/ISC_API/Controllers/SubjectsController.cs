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
    public class SubjectsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SubjectsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetSubjects()
        {
            return new BaseResponse
            {
                Message = "Get list success",
                Data = await _context.Subjects.ToListAsync()
            };
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 404,
                    Message = "Not found!"
                };
            }

            return new BaseResponse(subject);
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> PutSubject(int id, Subject subject)
        {
            if (id != subject.SUBJECTID)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Wrong id!"
                };
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Update successfully!",
                    Data = subject
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
                {
                    return new BaseResponse
                    {
                        ErrorCode = 404,
                        Message = "Not found!"
                    };
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostSubject(Subject subject)
        {
            try
            {
                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Added success",
                    Data = subject
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

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 404,
                    Message = "Not found!"
                };
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Deleted successfully!"
            };
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.SUBJECTID == id);
        }
    }
}
