using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface IPublisherService
    {
        Task<List<PublisherResponse>> GetAllPublishers();
        Task<PublisherResponse> GetPublisherById(int publisherId);
        Task<PublisherResponse> CreatePublisher(PublisherRequest newPublisher);
        Task<PublisherResponse> UpdatePublisher(int publisherId, PublisherRequest newPublisher);
        Task<PublisherResponse> DeletePublisher(int publisherId);
    }
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository PublisherRepository)
        {
            _publisherRepository = PublisherRepository;
        }

        public async Task<PublisherResponse> CreatePublisher(PublisherRequest newPublisher)
        {
            Publisher publisher  = MapPublisherRequestToPublisher(newPublisher);

            Publisher insertedPublisher = await _publisherRepository.InsertNewPublisher(publisher);

            if (insertedPublisher != null)
            {
                return MapPublisherToPublisherResponse(insertedPublisher);
            }
            return null;
        }

        public async Task<PublisherResponse> DeletePublisher(int publisherId)
        {
            Publisher deletedPublisher = await _publisherRepository.DeletePublisher(publisherId);

            if (deletedPublisher != null)
            {
                return MapPublisherToPublisherResponse(deletedPublisher);
            }
            return null;
        }

        public async Task<List<PublisherResponse>> GetAllPublishers()
        {
            List<Publisher> Publishers = await _publisherRepository.SelectAllPublishers();
            return Publishers.Select(Publisher => MapPublisherToPublisherResponse(Publisher)).ToList();
        }

        public async Task<PublisherResponse> GetPublisherById(int publisherId)
        {
            Publisher Publisher = await _publisherRepository.SelectPublisherById(publisherId);
            if (Publisher != null)
            {
                return MapPublisherToPublisherResponse(Publisher);
            }
            return null;
        }

        public async Task<PublisherResponse> UpdatePublisher(int publisherId, PublisherRequest updatePublisher)
        {
            Publisher Publisher = MapPublisherRequestToPublisher(updatePublisher);

            Publisher updatedPublisher = await _publisherRepository.UpdateExistingPublisher(publisherId, Publisher);

            if (updatedPublisher != null)
            {
                return MapPublisherToPublisherResponse(updatedPublisher);
            }
            return null;
        }

        private Publisher MapPublisherRequestToPublisher(PublisherRequest publisherRequest)
        {
            return new Publisher()
            {
                Name = publisherRequest.Name,            
            };
        }

        private PublisherResponse MapPublisherToPublisherResponse(Publisher Publisher)
        {
            return new PublisherResponse()
            {
                Id = Publisher.Id,
                Name = Publisher.Name,           
            };
        }
    }
}
