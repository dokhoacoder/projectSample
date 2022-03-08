using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudenAPI.DBContext;
using StudenAPI.Models;

namespace StudenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentManagerContext _context;
        private readonly int LimitItem = 1000;
        public StudentController(StudentManagerContext context)
        {
            _context = context;
        }
        // GET: api/student/list/page=:numPage
        [Route("list/page={numPage}")]
        [HttpGet]
        public async Task<IActionResult> GetStudents(int numPage)
        {
            var ListStudent = await _context.Sinhviens.OrderBy(o => o.Id).Skip((numPage - 1) * LimitItem).Take(LimitItem).ToListAsync();
            float numberpage = (float)_context.Sinhviens.ToList().Count() / (float)LimitItem;
            if (numPage > (int)Math.Ceiling(numberpage) && (int)Math.Ceiling(numberpage) > 0)
            {
                return BadRequest(new MessageRequest()
                {
                    status = "400",
                    message = "Paging size limit exceeded!",
                    description = ""
                });
            }
            else if ((int)Math.Ceiling(numberpage) == 0)
            {
                return Ok(new StudentRequest()
                {
                    totalPage = 1,
                    Page = 1,
                    data = ListStudent
                });
            }
            else
            {
                return Ok(new StudentRequest()
                {
                    totalPage = (int)Math.Ceiling(numberpage),
                    Page = numPage,
                    data = ListStudent
                });
            }
        }
        // GET api/student/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<Sinhvien>> GetStudentByID(int id)
        {
            var sv = await _context.Sinhviens.FindAsync(id);
            if (sv == null)
            {
                return BadRequest(new MessageRequest()
                {
                    status = "400",
                    message = "Student not exist!",
                    description = ""
                });
            }
            return sv;
        }
        // POST api/student/add
        [HttpPost("add")]
        public async Task<ActionResult<Sinhvien>> PostStudent(Sinhvien data)
        {
            var checkExist = _context.Sinhviens.Where(w => w.Cmnd == data.Cmnd).FirstOrDefault();
            if (checkExist == null)
            {
                _context.Sinhviens.Add(data);
            }
            else
            {
                return BadRequest(new MessageRequest()
                {
                    status = "400",
                    message = "Student is exist !",
                    description = ""
                });
            }
            var sttCode = await _context.SaveChangesAsync();
            if (sttCode > 0)
            {
                return Ok(new MessageRequest()
                {
                    status = "204",
                    message = "Student is created!",
                    description = ""
                });
            }
            else
            {
                return BadRequest(new MessageRequest()
                {
                    status = "400",
                    message = "Add Student Fail!",
                    description = ""
                });
            }
        }
        // PUT api/student/update/:id
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutStudents(int id, Sinhvien data)
        {
            var student = await _context.Sinhviens.FirstOrDefaultAsync(u => u.Id == id);
            if (student != null)
            {
                student.Name = data.Name;
                student.Gender = data.Gender;
                student.Cmnd = data.Cmnd;
                student.Phone = data.Phone;
                student.Class = data.Class;
            }
            else
            {
                return BadRequest(new MessageRequest()
                {
                    status = "400",
                    message = "Student not exist!",
                    description = ""
                });
            }
            var sttCode = await _context.SaveChangesAsync();
            if (sttCode > 0)
            {
                return Ok(new MessageRequest()
                {
                    status = "201",
                    message = "Update Student Success!",
                    description = ""
                });
            }
            else
            {
                return BadRequest(new MessageRequest()
                {
                    status = "403",
                    message = "Update Student Fail!",
                    description = ""
                });
            }
        }

        // DELETE api/student/delete/:id
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStudents(int id)
        {
            var student = await _context.Sinhviens.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                return BadRequest(new MessageRequest()
                {
                    status = "400",
                    message = "Student not exist!",
                    description = ""
                });
            }
            else
            {
                _context.Sinhviens.Remove(student);
            }
            var sttCode = await _context.SaveChangesAsync();
            if (sttCode > 0)
            {
                return Ok(new MessageRequest()
                {
                    status = "200",
                    message = "Delete Student success!",
                    description = ""
                });
            }
            else
            {
                return BadRequest(new MessageRequest()
                {
                    status = "403",
                    message = "Delete Student Fail!",
                    description = ""
                });
            }
        }
    }
}