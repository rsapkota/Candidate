using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;
using Xunit;
using Candidate.Domain.Interfaces;
using Candidate.Controllers;
using Candidate.WebAPI.DTOs;

namespace CandidateApp.Tests
{
    public class CandidateControllerTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly CandidateController _controller;

        public CandidateControllerTests()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _controller = new CandidateController(_candidateRepositoryMock.Object);
        }

        [Fact]
        public async Task UpsertCandidate_ShouldReturnBadRequest_WhenCandidateDtoIsNull()
        {
            // Act
            var result = await _controller.UpsertCandidate(null, CancellationToken.None);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpsertCandidate_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Email is required.");

            var candidateDto = new CandidateDto();

            // Act
            var result = await _controller.UpsertCandidate(candidateDto, CancellationToken.None);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpsertCandidate_ShouldUpdateExistingCandidate_WhenCandidateExists()
        {
            // Arrange
            var candidateDto = new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                CallTimeInterval = "Weekdays 10 AM - 12 PM",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                Comment = "Experienced developer."
            };

            var existingCandidate = new Candidate.Domain.Entities.Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                PhoneNumber = "0987654321",
                Email = "john.doe@example.com",
                CallTimeInterval = "Weekdays 2 PM - 4 PM",
                LinkedInUrl = "https://linkedin.com/in/janesmith",
                GitHubUrl = "https://github.com/janesmith",
                Comment = "Software engineer."
            };

            _candidateRepositoryMock
                .Setup(repo => repo.GetCandidateByEmail(candidateDto.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingCandidate);

            // Act
            var result = await _controller.UpsertCandidate(candidateDto, CancellationToken.None);

            // Assert
            _candidateRepositoryMock.Verify(repo => repo.Update(It.IsAny<Candidate.Domain.Entities.Candidate>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpsertCandidate_ShouldAddNewCandidate_WhenCandidateDoesNotExist()
        {
            // Arrange
            var candidateDto = new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                CallTimeInterval = "Weekdays 10 AM - 12 PM",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                Comment = "Experienced developer."
            };

            _candidateRepositoryMock
                .Setup(repo => repo.GetCandidateByEmail(candidateDto.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Candidate.Domain.Entities.Candidate)null);

            // Act
            var result = await _controller.UpsertCandidate(candidateDto, CancellationToken.None);

            // Assert
            _candidateRepositoryMock.Verify(repo => repo.Add(It.IsAny<Candidate.Domain.Entities.Candidate>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
