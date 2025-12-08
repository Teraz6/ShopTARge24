using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Data;
using ShopTARge24.Models;
using System.Security.Claims;

namespace ShopTARge24.Controllers
{

    [Route("/[controller]")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ShopTARge24Context _context;

        public ChatRoomsController(ShopTARge24Context context)
        {
            _context = context;
        }

        // GET: api/ChatRooms
        [HttpGet]
        [Route("/[controller]/GetChatRoom")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRoom()
        {
            if (_context.ChatRoom == null)
            {
                return NotFound();
            }
            return await _context.ChatRoom.ToListAsync();
        }

        [HttpGet]
        [Route("/[controller]/GetChatUser")]
        public async Task<ActionResult<Object>> GetChatUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            return users.Where(u => u.Id != userId).Select(u => new { u.Id, u.UserName }).ToList();
        }




        // POST: api/ChatRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("/[controller]/PostChatRoom")]
        public async Task<ActionResult<ChatRoom>> PostChatRoom(ChatRoom chatRoom)
        {
            if (_context.ChatRoom == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ChatRoom'  is null.");
            }
            _context.ChatRoom.Add(chatRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatRoom", new { id = chatRoom.Id }, chatRoom);
        }

        // DELETE: api/ChatRooms/5
        [HttpDelete("{id}")]
        [Route("/[controller]/DeleteChatRoom/{id}")]
        public async Task<IActionResult> DeleteChatRoom(int id)
        {
            if (_context.ChatRoom == null)
            {
                return NotFound();
            }
            var chatRoom = await _context.ChatRoom.FindAsync(id);
            if (chatRoom == null)
            {
                return NotFound();
            }

            _context.ChatRoom.Remove(chatRoom);
            await _context.SaveChangesAsync();

            var room = await _context.ChatRoom.FirstOrDefaultAsync();

            return Ok(new { deleted = id, selected = (room == null ? 0 : room.Id) });
        }


    }
}
