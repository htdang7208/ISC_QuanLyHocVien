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
    public class UniversitiesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UniversitiesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Universities
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetUniversitys()
        {
            return new BaseResponse
            {
                Message = "Get list success",
                Data = await _context.Universitys.ToListAsync()
            };
        }

        // GET: api/Universities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetUniversity(int id)
        {
            var university = await _context.Universitys.FindAsync(id);

            if (university == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Not Found"
                };
            }

            return new BaseResponse
            {
                Message = "Get successfully!",
                Data = university
            };
        }

        // PUT: api/Universities/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> PutUniversity(int id, University university)
        {
            if (id != university.UNIVERSITYID)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Wrong id!"
                };
            }

            _context.Entry(university).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Update successfully!",
                    Data = university
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversityExists(id))
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

        // POST: api/Universities
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostUniversity(University university)
        {
            try
            {
                _context.Universitys.Add(university);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Added success",
                    Data = university
                };
            }
            catch
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Added fail!"
                };
            }
        }

        // DELETE: api/Universities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteUniversity(int id)
        {
            var university = await _context.Universitys.FindAsync(id);
            if (university == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Deleted fail!"
                };
            }

            _context.Universitys.Remove(university);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Deleted successfully!"
            };
        }

        private bool UniversityExists(int id)
        {
            return _context.Universitys.Any(e => e.UNIVERSITYID == id);
        }
    }
}
