namespace Susalem.Abp.IdentityServer.Clients
{
    public class SecretCreateOrUpdateDto : SecretDto
    {
        public HashType HashType { get; set; }
    }
}
