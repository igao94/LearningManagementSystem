namespace Application.Interfaces;

public interface IEmailVerificationLinkFactory
{
    string Create(string tokenId);
}
