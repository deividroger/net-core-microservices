using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Repositories;
using Actio.Services.Activities.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Services.Activities.Tests.Unit.Services
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task Activity_service_add_async_should_succeed()
        {
            var category =  "test";

            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(x => x.GetAsync(category))
                .ReturnsAsync(new Category(category));


            var actitityService = new ActivityService(activityRepositoryMock.Object, categoryRepositoryMock.Object);

            var id = Guid.NewGuid();

            await actitityService.AddAsync(id, Guid.NewGuid(), category, "activity", "description", DateTime.Now);

            categoryRepositoryMock.Verify(x => x.GetAsync(category), Times.Once);

            activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>() ),Times.Once);

        }
    }
}
