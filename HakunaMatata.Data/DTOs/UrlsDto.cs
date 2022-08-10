namespace HakunaMatata.Data.DTOs
{
    public class UrlsDto
    {
        public IEnumerable<string> Urls { get; }

        public UrlsDto(IEnumerable<string> urls)
        {
            Urls = urls;
        }
    }
}
