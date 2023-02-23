using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.MappingProFiles;
using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;
using CollectionsAndLinq.Common.Models.Users;
using CollectionsAndLinq.WebAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectStructure.BL.Validator;

namespace CollectionsAndLinq.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITasksService, TaskService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IDataProcessingService, DataProcessingService>();
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TeamProFile());
                mc.AddProfile(new UserProFile());
                mc.AddProfile(new TasksProFile());
                mc.AddProfile(new ProjectProFiles());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void RegisterCustomValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<NewProjectDto>, ProjectDtoValidator>();
            services.AddSingleton<IValidator<NewTaskDto>, TaskDtoValidator>();
            services.AddSingleton<IValidator<NewTeamDto>, TeamDtoValidator>();
            services.AddSingleton<IValidator<NewUserDto>, UserDtoValidator>();
        }

        public static void ConfigureCustomValidationErrors(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Message = "Validation errors",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });
        }
    }
}
