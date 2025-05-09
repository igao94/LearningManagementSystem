﻿using Application.Courses.DTOs;
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

        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.StudentsCount, opt => opt.MapFrom(src => src.Attendees.Count));

        CreateMap<CreateCourseDto, Course>();

        CreateMap<Lesson, LessonDto>();

        CreateMap<UpdateCourseDto, Course>();

        CreateMap<UpdateLessonDto, Lesson>();

        CreateMap<Certificate, CertificateDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Student.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Student.LastName))
            .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course.Title));
    }
}
