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
    public class WorktracksController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public WorktracksController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Worktracks
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetWorktracks()
        {
            var list = await _context.Worktracks
                .Include(x => x.Company)
                .Include(x => x.Student)
                .Select(x => new WorktrackInfo
                {
                    WORKTRACKID = x.WORKTRACKID,
                    Company = x.Company,
                    Student = x.Student,
                    STARTDATE = x.STARTDATE,
                    CONTRACTDATE = x.CONTRACTDATE,
                    STATUS = x.STATUS,
                    NOTE = x.NOTE
                })
                .ToListAsync();

            return new BaseResponse
            {
                ErrorCode = 0,
                Data = list
            };
        }

        // GET: api/Worktracks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetWorktrack(int id)
        {
            var worktrack = await _context.Worktracks.FindAsync(id);

            if (worktrack == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Error!! This worktrack is not exists!"
                };
            }

            return new BaseResponse
            {
                ErrorCode = 0,
                Data = worktrack
            };
        }

        // PUT: api/Worktracks/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> PutWorktrack(int id, Worktrack worktrack)
        {
            if (id != worktrack.WORKTRACKID)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Wrong id!"
                };
            }

            _context.Entry(worktrack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Update successfully!",
                    Data = worktrack
                };

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorktrackExists(id))
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

        // POST: api/Worktracks
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostWorktrack(Worktrack worktrack)
        {
            try
            {
                _context.Worktracks.Add(worktrack);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Added success",
                    Data = worktrack
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

        // DELETE: api/Worktracks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteWorktrack(int id)
        {
            var worktrack = await _context.Worktracks.FindAsync(id);
            if (worktrack == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 404,
                    Message = "Not found!"
                };
            }

            _context.Worktracks.Remove(worktrack);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Deleted successfully!"
            };
        }

        private bool WorktrackExists(int id)
        {
            return _context.Worktracks.Any(e => e.WORKTRACKID == id);
        }
    }
}
