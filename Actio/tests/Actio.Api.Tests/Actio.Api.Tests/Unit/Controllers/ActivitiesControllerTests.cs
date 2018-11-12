using Actio.Api.Controllers;
using Actio.Api.Repositories;
using Actio.Common.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class ActivitiesControllerTests
    {
        [Fact]
        public async Task Activities_controlller_post_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();

            var activityRespositotyMock = new Mock<IActivityRepository>();

            var controller = new ActivitiesController(busClientMock.Object, activityRespositotyMock.Object);

            var user = Guid.NewGuid();

            controller.ControllerContext = new  ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name,user.ToString())
                    },"test"))
                }
            };


            var command = new CreateActivity()
            {
                Id = Guid.NewGuid(),
                UserId = user,
            };

            var result = await controller.Post(command);


            var contentResult = result as AcceptedResult;

            contentResult.Should().NotBeNull();

            contentResult.Location.ShouldBeEquivalentTo($"activities/{command.Id}");
        }
    }
}
