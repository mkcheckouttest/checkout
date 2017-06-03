using System;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using Checkout.OfficeShoppingList.WebApi.Exceptions;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Checkout.OfficeShoppingList.WebApi.Tests.Exceptions
{
    [TestFixture]
    public class GlobalExceptionHandlerUnitTests
    {
        private IFixture fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture()
                .Customize(new AutoMoqCustomization())
                .Customize(new WebApiContextCustomization());
        }

        [Test]
        public void Handle_WhenCalled_MustSetExceptionResultToBeTypeCoreExceptionResult()
        {
            // Arrange
            var context = new ExceptionHandlerContext(
                new ExceptionContext(
                    new Exception(),
                    fixture.Create<ExceptionContextCatchBlock>(),
                    fixture.Create<HttpRequestMessage>(),
                    fixture.Create<HttpResponseMessage>()));

            var subject = fixture.Create<GlobalExceptionHandler>();

            // Act
            subject.Handle(context);

            // Assert
            Assert.That(context.Result, Is.TypeOf<CoreExceptionResult>());
        }

        [Test]
        public void ShouldHandle_WhenCalled_MustReturnTrue()
        {
            // Arrange
            var context = new ExceptionHandlerContext(
                new ExceptionContext(
                    new Exception(),
                    fixture.Create<ExceptionContextCatchBlock>(),
                    fixture.Create<HttpRequestMessage>(),
                    fixture.Create<HttpResponseMessage>()));

            var subject = fixture.Create<GlobalExceptionHandler>();

            // Act
            var result = subject.ShouldHandle(context);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
