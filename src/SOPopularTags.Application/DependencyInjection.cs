using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Application.Jobs;
using SOPopularTags.Application.Mapping;
using SOPopularTags.Application.Services;

namespace SOPopularTags.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add automapper to ease converting entity objects to DTOs
            services.AddAutoMapper(cfg =>
                cfg.AddProfile<SOTagRequestProfile>());

            // Quartz.NET executes given jobs based on associated trigger
            services.AddQuartz(q =>
            {
                // This line makes it possible to use scoped services inside IHostedService implementation of Quartz.NET
                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                // Create a new job key so it can be associated with jobs and triggers later on
                var jobKey = new JobKey("ConsumePopularTags");

                // Add a new job we created with our key
                q.AddJob<ConsumePopularTagsJob>(options =>
                    options.WithIdentity(jobKey));

                // Add a trigger to our job so it knows when to execute
                q.AddTrigger(options => options
                    .ForJob(jobKey)
                    .WithIdentity($"{jobKey.Name}trigger")
                    // Execute job at the start of the program
                    .StartNow()
                    .WithSimpleSchedule(s => s
                        // Execute job every 10 minutes
                        .WithInterval(TimeSpan.FromMinutes(10))
                        .RepeatForever())
                    );
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            // Add some services
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IPopularityCalculatorService, PopularityCalculatorService>();
            services.AddScoped<IConsumePopularTagsService, ConsumePopularTagsService>();
            return services;
        }
    }
}
