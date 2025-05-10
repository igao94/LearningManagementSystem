namespace Application.Interfaces;

public interface IEmailVerificationLinkFactory
{
    string CreateVerificationLink(string tokenId);
}
