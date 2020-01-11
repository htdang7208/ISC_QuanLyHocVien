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
    public class CompaniesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CompaniesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetCompanies()
        {
            var companies = await _context.Companies.ToListAsync();

            return new BaseResponse {
                ErrorCode = 0,
                Data = companies
            };
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Error get. This company is not exists!"
                };
            }
            return new BaseResponse(company);
        }

        // PUT: api/Companies/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> PutCompany(int id, Company company)
        {
            if (id != company.COMPANYID)
            {
                return new BaseResponse {
                    ErrorCode = 1,
                    Message = "The company does not exist!" 
                };
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    ErrorCode = 0,
                    Message = "Updated!"
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Companies
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            CreatedAtAction("Get", new { id = company.COMPANYID }, company);
            return new BaseResponse
            {
                Message = "Postted"
            };
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Error delete. This company is not exists!"
                };
            }
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Deleted!",
                Data = company
            };
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.COMPANYID == id);
        }
    }
}
