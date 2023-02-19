namespace UnlockablePostsAPI.InputModels
{
    public class NonceValidationDTO
    {
        public string SignedNonce { get; set; } = "";
        public string NewAddress { get; set; } = "";
    }
}
