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
        Task<List<Publisher>> SelectAllPublishers();
        Task<Publisher> SelectPublisherById(int publisherId);
        Task<Publisher> InsertNewPublisherWithProcedure(Publisher publisher);
        Task<Publisher> UpdateExistingPublisher(int publisherId, Publisher publisher);
        Task<Publisher> DeletePublisherWithProcedure(int publisherId);
    }
    public class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryProjectContext _context;
        public string _connectionString;

        public PublisherRepository(LibraryProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
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

        public async Task<Publisher> InsertNewPublisherWithProcedure(Publisher publisher)
        {         
            using SqlConnection sql = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("insertPublisher", sql);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Name", publisher.Name));            

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return publisher;
        }

        public async Task<List<Publisher>> SelectAllPublishers()
        {
            return await _context.Publisher.ToListAsync();
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
