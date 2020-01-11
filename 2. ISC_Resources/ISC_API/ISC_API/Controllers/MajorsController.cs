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
    public class MajorsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MajorsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Majors
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetMajors()
        {
            return new BaseResponse
            {
                Message = "Get list success",
                Data = await _context.Majors.ToListAsync()
            };
        }

        // GET: api/Majors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetMajor(int id)
        {
            var major = await _context.Majors.FindAsync(id);

            if (major == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 404,
                    Message = "Not found!"
                };
            }

            return new BaseResponse {
                ErrorCode = 0,
                Data = major
            };
        }

        // PUT: api/Majors/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> PutMajor(int id, Major major)
        {
            if (id != major.MAJORID)
            {
                return new BaseResponse {
                    ErrorCode = 1,
                    Message = "Wrong id!"
                };
            }

            _context.Entry(major).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Update successfully!",
                    Data = major
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MajorExists(id))
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

        // POST: api/Majors
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostMajor(Major major)
        {
            try
            {
                _context.Majors.Add(major);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Added success",
                    Data = major
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

        // DELETE: api/Majors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteMajor(int id)
        {
            var major = await _context.Majors.FindAsync(id);
            if (major == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Not found!"
                };
            }

            _context.Majors.Remove(major);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Deleted successfully!"
            };
        }

        private bool MajorExists(int id)
        {
            return _context.Majors.Any(e => e.MAJORID == id);
        }
    }
}
