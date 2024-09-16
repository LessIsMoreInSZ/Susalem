namespace Susalem.Abp.IdentityServer.ApiResources
{
    public class ApiResourceSecretCreateOrUpdateDto : SecretDto
    {
        public HashType HashType { get; set; }
    }
}
