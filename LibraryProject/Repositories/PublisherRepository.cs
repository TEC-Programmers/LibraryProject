using LibraryProject.API.Database.Entities;
using LibraryProject.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> SelectAllPublishers();
        Task<Publisher> SelectPublisherById(int publisherId);
        Task<Publisher> InsertNewPublisher(Publisher publisher);
        Task<Publisher> UpdateExistingPublisher(int publisherId, Publisher publisher);
        Task<Publisher> DeletePublisher(int publisherId);
    }
    public class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryProjectContext _context;

        public PublisherRepository(LibraryProjectContext context)
        {
            _context = context;
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

        public async Task<Publisher> InsertNewPublisher(Publisher publisher)
        {
            _context.Publisher.Add(publisher);
            await _context.SaveChangesAsync();

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
