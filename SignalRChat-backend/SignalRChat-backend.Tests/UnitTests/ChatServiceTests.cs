using Moq;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;
using SignalRChat_backend.Services.Interfaces;
using SignalRChat_backend.Services.Services;

namespace SignalRChat_backend.Tests.UnitTests
{
    [TestFixture]
    public class ChatServiceTests
    {
        private Mock<IDbEntityService<Chat>> _mockChatService;
        private Mock<IChatDbService> _mockChatDbService;
        private IChatService _chatService;

        [SetUp]
        public void SetUp()
        {
            _mockChatService = new Mock<IDbEntityService<Chat>>();
            _mockChatDbService = new Mock<IChatDbService>();
            _chatService = new ChatService(_mockChatService.Object, _mockChatDbService.Object);
        }

        [Test]
        public void DeleteChatByIdAsync_ShouldThrowException_WhenChatNotFound()
        {
            // Arrange
            var chatId = 1;
            var userId = 1;

            _mockChatService.Setup(cs => cs.GetById(chatId)).ReturnsAsync((Chat)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _chatService.DeleteChatByIdAsync(chatId, userId));
            Assert.AreEqual($"Chat with Id: {chatId} not found", ex.Message);
        }

        [Test]
        public void DeleteChatByIdAsync_ShouldThrowUserException_WhenUserIsNotCreator()
        {
            // Arrange
            var chatId = 1;
            var userId = 1;
            var chat = new Chat { Id = chatId, CreatorId = 2 };

            _mockChatService.Setup(cs => cs.GetById(chatId)).ReturnsAsync(chat);

            // Act & Assert
            var ex = Assert.ThrowsAsync<UserException>(async () => await _chatService.DeleteChatByIdAsync(chatId, userId));
            Assert.AreEqual("There are no permissions to do the operation", ex.Message);
        }

        [Test]
        public async Task DeleteChatByIdAsync_ShouldDeleteChat_WhenUserIsCreator()
        {
            // Arrange
            var chatId = 1;
            var userId = 1;
            var chat = new Chat { Id = chatId, CreatorId = userId };

            _mockChatService.Setup(cs => cs.GetById(chatId)).ReturnsAsync(chat);
            _mockChatService.Setup(cs => cs.Delete(chat)).Returns(Task.CompletedTask);

            // Act
            await _chatService.DeleteChatByIdAsync(chatId, userId);

            // Assert
            _mockChatService.Verify(cs => cs.Delete(chat), Times.Once);
        }

        [Test]
        public void GetChatByIdAsync_ShouldThrowException_WhenChatNotFound()
        {
            // Arrange
            var chatId = 1;

            _mockChatDbService.Setup(cds => cds.GetChatByIdAsync(chatId)).ReturnsAsync((Chat)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _chatService.GetChatByIdAsync(chatId));
            Assert.AreEqual($"Chat with Id: {chatId} not found", ex.Message);
        }

        [Test]
        public async Task GetChatByIdAsync_ShouldReturnChat_WhenChatFound()
        {
            // Arrange
            var chatId = 1;
            var chat = new Chat { Id = chatId };

            _mockChatDbService.Setup(cds => cds.GetChatByIdAsync(chatId)).ReturnsAsync(chat);

            // Act
            var result = await _chatService.GetChatByIdAsync(chatId);

            // Assert
            Assert.AreEqual(chatId, result.Id);
        }

        [Test]
        public async Task SearchChatsByNameAsync_ShouldReturnChats_WhenChatsFound()
        {
            // Arrange
            var chatName = "Test";
            var chats = new List<Chat> { new Chat(), new Chat() };

            _mockChatDbService.Setup(cds => cds.ChatSearchByNameAsync(chatName)).ReturnsAsync(chats);

            // Act
            var result = await _chatService.SearchChatsByNameAsync(chatName);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task AddUserToChatAsync_ShouldAddUserToChat()
        {
            // Arrange
            var chatId = 1;
            var userId = 1;
            var connectionId = "connection";

            _mockChatDbService.Setup(cds => cds.AddUserToChatAsync(chatId, userId, connectionId)).Returns(Task.CompletedTask);

            // Act
            await _chatService.AddUserToChatAsync(chatId, userId, connectionId);

            // Assert
            _mockChatDbService.Verify(cds => cds.AddUserToChatAsync(chatId, userId, connectionId), Times.Once);
        }

        [Test]
        public async Task RemoveUserFromChatAsync_ShouldRemoveUserFromChat()
        {
            // Arrange
            var chatId = 1;
            var userId = 1;
            var connectionId = "connection";

            _mockChatDbService.Setup(cds => cds.RemoveUserFromChatAsync(chatId, userId, connectionId)).Returns(Task.CompletedTask);

            // Act
            await _chatService.RemoveUserFromChatAsync(chatId, userId, connectionId);

            // Assert
            _mockChatDbService.Verify(cds => cds.RemoveUserFromChatAsync(chatId, userId, connectionId), Times.Once);
        }

        [Test]
        public async Task RemoveUsersFromChatAsync_ShouldReturnRemovedUsers()
        {
            // Arrange
            var chatId = 1;
            var userChats = new List<UserChat> { new UserChat(), new UserChat() };

            _mockChatDbService.Setup(cds => cds.RemoveUsersFromChatAsync(chatId)).ReturnsAsync(userChats);

            // Act
            var result = await _chatService.RemoveUsersFromChatAsync(chatId);

            // Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
