using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.Logout
{
    public class LogoutCommandHandler : BaseHandler, IRequestHandler<LogoutCommand, Unit>
    {
        private ILocalStorageService LocalStorageService { get; }

        public LogoutCommandHandler(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient)
        {
            LocalStorageService = localStorageService;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await LocalStorageService.RemoveItemAsync("token");
            await LocalStorageService.RemoveItemAsync("refreshToken");
            return Unit.Value;
        }
    }
}