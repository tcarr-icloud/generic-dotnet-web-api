using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webapi;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly DatabaseApiContext _context;
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public UsersController(IMapper mapper, DatabaseApiContext context)
    {
        _context = context;
        _mapper = mapper;
        _unitOfWork = new UnitOfWork(context);
    }
    
    [HttpGet]
    public async Task<IEnumerable<User>> GetUser()
    {
        // Expression<Func<User, bool>> expr = u => u.Guid.ToString().Equals("507a43f4-6806-4035-b2b5-e12d6982df6d");
        // return await _unitOfWork.UserRepository.GetAsync(u => u.Guid.ToString().Equals("507a43f4-6806-4035-b2b5-e12d6982df6d"));
        return await _unitOfWork.UserRepository.GetAsync();
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<User>> GetUser(long id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

        if (user == null) return NotFound();

        return user;
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> PutUser(long id, User user)
    {
        if (id != user.ID) return BadRequest();

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        var newUser = await _unitOfWork.UserRepository.InsertAsync(user);

        // if (Guid.Empty == user.Guid) user.Guid = Guid.NewGuid();
        // _context.User.Add(user);
        // await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = newUser.ID }, newUser);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null) return NotFound();

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(long id)
    {
        return _context.User.Any(e => e.ID == id);
    }
}