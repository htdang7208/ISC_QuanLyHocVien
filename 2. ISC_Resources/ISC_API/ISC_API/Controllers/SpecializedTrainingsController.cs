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
    public class SpecializedTrainingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SpecializedTrainingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/SpecializedTrainings
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetSpecializedTrainings()
        {
            List<TrainingInfo> list = await _context.SpecializedTrainings
                .AsNoTracking()
                .Select(x => new TrainingInfo
                {
                    TRAININGID = x.TRAININGID,
                    TRAININGNAME = x.TRAININGNAME,
                    NUMBERWEEK = x.NUMBERWEEK,
                    STATUS = x.STATUS
                })
                .ToListAsync();
            foreach (TrainingInfo item in list)
            {
                item.listSubjects = await _context.TrainingSubject
                    .AsNoTracking()
                    .Where(x => x.TRAININGID == item.TRAININGID)
                    .Select(x => new Subject
                    {
                        SUBJECTID = x.SUBJECTID,
                        SUBJECTNAME = x.Subject.SUBJECTNAME,
                        NUMBERLESSON = x.Subject.NUMBERLESSON
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

        // GET: api/SpecializedTrainings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetSpecializedTraining(int id)
        {
            BaseResponse result = new BaseResponse();
            var item = await _context.SpecializedTrainings
                .AsNoTracking()
                .Select(x => new TrainingInfo
                {
                    TRAININGID = x.TRAININGID,
                    TRAININGNAME = x.TRAININGNAME,
                    NUMBERWEEK = x.NUMBERWEEK,
                    STATUS = x.STATUS

                })
                .FirstOrDefaultAsync(x => x.TRAININGID == id);

            item.listSubjects = await _context.TrainingSubject
                    .AsNoTracking()
                    .Where(x => x.TRAININGID == item.TRAININGID)
                     .Select(x => new Subject
                     {
                         SUBJECTID = x.SUBJECTID,
                         SUBJECTNAME = x.Subject.SUBJECTNAME,
                         NUMBERLESSON = x.Subject.NUMBERLESSON
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
            var item = await _context.SpecializedTrainings.LastAsync();

            return new BaseResponse
            {
                ErrorCode = 0,
                Data = item
            };
        }

        // PUT: api/SpecializedTrainings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecializedTraining(int id, SpecializedTraining specializedTraining)
        {
            if (id != specializedTraining.TRAININGID)
            {
                return BadRequest();
            }

            _context.Entry(specializedTraining).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecializedTrainingExists(id))
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

        // POST: api/SpecializedTrainings
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostSpecializedTraining(SpecializedTraining specializedTraining)
        {
            try
            {
                _context.SpecializedTrainings.Add(specializedTraining);
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

        // DELETE: api/SpecializedTrainings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteSpecializedTraining(int id)
        {
            var specializedTraining = await _context.SpecializedTrainings.FindAsync(id);
            if (specializedTraining == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Object does not exist!"
                };
            }

            _context.SpecializedTrainings.Remove(specializedTraining);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                ErrorCode = 0,
                Message = "Object was deleted!"
            };
        }

        private bool SpecializedTrainingExists(int id)
        {
            return _context.SpecializedTrainings.Any(e => e.TRAININGID == id);
        }
    }
}
