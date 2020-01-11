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
    public class CourseTrainingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CourseTrainingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/CourseTrainings
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetCourseTraining()
        {
            List<Course_Training> list = await _context.CourseTraining
                                        .AsNoTracking()
                                        .Select(x => new Course_Training
                                        {
                                            COURSE_TRAINING_ID = x.COURSE_TRAINING_ID,
                                            TRAININGID = x.TRAININGID,
                                            COURSEID = x.COURSEID
                                        })
                                        .ToListAsync();
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

        // GET: api/TrainingSubjects/5
        [HttpGet("{courseId}")]
        public async Task<ActionResult<List<Course_Training>>> GetTrainingSubjectByTrainingId(int courseId)
        {
            List<Course_Training> list = await _context.CourseTraining
                .Where(x => x.TRAININGID == courseId)
                .ToListAsync();

            if (list.Count() == 0)
            {
                return NotFound();
            }
            return list;
        }

        [Route("getcoursetraining")]
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> GetTrainingSubject(Course_Training item)
        {
            Course_Training result = await _context.CourseTraining
                .Where(x => x.TRAININGID == item.TRAININGID)
                .Where(x => x.COURSEID == item.COURSEID)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 0,
                    Data = null
                };
            }
            return new BaseResponse
            {
                ErrorCode = 0,
                Data = result
            };
        }

        // PUT: api/CourseTrainings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse_Training(int id, Course_Training course_Training)
        {
            if (id != course_Training.COURSE_TRAINING_ID)
            {
                return BadRequest();
            }

            _context.Entry(course_Training).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Course_TrainingExists(id))
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

        // POST: api/CourseTrainings
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostCourse_Training(Course_Training course_Training)
        {
            try
            {
                _context.CourseTraining.Add(course_Training);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Added success",
                    Data = course_Training
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

        // DELETE: api/TrainingSubjects/5
        [HttpDelete("DeleteAllByCourseId/{courseId}")]
        public async Task<ActionResult<BaseResponse>> DeleteAllByTrainingId(int courseId)
        {
            List<Course_Training> list = await _context.CourseTraining
                .Where(x => x.COURSEID == courseId)
                .ToListAsync();

            if (list.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                foreach (Course_Training item in list)
                {
                    _context.CourseTraining.Remove(item);
                }
            }

            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                ErrorCode = 0,
                Message = "All subjects in training are deleted!"
            };
        }

        // DELETE: api/CourseTrainings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteCourse_Training(int id)
        {
            var item = await _context.CourseTraining.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.CourseTraining.Remove(item);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                ErrorCode = 0,
                Message = "Object Deleted!",
                Data = item
            };
        }

        private bool Course_TrainingExists(int id)
        {
            return _context.CourseTraining.Any(e => e.COURSE_TRAINING_ID == id);
        }
    }
}
