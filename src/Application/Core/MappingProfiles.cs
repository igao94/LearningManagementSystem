using Application.Courses.DTOs;
using Application.Students.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, StudentDto>();

        CreateMap<UpdateStudentDto, User>();

        CreateMap<Course, CourseDto>();

        CreateMap<CreateCourseDto, Course>();

        CreateMap<Lesson, LessonDto>();
    }
}
