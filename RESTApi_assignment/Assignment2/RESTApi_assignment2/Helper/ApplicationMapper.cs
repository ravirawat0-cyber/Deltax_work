using AutoMapper;
using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;

namespace RESTApi_assignment2.Helper
{
    public class ApplicationMapper: Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Movie, MovieRequest>().ReverseMap();
            CreateMap<Movie, MovieResponse>().ReverseMap();

            CreateMap<Actor, ActorRequest>().ReverseMap();
            CreateMap<Actor, ActorRespone>().ReverseMap();

            CreateMap<Producer, ProducerRequest>().ReverseMap();
            CreateMap<Producer, ProducerRespone>().ReverseMap();

            CreateMap<Genre, GenreRequest>().ReverseMap();
            CreateMap<Genre, GenreResponse>().ReverseMap();

            CreateMap<Review, ReviewRequest>().ReverseMap();
            CreateMap<Review, ReviewResponse>().ReverseMap();
        }
    }
}
