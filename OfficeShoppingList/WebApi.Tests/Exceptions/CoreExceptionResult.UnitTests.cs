using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Checkout.OfficeShoppingList.WebApi.Exceptions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Checkout.OfficeShoppingList.WebApi.Tests.Exceptions
{
    [TestFixture]
    public class CoreExceptionResultUnitTests
    {
        private IFixture fixture;
        private Mock<IHttpErrorResponseFactory> mockHttpErrorResponseFactory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            fixture = new Fixture()
                .Customize(new AutoMoqCustomization())
                .Customize(new WebApiContextCustomization());
            mockHttpErrorResponseFactory = fixture.Freeze<Mock<IHttpErrorResponseFactory>>();
        }

        [TearDown]
        public void TearDown()
        {
            mockHttpErrorResponseFactory.Reset();
        }

        [Test]
        public void ExecuteAsync_WhenCalled_MustReturnResponseWithCorrectStatusCode()
        {
            var context =
                new ExceptionHandlerContext(
                    new ExceptionContext(
                        new Exception("foo"),
                        new ExceptionContextCatchBlock("foo", false, false),
                        fixture.Create<HttpRequestMessage>(),
                        fixture.Create<HttpResponseMessage>()));

            var expectedResponse = fixture.Create<HttpErrorResponse>();
            mockHttpErrorResponseFactory.Setup(x => x.Create(context.Exception)).Returns(expectedResponse);

            var subject = new CoreExceptionResult(mockHttpErrorResponseFactory.Object, context);

            var result = subject.ExecuteAsync(new CancellationToken()).Result;

            result.StatusCode.Should().Be(expectedResponse.StatusCode);
        }

        [Test]
        public void ExecuteAsync_WhenCalled_MustReturnResponseWithCorrectResponse()
        {
            var context =
                new ExceptionHandlerContext(
                    new ExceptionContext(
                        new Exception("foo"),
                        new ExceptionContextCatchBlock("foo", false, false),
                        fixture.Create<HttpRequestMessage>(),
                        fixture.Create<HttpResponseMessage>()));

            var expectedResponse = fixture.Create<HttpErrorResponse>();
            mockHttpErrorResponseFactory.Setup(x => x.Create(context.Exception)).Returns(expectedResponse);

            var subject = new CoreExceptionResult(mockHttpErrorResponseFactory.Object, context);

            var result = subject.ExecuteAsync(new CancellationToken()).Result;

            var content = (ObjectContent<HttpErrorResponse>) result.Content;
            content.Value.Should().Be(expectedResponse);
        }
    }
}
