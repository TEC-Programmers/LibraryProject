using LibraryProject.API.Database.Entities;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> SelectAllPublishersWithProcedure();
        Task<Publisher> SelectPublisherById(int publisherId);
        Task<Publisher> InsertNewPublisherWithProcedure(Publisher publisher);
        Task<Publisher> InsertNewPublisher(Publisher publisher);
        Task<Publisher> UpdateExistingPublisher(int publisherId, Publisher publisher);
        Task<Publisher> DeletePublisherWithProcedure(int publisherId);
        Task<Publisher> DeletePublisher(int publisherId);

    }
    public class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryProjectContext _context;

        public PublisherRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<Publisher> DeletePublisherWithProcedure(int publisherId)
        {           
            Publisher deletePublisher = await _context.Publisher
               .FirstOrDefaultAsync(u => u.Id == publisherId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", publisherId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC deletePublisher @Id", parameter.ToArray());
            return deletePublisher;
        }
        public async Task<Publisher> DeletePublisher(int publisherId)
        {
            Publisher deletePublisher = await _context.Publisher
                .FirstOrDefaultAsync(publisher => publisher.Id == publisherId);
            if (deletePublisher != null)
            {
                _context.Publisher.Remove(deletePublisher);
                await _context.SaveChangesAsync();
            }
            return deletePublisher;
        }

        public async Task<Publisher> InsertNewPublisherWithProcedure(Publisher publisher)
        {
            var name = new SqlParameter("@Name", publisher.Name);

            await _context.Database.ExecuteSqlRawAsync("exec insertPublisher @Name", name);
            return publisher;          
        }
        public async Task<Publisher> InsertNewPublisher(Publisher publisher)
        {
            _context.Publisher.Add(publisher);
            await _context.SaveChangesAsync();

            return publisher;
        }
        public async Task<List<Publisher>> SelectAllPublishersWithProcedure()
        {
            return await _context.Publisher.FromSqlRaw("selectAllPublishers").ToListAsync();
        }
        public async Task<Publisher> SelectPublisherById(int publisherId)
        {
            return await _context.Publisher
                .FirstOrDefaultAsync(Publisher => Publisher.Id == publisherId);
        }
        public async Task<Publisher> UpdateExistingPublisher(int publisherId, Publisher Publisher)
        {
            Publisher upatePublisher = await _context.Publisher
                .FirstOrDefaultAsync(publisher => publisher.Id == publisherId);

            if (upatePublisher != null)
            {
                upatePublisher.Name = Publisher.Name;             
                await _context.SaveChangesAsync();
            }

            return upatePublisher;
        }
    }
}
