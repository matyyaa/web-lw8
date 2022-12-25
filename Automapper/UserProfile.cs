using AutoMapper;
using Lab8.Auth.Requests;
using Lab8.Auth.Responses;
using Lab8.Models;

namespace Lab8.Automapper;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<UserRegisterRequest, User>()
            .ForMember(d => d.UserName, x => x.MapFrom(s => s.FirstName))
            .ForMember(d => d.PhoneNumber, x => x.MapFrom(s => s.Phone))
            .ForMember(d => d.Role, x => x.MapFrom(s => Roles.User));

        CreateMap<User, UserInfoResponse>()
            .ForMember(d => d.Name, x => x.MapFrom(s => s.UserName))
            .ForMember(d => d.Phone, x => x.MapFrom(s => s.PhoneNumber));
    }
}