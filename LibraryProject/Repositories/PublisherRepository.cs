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
    public class PublisherRepository : IPublisherRepository     // This class is inheriting interfcae IPublisherRepository and implement the interfaces
    {
        private readonly LibraryProjectContext _context;        //making an instance of the class LibraryProjectContext

        public PublisherRepository(LibraryProjectContext context)      //dependency injection with parameter 
        {
            _context = context;
        }

        //**implementing the methods of IPublisherRepository interface**// 

        //This method will remove one specific Publisher whose Id has been got
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

        //This method will add a new Publisher to the system
        public async Task<Publisher> InsertNewPublisher(Publisher publisher)
        {
            _context.Publisher.Add(publisher);
            await _context.SaveChangesAsync();

            return publisher;
        }

        //this method will get all Publishers details
        public async Task<List<Publisher>> SelectAllPublishers()
        {
            return await _context.Publisher.ToListAsync();
        }

        //this method will get info of one Publisher by specific ID
        public async Task<Publisher> SelectPublisherById(int publisherId)
        {
            return await _context.Publisher
                .FirstOrDefaultAsync(Publisher => Publisher.Id == publisherId);
        }

        //This method will update the information of the specific Publisher by ID
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
