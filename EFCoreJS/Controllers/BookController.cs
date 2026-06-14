using EFCoreJS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreJS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var result = await appDbContext.Books.ToListAsync();
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBookAsync([FromBody] Book model)
        {
            appDbContext.Books.Add(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooksAsync([FromBody] List<Book> books)
        {
            appDbContext.Books.AddRange(books);
            await appDbContext.SaveChangesAsync();
            return Ok(books);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int bookId, [FromBody] Book book)
        {
            var result = await appDbContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);
            if (result == null) return NotFound();
            result.Title = book.Title;
            result.Description = book.Description;
            result.NoOfPages = book.NoOfPages;
            await appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateBookWithSingleQueryAsync([FromBody] Book book)
        {
            appDbContext.Books.Update(book);
            await appDbContext.SaveChangesAsync();
            return Ok(book);
        }

    }

}