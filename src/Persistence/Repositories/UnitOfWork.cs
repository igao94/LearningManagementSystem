﻿using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(AppDbContext context,
    IAccountRepository accountRepository,
    IStudentRepository studentRepository,
    ICourseRepository courseRepository,
    IEmailTokenRepository emailTokenRepository) : IUnitOfWork
{
    public IAccountRepository AccountRepository => accountRepository;

    public IStudentRepository StudentRepository => studentRepository;

    public ICourseRepository CourseRepository => courseRepository;

    public IEmailTokenRepository EmailTokenRepository => emailTokenRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
