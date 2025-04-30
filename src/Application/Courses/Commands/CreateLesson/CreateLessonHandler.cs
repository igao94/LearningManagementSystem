using Application.Core;
using Application.Courses.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.CreateLesson;

public class CreateLessonHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateLessonCommand, Result<LessonDto>>
{
    public async Task<Result<LessonDto>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.CourseRepository.GetCourseByIdAsync(request.CreateLessonDto.CourseId);

        if (course is null)
        {
            return Result<LessonDto>.Failure("Course not found.", 404);
        }

        var lesson = new Lesson
        {
            CourseId = course.Id,
            Title = request.CreateLessonDto.Title,
            ContentUrl = request.CreateLessonDto.ContentUrl
        };

        course.Lessons.Add(lesson);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<LessonDto>.Success(mapper.Map<LessonDto>(lesson))
            : Result<LessonDto>.Failure("Failed to create lesson.", 400);
    }
}
