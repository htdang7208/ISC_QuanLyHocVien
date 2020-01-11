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
    [Route("api/trainingsubjects")]
    [ApiController]
    public class TrainingSubjectsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TrainingSubjectsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TrainingSubjects
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetTrainingSubjects()
        {
            List<Training_Subject> list = await _context.TrainingSubject
                            .AsNoTracking()
                            .Select(x => new Training_Subject
                            {
                                TRAINING_SUBJECT_ID = x.TRAINING_SUBJECT_ID,
                                TRAININGID = x.TRAININGID,
                                SUBJECTID = x.SUBJECTID
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
        [HttpGet("{trainingId}")]
        public async Task<ActionResult<List<Training_Subject>>> GetTrainingSubjectByTrainingId(int trainingId)
        {
            List<Training_Subject> trainingSubjects = await _context.TrainingSubject
                .Where(x => x.TRAININGID == trainingId)
                .ToListAsync();

            if (trainingSubjects.Count() == 0)
            {
                return NotFound();
            }
            return trainingSubjects;
        }

        [Route("gettrainingsubject")]
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> GetTrainingSubject(Training_Subject item)
        {
            Training_Subject result = await _context.TrainingSubject
                .Where(x => x.TRAININGID == item.TRAININGID)
                .Where(x => x.SUBJECTID == item.SUBJECTID)
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

        // PUT: api/TrainingSubjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraining_Subject(int id, Training_Subject training_Subject)
        {
            if (id != training_Subject.TRAINING_SUBJECT_ID)
            {
                return BadRequest();
            }

            _context.Entry(training_Subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Training_SubjectExists(id))
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

        // POST: api/TrainingSubjects
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostTraining_Subject(Training_Subject training_Subject)
        {
            try
            {
                _context.TrainingSubject.Add(training_Subject);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Added success",
                    Data = training_Subject
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
        [HttpDelete("DeleteAllByTrainingId/{trainingId}")]
        public async Task<ActionResult<BaseResponse>> DeleteAllByTrainingId(int trainingId)
        {
            List<Training_Subject> list = await _context.TrainingSubject
                .Where(x => x.TRAININGID == trainingId)
                .ToListAsync();

            if (list.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                foreach (Training_Subject item in list)
                {
                    _context.TrainingSubject.Remove(item);
                }
            }

            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                ErrorCode = 0,
                Message = "All subjects in training are deleted!"
            };
        }


        // DELETE: api/TrainingSubjects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteTraining_Subject(int id)
        {
            var trainingSubject = await _context.TrainingSubject.FindAsync(id);
            if (trainingSubject == null)
            {
                return NotFound();
            }

            _context.TrainingSubject.Remove(trainingSubject);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                ErrorCode = 0,
                Message = "Object Deleted!",
                Data = trainingSubject
            };
        }

        private bool Training_SubjectExists(int id)
        {
            return _context.TrainingSubject.Any(e => e.TRAINING_SUBJECT_ID == id);
        }
    }
}
