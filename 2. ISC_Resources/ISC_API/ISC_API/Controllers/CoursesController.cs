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
    public class CoursesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CoursesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetCourses()
        {
            List<CourseInfo> list = await _context.Courses
                .AsNoTracking()
                .Select(x => new CourseInfo
                {
                    COURSEID = x.COURSEID,
                    COURSENAME = x.COURSENAME,
                    STARTDATE = x.STARTDATE,
                    ENDDATE = x.ENDDATE,
                    NOTE = x.NOTE
                })
                .ToListAsync();
            foreach (CourseInfo item in list)
            {
                item.listTrainings = await _context.CourseTraining
                    .AsNoTracking()
                    .Where(x => x.COURSEID == item.COURSEID)
                    .Select(x => new SpecializedTraining
                    {
                        TRAININGID = x.TRAININGID,
                        TRAININGNAME = x.Training.TRAININGNAME,
                    })
                    .ToListAsync();
            }

            BaseResponse result = new BaseResponse();
            if (list != null)
            {
                result.ErrorCode = 0;
                result.Data = list;
            }
            else
            {
                result.ErrorCode = 1;
                result.Message = "Data is not available!";
            }
            return result;
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetCourse(int id)
        {
            BaseResponse result = new BaseResponse();
            var item = await _context.Courses
                .AsNoTracking()
                .Select(x => new CourseInfo
                {
                    COURSEID = x.COURSEID,
                    COURSENAME = x.COURSENAME,
                    STARTDATE = x.STARTDATE,
                    ENDDATE = x.ENDDATE,
                    NOTE = x.NOTE
                })
                .FirstOrDefaultAsync(x => x.COURSEID == id);

            item.listTrainings = await _context.CourseTraining
                     .AsNoTracking()
                     .Where(x => x.COURSEID == item.COURSEID)
                     .Select(x => new SpecializedTraining
                     {
                         TRAININGID = x.TRAININGID,
                         TRAININGNAME = x.Training.TRAININGNAME,
                     })
                     .ToListAsync();

            if (item == null)
            {
                result.ErrorCode = 404;
                result.Message = "Not found";
            }
            else
            {
                result.ErrorCode = 0;
                result.Data = item;
            }
            return result;
        }

        [Route("getthelast")]
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetTheLast()
        {
            var item = await _context.Courses.LastAsync();

            return new BaseResponse
            {
                ErrorCode = 0,
                Data = item
            };
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.COURSEID)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                return new BaseResponse
                {
                    Message = "Add successfully!"
                };
            }
            catch
            {
                return new BaseResponse
                {
                    Message = "Add fail!"
                };
            }
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Object does not exist!"
                };
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                ErrorCode = 0,
                Message = "Object was deleted!"
            };
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.COURSEID == id);
        }
    }
}
