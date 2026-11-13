using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;

namespace PDV.Web.Autenticacao;

public sealed class ArmazenamentoTicketAutenticacao(IMemoryCache cache) : ITicketStore
{
    public Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var chave = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        Guardar(chave, ticket);
        return Task.FromResult(chave);
    }

    public Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        Guardar(key, ticket);
        return Task.CompletedTask;
    }

    public Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        cache.TryGetValue(key, out AuthenticationTicket? ticket);
        return Task.FromResult(ticket);
    }

    public Task RemoveAsync(string key)
    {
        cache.Remove(key);
        return Task.CompletedTask;
    }

    private void Guardar(string key, AuthenticationTicket ticket)
    {
        var expiracao = ticket.Properties.ExpiresUtc
            ?? throw new InvalidOperationException("O ticket de autenticação precisa ter expiração.");

        cache.Set(key, ticket, new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = expiracao
        });
    }
}
